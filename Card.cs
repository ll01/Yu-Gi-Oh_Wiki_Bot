using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Ygo_Deck_Helper
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




        private List<string> m_Link_Arrows = new List<string>();
        public List<string> Link_Arrows { get => m_Link_Arrows; set => m_Link_Arrows = value; }
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
            Foreign_Name_Entrys.ForEach(x => x.passcode = this.Passcode);

        }

        public int Get_Card_Type_Number()
        {
            return YGOPro_type_number;
        }

        public Main_Card_Data Get_main_card_data()
        {
            return main_Card_Data;
        }



        public void Add_Nullable_Parameter(SQLiteCommand Sql_Command, string Parameter_Name, object Value_To_Add)
        {
            if (Value_To_Add != null)
            {
                Sql_Command.Parameters.AddWithValue(Parameter_Name, Value_To_Add);
            }
            else
            {
                Sql_Command.Parameters.AddWithValue(Parameter_Name, DBNull.Value);
            }
        }
        public void insert_Into_Wiki_Database(Database database)
        {

            using (var card_database_context = new Card_Context(database))
            {
                if (card_database_context.Main_Card_Data.All(x => x.passcode != this.Passcode))
                {
                    var Link_Arrows_To_Insert = Link_Arrows.Select(x =>
                    {
                        if (card_database_context.link_arrow_Table.All(y => y.link_arrow != x && y.passcode != this.Passcode))
                        {
                            var temp = new link_arrow_Table();
                            temp.link_arrow = x;
                            temp.passcode = this.Passcode;
                            return temp;
                        }
                        else
                        {
                            return null;
                        }

                    }).Where(x => x != null);

                    var Effect_Types_To_Insert = Effect_type_list.Select(x =>
                    {
                        if (card_database_context.Effect_keyword_Table.All(y => y.effect_name != x && y.passcode != this.Passcode))
                        {
                            var temp = new Effect_keyword_Table();
                            temp.effect_name = x;
                            temp.passcode = this.Passcode;
                            return temp;
                        }
                        else
                        {
                            return null;
                        }

                    }).Where(x => x != null);

                    var Archtypes_To_Insert = Archtype_List.Select(x =>
                    {
                        if (card_database_context.Archtype_Table.All(y => y.archtype_name != x && y.passcode != this.Passcode))
                        {
                            var temp = new Archtype_Table();
                            temp.archtype_name = x;
                            temp.passcode = this.Passcode;
                            return temp;
                        }
                        else
                        {
                            return null;
                        }
                    }).Where(x => x != null);

                    var Attribute_Types_To_Insert = Attribute_Type_List.Select(x =>
                    {
                        if (card_database_context.Attribute_Table.All(y => y.Attribute_Name != x && y.passcode != this.Passcode))
                        {
                            var temp = new Attribute_Table();
                            temp.Attribute_Name = x;
                            temp.passcode = this.Passcode;
                            return temp;
                        }
                        else
                        {
                            return null;
                        }
                    }).Where(x => x != null);


                    //http://www.hexacta.com/2016/06/01/task-run-vs-async-await/
                    //TODO: WRITE TABLE INSERTION   


                    card_database_context.Archtype_Table.AddRange(Archtypes_To_Insert);
                    card_database_context.link_arrow_Table.AddRange(Link_Arrows_To_Insert);
                    card_database_context.Effect_keyword_Table.AddRange(Effect_Types_To_Insert);
                    card_database_context.Attribute_Table.AddRange(Attribute_Types_To_Insert);
                    card_database_context.Main_Card_Data.AddRange(main_Card_Data);
                    card_database_context.Foreign_Name_Table.AddRange(Foreign_Name_Entrys);
                    card_database_context.SaveChanges();
                }
            }

        }
    }
}