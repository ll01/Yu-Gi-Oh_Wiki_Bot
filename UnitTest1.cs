using System;
using System.Linq;
using Xunit;
using Ygo_Deck_Helper;
using Newtonsoft.Json;

namespace Ygo_Unit_Tests
{
    public class UnitTest1
    {
        Database testDatabase = new Database("127.0.0.1", "x", "x", "card_db");
        [Fact]
        public async void Name_En()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);
            var TestQuery =  Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_en);
            Assert.Equal("Trishula, Dragon of the Ice Barrier", TestQuery.Infomation);

        }

        [Fact]
        public async void Name_Fr()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);
            var TestQuery =  Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_fr);
            Assert.Equal("Trishula, Dragon de la Barrière de Glace", TestQuery.Infomation);

        }
        [Fact]
        public async void Name_De()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);
            var TestQuery =  Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_de);
            Assert.Equal("Trishula, Drache der Eisbarriere", TestQuery.Infomation);

        }
        [Fact]
        public async void Name_It()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery =  Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_it);
            Assert.Equal("Trishula, Drago della Barriera di Ghiaccio", TestQuery.Infomation);

        }
        [Fact]
        public async void Name_Kr()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_kr);
            Assert.Equal("빙결계의 용 트리슈라", TestQuery.Infomation);

        }

        [Fact]
        public async void Name_Pt()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);
            var TestQuery =  Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_pt);
            Assert.Equal("Trishula, o Dragão da Barreira de Gelo", TestQuery.Infomation);

        }

        [Fact]
        public async void Name_Es()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Name((int)YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_es);
            Assert.Equal("Trishula, Dragón de la Barrera de Hielo", TestQuery.Infomation);

        }

        [Fact]
        public async void Name_Jp()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery =  Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_jp);
            Assert.Equal("氷結界の龍 トリシューラ", TestQuery.Infomation);

        }

        [Fact]
        public async void Card_Type()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Card_Type);
            Assert.Equal("Monster", TestQuery.Infomation);

        }

        [Fact]
        public async void Attribute()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Attribute);
            Assert.Equal("WATER", TestQuery.Infomation);

        }

        [Fact]
        public async void Type_List()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Type_List);
            Assert.Equal("Dragon / Synchro / Effect", TestQuery.Infomation);

        }

        [Fact]
        public async void Property()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Monster Reborn", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Type_List);
            Assert.Equal("Normal", TestQuery.Infomation);

        }

        [Fact]
        public async void Level()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Level_Rank);
            Assert.Equal("9", TestQuery.Infomation);

        }

        [Fact]
        public async void Rank()
        {

            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Evilswarm Ouroboros", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Level_Rank);
            Assert.Equal("4", TestQuery.Infomation);

        }

        [Fact]
        public async void Stat_Line()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);


            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Stat_Line);
            Assert.Equal("2700 / 2000", TestQuery.Infomation);

        }

        [Fact]
        public async void Stat_Line_with_Link()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Stargrail Shrine Maiden Eve", false);


            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Stat_Line);
            Assert.Equal("1800 / 2", TestQuery.Infomation);

        }

        [Fact]
        public async void Material()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Material);
            Assert.Equal("1 Tuner + 2+ non-Tuner monsters", TestQuery.Infomation);

        }

        [Fact]
        public async void Effect_Type_List_Single()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Trishula, Dragon of the Ice Barrier", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Effect_Type_List);
            Assert.Equal("Trigger", TestQuery.Infomation);

        }

        [Fact]
        public async void Effect_Type_List_Multiple()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Evilswarm Ouroboros", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Effect_Type_List);

            Assert.Equal("Ignition\n Condition", TestQuery.Infomation);
        }

        [Fact]
        public async void Split_Effect_Type_List()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Evilswarm Ouroboros", false);
            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Effect_Type_List);

            string Effect_Collection_NoSplit = TestQuery.Infomation;
            var Actual_Effect_Collection_Split = Data_Check.Split_Card_Effect_List(Effect_Collection_NoSplit).ToArray();
            Assert.Equal(new string[] { "Ignition", "Condition" }, Actual_Effect_Collection_Split);
        }

        [Fact]
        public async void Pendulum_Scale()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Performapal Five-Rainbow Magician", false);

            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Scale);

            Assert.Equal("12", TestQuery.Infomation);
        }

        [Fact]
        public async void Link_Arrows()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Stargrail Shrine Maiden Eve", false);


            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Link_Arrows);
            Assert.Equal("Left,Right", TestQuery.Infomation);

        }

        [Fact]
        public async void Archetype()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Performapal Five-Rainbow Magician", false);
            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.ArchType);

            Assert.Equal("Magician,Performapal", TestQuery.Infomation);
        }

        [Fact]
        public async void Name_Using_Url()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("http://yugioh.wikia.com/wiki/Trishula,_Dragon_of_the_Ice_Barrier", true);
            var TestQuery = Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Name_en);
            Assert.Equal("Trishula, Dragon of the Ice Barrier", TestQuery.Infomation);

        }

        [Fact]
        public async void Scrape_Normal_Monster()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Stargrail-Bearing Priestess", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Crowned by the World Chalice", Normal_Monster.Name_English);
            Assert.Equal("星杯を戴く巫女", Normal_Monster.Name_Japanese);
            Assert.Equal("Monster", Normal_Monster.Card_Type_Text);
            Assert.Equal("WATER", Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Spellcaster", "Normal" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(0, Normal_Monster.Effect_type_list.Count());
            Assert.Equal(2, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(0, Normal_Monster.Attack);
            Assert.Equal(2100, Normal_Monster.Defence);
            Assert.Equal(95511642, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "World Chalice" }, Normal_Monster.Archtype_List);
            Assert.Equal(null, Normal_Monster.Matieral);

        }

        [Fact]
        public async void Scrape_Effect_Monster()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Horus the Black Flame Dragon LV8", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Horus the Black Flame Dragon LV8", Normal_Monster.Name_English);
            Assert.Equal("ホルスの黒炎竜 ＬＶ８", Normal_Monster.Name_Japanese);
            Assert.Equal("Monster", Normal_Monster.Card_Type_Text);
            Assert.Equal("FIRE", Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Dragon", "Effect" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Summon", "Summon", "Quick", "Condition" }, Normal_Monster.Effect_type_list);
            Assert.Equal(8, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(null, Normal_Monster.Scale);

            Assert.Equal(3000, Normal_Monster.Attack);
            Assert.Equal(1800, Normal_Monster.Defence);
            Assert.Equal(48229808, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "Horus the Black Flame Dragon", "LV", "Dark counterpart" }, Normal_Monster.Archtype_List);
            Assert.Equal(null, Normal_Monster.Matieral);
        }


        [Fact]
        public async void Scrape_Pendulum_Monster()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Performapal Five-Rainbow Magician", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Performapal Five-Rainbow Magician", Normal_Monster.Name_English);
            Assert.Equal("ＥＭ五虹の魔術師", Normal_Monster.Name_Japanese);
            Assert.Equal("Monster", Normal_Monster.Card_Type_Text);
            Assert.Equal("LIGHT", Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Spellcaster", "Pendulum", "Effect" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Pendulum Effect", "Continuous-like", "Condition", "Condition", "Continuous-like", "Continuous-like", "Monster Effect", "Trigger" },
             Normal_Monster.Effect_type_list.ToArray());
            Assert.Equal(1, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(12, Normal_Monster.Scale);
            Assert.Equal(100, Normal_Monster.Attack);
            Assert.Equal(100, Normal_Monster.Defence);
            Assert.Equal(19619755, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "Magician", "Performapal" }, Normal_Monster.Archtype_List);
            Assert.Equal(null, Normal_Monster.Matieral);
        }


        [Fact]
        public async void Scrape_Link_Monster()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Imduk the World Chalice Dragon", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Imduk the World Chalice Dragon", Normal_Monster.Name_English);
            Assert.Equal("星杯竜イムドゥーク", Normal_Monster.Name_Japanese);
            Assert.Equal("Monster", Normal_Monster.Card_Type_Text);
            Assert.Equal("WIND", Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Dragon", "Link", "Effect" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Continuous", "Trigger", "Trigger" }, Normal_Monster.Effect_type_list);
            Assert.Equal(null, Normal_Monster.Scale);
            Assert.Equal(1, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(800, Normal_Monster.Attack);
            Assert.Equal(null, Normal_Monster.Defence);
            Assert.Equal(31226177, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "World Chalice" }, Normal_Monster.Archtype_List);
            Assert.Equal("1 Normal Monster, except a Token", Normal_Monster.Matieral);

        }

        [Fact]
        public async void Scrape_Trap()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Battle_Break", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Battle Break", Normal_Monster.Name_English);
            Assert.Equal("バトル・ブレイク", Normal_Monster.Name_Japanese);
            Assert.Equal("Trap", Normal_Monster.Card_Type_Text);
            Assert.Equal(null, Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Normal" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Activation requirement", "Effect" }, Normal_Monster.Effect_type_list);
            Assert.Equal(null, Normal_Monster.Scale);
            Assert.Equal(0, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(null, Normal_Monster.Attack);
            Assert.Equal(null, Normal_Monster.Defence);
            Assert.Equal(22047978, Normal_Monster.Passcode);
            Assert.Equal(0, Normal_Monster.Archtype_List.Count());
            Assert.Equal(null, Normal_Monster.Matieral);

        }


        [Fact]
        public async void Scrape_Spell()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Against the Wind", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Against the Wind", Normal_Monster.Name_English);
            Assert.Equal("アゲインスト・ウィンド", Normal_Monster.Name_Japanese);
            Assert.Equal("Spell", Normal_Monster.Card_Type_Text);
            Assert.Equal(null, Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Normal" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Effect" }, Normal_Monster.Effect_type_list);
            Assert.Equal(null, Normal_Monster.Scale);
            Assert.Equal(0, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(null, Normal_Monster.Attack);
            Assert.Equal(null, Normal_Monster.Defence);
            Assert.Equal(64952266, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "Blackwing" }, Normal_Monster.Archtype_List);
            Assert.Equal(null, Normal_Monster.Matieral);

        }


        [Fact]
        public async void Scrape_Spell2()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("U.A. Powered Jersey", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("U.A. Powered Jersey", Normal_Monster.Name_English);
            Assert.Equal("Ｕ．Ａ．パワードギプス", Normal_Monster.Name_Japanese);
            Assert.Equal("Spell", Normal_Monster.Card_Type_Text);
            Assert.Equal(null, Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Equip" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Condition", "Continuous-like", "Trigger-like", "Trigger-like", "Trigger-like" }, Normal_Monster.Effect_type_list);
            Assert.Equal(null, Normal_Monster.Scale);
            Assert.Equal(0, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(null, Normal_Monster.Attack);
            Assert.Equal(null, Normal_Monster.Defence);
            Assert.Equal(35884610, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "U.A." }, Normal_Monster.Archtype_List);
            Assert.Equal(null, Normal_Monster.Matieral);

        }


        [Fact]
        public async void Scrape_Fusion_With_Question_Mark_Monster()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("Chimeratech Overdragon", false);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Assert.Equal("Chimeratech Overdragon", Normal_Monster.Name_English);
            Assert.Equal("キメラテック・オーバー・ドラゴン", Normal_Monster.Name_Japanese);
            Assert.Equal("Monster", Normal_Monster.Card_Type_Text);
            Assert.Equal("DARK", Normal_Monster.Attribute);
            Assert.Equal(new string[] { "Machine", "Fusion", "Effect" }, Normal_Monster.Attribute_Type_List);
            Assert.Equal(new string[] { "Summon", "Trigger", "Continuous", "Continuous" }, Normal_Monster.Effect_type_list);
            Assert.Equal(null, Normal_Monster.Scale);
            Assert.Equal(9, Normal_Monster.Level_Rank_Or_Link);
            Assert.Equal(null, Normal_Monster.Attack);
            Assert.Equal(null, Normal_Monster.Defence);
            Assert.Equal(64599569, Normal_Monster.Passcode);
            Assert.Equal(new string[] { "Chimeratech", "Cyber Dragon" }, Normal_Monster.Archtype_List);
            Assert.Equal("\"Cyber Dragon\" + 1+ Machine monsters", Normal_Monster.Matieral);
        }


        [Fact]
        public async void insert_Card_Into_Database()
        {
            
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("http://yugioh.wikia.com/wiki/Dark_Magician_(Arkana)", true);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
             Normal_Monster.insert_Into_Wiki_Database(testDatabase);
             
            using (var Card_Context = new Card_Context(testDatabase))
            {
                Main_Card_Data Normal_Monster_From_Database_main_card_data = Card_Context.Main_Card_Data.First(x => x.passcode == Normal_Monster.Passcode);
                var Normal_Monster_From_Database_Archtype_List = Card_Context.Archtype_Table.Where(x => x.passcode == Normal_Monster.Passcode).Select(x => x.name).ToList();
				var Normal_Monster_From_Database_Link_Arrow_List  = Card_Context.link_arrow_Table.Where(x => x.passcode == Normal_Monster.Passcode).Select(x => x.name).ToList();
                var Normal_Monster_From_Database_Foreign_Name_List = Card_Context.Foreign_Name_Table.Where(x => x.passcode == Normal_Monster.Passcode).ToList();

                Assert.Equal(JsonConvert.SerializeObject(Normal_Monster.Get_main_card_data()), JsonConvert.SerializeObject(Normal_Monster_From_Database_main_card_data));
				Assert.Equal(Normal_Monster.Archtype_List.OrderBy(i => i),Normal_Monster_From_Database_Archtype_List.OrderBy(i => i));
				Assert.Equal(Normal_Monster.Link_Arrows.OrderBy(i => i), Normal_Monster_From_Database_Link_Arrow_List.OrderBy(i => i));
                Assert.Equal(Normal_Monster.Foreign_Name_Entrys.Select(i=> i.name).OrderBy(i => i), Normal_Monster_From_Database_Foreign_Name_List.Select(i => i.name).OrderBy(i => i));
            }
        }



        [Fact]
        public async void Scrape_Card()
        {
            await YuGiOh_Wiki_Search.Scrape_All_Cards(testDatabase);
        }

    }
}