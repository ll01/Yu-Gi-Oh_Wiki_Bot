using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace Ygo_Deck_Helper
{
    internal class YuGiOh_Wiki_Search
    {
        private const string YuGiOhWikiUrl = "http://yugioh.wikia.com/wiki/";

        // Only use when string already contains /wiki/ url inside
        private const string YuGiOhWikiUrl_LOCAL = "http://yugioh.wikia.com";
        private string NOT_APPLICABLE = null;

        private string Card_Url;
        private HtmlDocument Card_Page;

        public enum YuGiOh_Wiki_Data_Field
        {
            Name_en,
            Name_fr,
            Name_de,
            Name_it,
            Name_kr,
            Name_pt,
            Name_es,
            Name_jp,
            Name_Translated,
            Card_Type,
            Attribute,
            Type_List,
            Level_Rank,
            Scale,
            Link_Arrows,
            Stat_Line,
            Passcode,
            Material,
            Effect_Type_List,
            Current_Banlist_Status,
            Effect_Text,
            TCG_en_Set_Code,
            OCG_jp_Set_Code,
            ArchType,
            TCG_Rarity,
            OCG_Rarity,
            YuGiOh_Wiki_URL,
        }

        /// <summary>
        /// finds the cards id given its name
        /// </summary>
        /// <param name="Card_Name">the name to search ygo wiki for</param>
        /// <returns>a key value pair where the key is the id of the card and the bool is wether or not the search is sucsesful</returns>
        public async Task<(string Infomation, string Country_Code, bool isSucsesful)> Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field Data_Field)
        {
            string xPath_Query = null;
            bool select_Multiple = false;
            string Country_Code = "N/A";
            switch (Data_Field)
            {
                case YuGiOh_Wiki_Data_Field.Passcode:
                    xPath_Query = "//th/a[text() = 'Passcode']/../following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_en:
                    xPath_Query = "//th[text() = 'English']/following-sibling::td";
                    Country_Code = "EN";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_fr:
                    xPath_Query = "//th[text() = 'French']/following-sibling::td/span";
                    Country_Code = "FR";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_de:
                    xPath_Query = "//th[text() = 'German']/following-sibling::td/span";
                    Country_Code = "DE";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_it:
                    xPath_Query = "//th[text() = 'Italian']/following-sibling::td/span";
                    Country_Code = "IT";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_kr:
                    xPath_Query = "//th[text() = 'Korean']/following-sibling::td/span";
                    Country_Code = "KR";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_pt:
                    xPath_Query = "//th[text() = 'Portuguese']/following-sibling::td/span";
                    Country_Code = "PT";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_es:
                    xPath_Query = "//th[text() = 'Spanish']/following-sibling::td/span";
                    Country_Code = "ES";
                    break;

                case YuGiOh_Wiki_Data_Field.Name_jp:
                    xPath_Query = "//th[contains(., 'Japanese') and contains(., '(base)')]/following-sibling::td/span | //th[text() = 'Japanese']/following-sibling::td/span";
                   Country_Code = "JP";
                    break;

                case YuGiOh_Wiki_Data_Field.Card_Type:
                    xPath_Query = "//th/a[normalize-space(text()) = 'Card type']/../following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Attribute:
                    xPath_Query = "//th/a[text() = 'Attribute']/../following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Type_List:
                    xPath_Query = "//th/a[text() = 'Types']/../following-sibling::td |//th/a[text() = 'Type']/../following-sibling::td | //th/a[text() = 'Property']/../following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Level_Rank:
                    xPath_Query = "//th/a[text() = 'Level']/../following-sibling::td/a[1] | //th/a[text() = 'Rank']/../following-sibling::td/a[1]";
                    break;

                case YuGiOh_Wiki_Data_Field.Scale:
                    xPath_Query = "//th/a[text() = 'Pendulum Scale']/../following-sibling::td/a[2]";
                    break;
                case YuGiOh_Wiki_Data_Field.Link_Arrows:
                    select_Multiple = true;
                    xPath_Query = "//th/a[normalize-space(text()) = 'Link Arrows']/../following-sibling::td/a[not(contains(.,'img'))]";
                    break;

                case YuGiOh_Wiki_Data_Field.Stat_Line:
                    xPath_Query = "//th[contains(., 'ATK') and contains(., 'DEF')]/following-sibling::td | //th[contains(., 'ATK') and contains(., 'LINK')]/following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Material:
                    xPath_Query = "//th[text() = 'Materials']/following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.Effect_Type_List:
                    xPath_Query = "//th[normalize-space(text()) = 'Card effect types']/following-sibling::td";
                    break;

                case YuGiOh_Wiki_Data_Field.ArchType:
                    select_Multiple = true;
                    xPath_Query = "//div[@class = 'cardtable-categories']/div[contains(., 'Archetypes')]/dl/dd/a | //div[@class = 'cardtable-categories']/div[contains(., 'archetypes')]/dl/dd/a";
                    break;


                default:
                    Exception e = new NotImplementedException("Feild to scrape " + Data_Field.ToString() + "not implemented yet");
                    throw e;
            }
            try
            {
                string Card_Text = NOT_APPLICABLE;
                //find the table row that contains card number and scrap that cell
                var Card_Text_Node = this.Card_Page.DocumentNode.SelectNodes(xPath_Query);
                if (Card_Text_Node != null)
                {
                    string Card_Infomation_Selected = select_Multiple ? string.Join(",", Card_Text_Node.Nodes().
                           Where(x => !string.IsNullOrEmpty(x.InnerText)).
                           Select(x => x.InnerText.Trim())) : Card_Text_Node[0].InnerText;
                    Card_Text = string.IsNullOrWhiteSpace(Card_Infomation_Selected) ? NOT_APPLICABLE : Card_Infomation_Selected.Trim();
                }
                return (Card_Text, Country_Code, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                string error = "N/A somthing whent wrong" + e.Message ;
                return (error, error, false);
            }
        }

        // async factory http://blog.stephencleary.com/2013/01/async-oop-2-constructors.html
        private YuGiOh_Wiki_Search(string New_Card_Url, bool isUrl)
        {
            if (isUrl == false)
            {
                string Wiki_Card_Name = Data_Check.Format_Card_Name_For_Wiki_Search(New_Card_Url);
                this.Card_Url = YuGiOhWikiUrl + Wiki_Card_Name;
            }
            else
            {
                this.Card_Url = New_Card_Url;
            }
        }

        private async Task<YuGiOh_Wiki_Search> Initialize_YuGiOh_Wiki_Search()
        {
            var Card_Page_Get = new HtmlWeb();
            Card_Page = await Task.Run(() => Card_Page_Get.Load(Card_Url));
            return this;
        }

        public static Task<YuGiOh_Wiki_Search> Create_YuGiOh_Wiki_Search(string Card_Url, bool isUrl)
        {
            var New_Wiki_Search = new YuGiOh_Wiki_Search(Card_Url, isUrl);
            return New_Wiki_Search.Initialize_YuGiOh_Wiki_Search();
        }

        private async Task<string> GetPageAsString_Using_WebRequest()
        {
            var request = WebRequest.Create(Card_Url);
            using (var response = await request.GetResponseAsync())
            using (var stream = new System.IO.StreamReader(response.GetResponseStream()))
                return stream.ReadToEnd();
        }

        private async Task<string> GetPageAsString_Using_HttpClient()
        {
            var client = new HttpClient();
            using (var data = await client.GetAsync(Card_Url).ConfigureAwait(continueOnCapturedContext: false))
                return await data.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
        }
        static  int illigal_Card_Counter  = -1;
        public async Task<Card> Scrape_Card()
        {

           

                var Name_EN = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Name_en);
                var Card_Name_Japanese_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Name_jp);

                var Card_Type_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Card_Type);
                var Passcode_Raw_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Passcode);



                var ArchType_Query_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.ArchType);

                var Effect_Type_Query_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Effect_Type_List);

                var Type_Property_Query_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Type_List);



                string Card_Name_English = (await Name_EN).Infomation;
                string Card_Name_Japanese = (await Card_Name_Japanese_Task).Infomation;

                //TODO: ADD FORIGN NAMES 

                string Passcode = (await Passcode_Raw_Task).Infomation;
                if (Passcode == null)
                    Passcode = illigal_Card_Counter--.ToString();

                var ArchType_Query = (await ArchType_Query_Task);
                string ArchType_List_Raw = ArchType_Query.isSucsesful ? ArchType_Query.Infomation : NOT_APPLICABLE;
                List<string> ArchType_List_Split = !string.IsNullOrEmpty(ArchType_List_Raw) ? ArchType_List_Split = ArchType_List_Raw.Split(',').ToList() : null;

                var Effect_Type_Query = (await Effect_Type_Query_Task);
                List<string> Effect_Type_List = Effect_Type_Query.isSucsesful && Effect_Type_Query.Infomation != null ? Data_Check.Split_Card_Effect_List(Effect_Type_Query.Infomation) : null;

                var Type_Property_Query = (await Type_Property_Query_Task);
                List<string> Type_Property_List = Type_Property_Query.isSucsesful ? Type_Property_Query.Infomation.Split('/').Select(x => x.Trim()).ToList() : null;
                string Card_Type = (await Card_Type_Task).Infomation;
                bool isMonster = Card_Type.ToLower() == "monster";
                bool isLink = Type_Property_List.Contains("Link");
                bool isExtaDeckCard = Type_Property_List.Contains("Link") || Type_Property_List.Contains("Synchro") ||
                  Type_Property_List.Contains("Xyz") || Type_Property_List.Contains("Fusion");



                var Attribute_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Attribute);
                var Level_Rank_String_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Level_Rank);
                var Scale_Query_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Scale);
                var Link_Arrows_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Link_Arrows);
                var Material_Task = Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Material);

                string Attribute = isMonster ? (await Attribute_Task).Infomation : NOT_APPLICABLE;
                string Level_Rank_String = isMonster ? (await Level_Rank_String_Task).Infomation : NOT_APPLICABLE;


                var Scale_Query = await Scale_Query_Task;
                string Scale = isMonster ? Scale_Query.Infomation : NOT_APPLICABLE;

                string Defence_or_Link = null;
                string Attack = null;
                if (isMonster)
                {
                    string Stat_Line_Raw = isMonster ? (await Scrape_Card_Field_Infomation(YuGiOh_Wiki_Data_Field.Stat_Line)).Infomation : NOT_APPLICABLE;
                    string[] Stat_Line_Split = Stat_Line_Raw.Split('/');
                    Attack = Stat_Line_Split[0];
                    Defence_or_Link = Stat_Line_Split[1];
                }

                List<string> Link_Arrows = isLink ? (await Link_Arrows_Task).Infomation.Split(',').ToList() : null;
                string Material = isExtaDeckCard ? (await Material_Task).Infomation : NOT_APPLICABLE;
                return new Card(int.Parse(Passcode), Card_Name_English, Card_Name_Japanese, Card_Type, Attribute,
                Level_Rank_String, Scale, Attack, Defence_or_Link, Material, Effect_Type_List, Type_Property_List, ArchType_List_Split);
         

               
            
         
        }

        public static async Task Scrape_Wiki_Name_List_Page(string Current_Wiki_Card_Page_Url)
        {

            var Card_List_Page_Get = new HtmlWeb();
            var Card_List_Page = Card_List_Page_Get.Load(Current_Wiki_Card_Page_Url);
            HtmlNode YuGioh_Wiki_Main_DataNode = Card_List_Page.GetElementbyId("mw-pages");
            var Card_Collection = YuGioh_Wiki_Main_DataNode.SelectNodes("//div[@class='mw-content-ltr']//table//ul//li").Nodes().ToArray();
            List<Task> tl = new List<Task>();
            for (int i = 0; i < Card_Collection.Length; i++)
            {
                HtmlNode Node = Card_Collection[i];
                string card_Url = YuGiOhWikiUrl_LOCAL + Node.OuterHtml.Split('"')[1];
                if (card_Url.Contains("(temp)") || card_Url.Contains("(original)"))
                    continue;
                var Card_Query = await Create_YuGiOh_Wiki_Search(card_Url, true);
                try
                {
                  
                    Card New_Card = await Card_Query.Scrape_Card();
                     New_Card.insert_Into_Wiki_Database();
                }
                catch
                {
                    Console.WriteLine(card_Url);
                    System.IO.StreamWriter file = new System.IO.StreamWriter("test.txt");
                    file.WriteLine(card_Url);

                    file.Close();

                    
                    continue;
                } finally {
                    using(var card_context = new Card_Context()) {
                        //TODO: FINISH SAVING
                    }
                }

                //Console.WriteLine(card_Url);

            }
            // await Task.WhenAll(tl.ToArray());
        }

        public static async Task<bool> Scrape_All_Cards()
        {
            string Current_Wiki_Card_Page_Url = YuGiOhWikiUrl + "Category:TCG_cards";

            // this code scrapes the first page for the number of pages it needs to scrape
            var Card_List_Page_Get = new HtmlWeb();
            var Card_List_Page = Card_List_Page_Get.Load(Current_Wiki_Card_Page_Url);
            HtmlNode YuGioh_Wiki_Main_DataNode = Card_List_Page.GetElementbyId("mw-pages");
            string page_Count_String = YuGioh_Wiki_Main_DataNode.SelectSingleNode("//div[@class='wikia-paginator']//ul//li[7]//a[@class='paginator-page']").InnerText;
            int page_Count = int.Parse(page_Count_String);

            List<Task> Scrape_Task_List = new List<Task>();
            for (int i = 1; i < page_Count; i = i + 2)
            {
                //var Current_Task =  Task.Run(() => Scrape_Wiki_Name_List_Page(Current_Wiki_Card_Page_Url + "?page=" + i));

               Task Current_Task = await Task.Run<Task>(() => Scrape_Wiki_Name_List_Page(Current_Wiki_Card_Page_Url + "?page=" + i));
               Scrape_Task_List.Add(Current_Task);
            }
            for (int i = 2; i < page_Count; i = i + 2)
            {
                //var Current_Task =  Task.Run(() => Scrape_Wiki_Name_List_Page(Current_Wiki_Card_Page_Url + "?page=" + i));

                Task Current_Task = await Task.Run<Task>(() => Scrape_Wiki_Name_List_Page(Current_Wiki_Card_Page_Url + "?page=" + i));
                Scrape_Task_List.Add(Current_Task);
            }
            await Task.WhenAll(Scrape_Task_List.ToArray()); 
            return true;
        }
    }
}