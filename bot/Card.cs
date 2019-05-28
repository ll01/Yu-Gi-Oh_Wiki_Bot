using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yu_Gi_Oh_Wiki_Bot
{

    public class Card
    {

        private Main_Card_Data main_Card_Data;
        private int YGOPro_type_number;

        public int Passcode { get => main_Card_Data.passcode; set => main_Card_Data.passcode = value; }
        public string Name_English { get => main_Card_Data.name_en; set => main_Card_Data.name_en = value; }
        public string Name_Japanese { get => main_Card_Data.name_jp; set => main_Card_Data.name_jp = value; }
        public string Card_Type_Text { get => main_Card_Data.card_type; set => main_Card_Data.card_type = value; }
        public string Attribute { get => main_Card_Data.attribute; set => main_Card_Data.attribute = value; }
        public string Matieral { get => main_Card_Data.material; set => main_Card_Data.material = value; }
        public int Level_Rank_Or_Link { get => main_Card_Data.level_or_rank; set => main_Card_Data.level_or_rank = value; }
        public int? Scale { get => main_Card_Data.scale; set => main_Card_Data.scale = value; }
        public int? Attack { get => main_Card_Data.attack; set => main_Card_Data.attack = value; }
        public int? Defence { get => main_Card_Data.defence; set => main_Card_Data.defence = value; }

        private List<string> m_link_arrow_list = new List<string>();
        public List<string> Link_Arrows { get => m_link_arrow_list; set => m_link_arrow_list = value; }
        private List<string> m_effect_type_list = new List<string>();
        public List<string> Effect_type_list { get => m_effect_type_list; set => m_effect_type_list = value; }
        private List<string> m_Archtype_List = new List<string>();
        public List<string> Archtype_List { get => m_Archtype_List; set => m_Archtype_List = value; }


        private List<string> m_Attribute_Type_List = new List<string>();
        public List<string> Attribute_Type_List { get => m_Attribute_Type_List; set => m_Attribute_Type_List = value; }

        public List<Foreign_Name_Table> Foreign_Name_Entrys = new List<Foreign_Name_Table>();

        public Card(int Passcode, int Type_Number, string Card_Name)
        {
            main_Card_Data = new Main_Card_Data();
            this.Passcode = Passcode;
            YGOPro_type_number = Type_Number;
            this.Name_English = Card_Name;
        }

        public Card(int Passcode, string Card_Name_English, string Card_Name_Japanese, string Card_Type, string Attribute,
        string Level_Or_Rank_As_String, string Scale_As_String, string Attack_As_String, string defence_Link_As_String, string Matieral,
         List<String> effect_type_list, List<string> Type_List, List<string> Archtype_List, List<Foreign_Name_Table> Foreign_Names)
        {
            main_Card_Data = new Main_Card_Data();
            this.Passcode = Passcode;
            this.Name_English = Card_Name_English;
            this.Name_Japanese = Card_Name_Japanese;
            this.Card_Type_Text = Card_Type;

            //new collections 


            this.m_Attribute_Type_List = Type_List;
            this.m_effect_type_list = effect_type_list;
            if (Archtype_List != null)
                this.m_Archtype_List = Archtype_List.Distinct().ToList();
            if (Card_Type == "Monster")
            {
                if (m_Attribute_Type_List.Contains("Link"))
                {
                    this.Level_Rank_Or_Link = int.Parse(defence_Link_As_String);
                    this.Defence = null;
                }
                else
                {
                    this.Level_Rank_Or_Link = int.Parse(Level_Or_Rank_As_String);
                    // Console.WriteLine(defence_Link_As_String);
                    this.Defence = defence_Link_As_String.Contains("?") ? null : (int?)int.Parse(defence_Link_As_String);

                }
                this.Attack = Attack_As_String.Contains("?") ? null : (int?)int.Parse(Attack_As_String);
                //int.TryParse(Attack_As_String, out (int)this.m_Attack))
                this.Matieral = Matieral;


                this.Attribute = Attribute;
                if (this.m_Attribute_Type_List.Contains("Pendulum"))
                    this.Scale = int.Parse(Scale_As_String);
            }

            Foreign_Name_Entrys = Foreign_Names;
            // Foreign_Name_Entrys.ForEach(x => x.passcode = this.Passcode);

        }

        public int Get_Card_Type_Number()
        {
            return YGOPro_type_number;
        }

        public Main_Card_Data Get_main_card_data()
        {
            return main_Card_Data;
        }

        public void insert_Into_Wiki_Database(Database database)
        {

            using (var card_database_context = new Card_Context(database))
            {
                var effects = new List<Effect_keyword_Main_Table>();
                foreach (var effect in m_effect_type_list)
                {
                    var one = new Effect_keyword_Table { name = effect };
                    card_database_context.Add(new Effect_keyword_Main_Table
                    {
                        Effect_keyword_Table = one,
                        Main_Card_Data = main_Card_Data
                    });
                }
                Console.WriteLine(card_database_context.Effect_keyword_Table.Count());
                Console.WriteLine(card_database_context.Main_Card_Data.Count());
            }
            

        }
    }
}