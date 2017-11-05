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
        private List<string> m_Link_Arrows;
        public List<string> Link_Arrows { get => m_Link_Arrows; set => m_Link_Arrows = value; }
        private List<string> effect_type_list;
        public List<string> Effect_type_list { get => effect_type_list; set => effect_type_list = value; }
        private List<string> m_Archtype_List;
        public List<string> Archtype_List { get => m_Archtype_List; set => m_Archtype_List = value; }


        private List<string> m_Attribute_Type_List;
        public List<string> Attribute_Type_List { get => m_Attribute_Type_List; set => m_Attribute_Type_List = value; }


        public Card(int Passcode, int Type_Number, string Card_Name)
        {
            main_Card_Data = new Main_Card_Data();
            this.Passcode = Passcode;
            YGOPro_type_number = Type_Number;
            this.Name_English = Card_Name;
        }

        public Card(int Passcode, string Card_Name_English, string Card_Name_Japanese, string Card_Type, string Attribute,
        string Level_Or_Rank_As_String, string Scale_As_String, string Attack_As_String, string defence_Link_As_String, string Matieral,
         List<String> effect_type_list, List<string> Type_List, List<string> Archtype_List)
        {
            main_Card_Data = new Main_Card_Data();
            this.Passcode = Passcode;
            this.Name_English = Card_Name_English;
            this.Name_Japanese = Card_Name_Japanese;
            this.Card_Type_Text = Card_Type;

            this.m_Attribute_Type_List = Type_List;
            this.effect_type_list = effect_type_list;
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

        }

        public int Get_Card_Type_Number()
        {
            return YGOPro_type_number;
        }

        public Main_Card_Data Get_main_card_data() {
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
        public async Task insert_Into_Wiki_Database()
        {
            // 	var SqlConnection = new SQLiteConnection(@"Data Source = data.db");
            // 	SqlConnection.Open();
            // 	var Sql_Transaction = SqlConnection.BeginTransaction();
            // 	var Sql_Command = new SQLiteCommand();
            // 	Sql_Command.Connection = SqlConnection;

            // 	try
            // 	{

            // 		Sql_Command.Parameters.AddWithValue("@passcode", this.Passcode);
            // 		Sql_Command.Parameters.AddWithValue("@name_en", this.Card_Name_English);
            // 		if(this.m_Name_Japanese == null)
            // 		this.m_Name_Japanese = "N/A";
            // 		Sql_Command.Parameters.AddWithValue("@name_jp", this.m_Name_Japanese);
            // 		Sql_Command.Parameters.AddWithValue("@card_type", this.Card_Type_Text);

            // 		List<Task> Sub_Query_List = new List<Task>();

            // 		Sql_Command.Parameters.Add("@archtype_name", DbType.String);
            // 		foreach (string Archtype in Archtype_List ?? Enumerable.Empty<string>())
            // 		{
            // 			Sql_Command.Parameters["@archtype_name"].Value = Archtype;
            // 			Sql_Command.CommandText = "INSERT INTO ygo_archtype(passcode, wiki_archtype_text) values (@passcode, @archtype_name)";
            // 			Sub_Query_List.Add( Sql_Command.ExecuteNonQueryAsync());
            // 		}

            // 		if (Card_Type_Text == "Monster")
            // 		{
            // 			Sql_Command.Parameters.AddWithValue("@attribute", this.Attribute);
            // 			Sql_Command.Parameters.AddWithValue("@level_rank_or_link", this.m_Level_Rank_Or_Link);

            // 			Add_Nullable_Parameter(Sql_Command, "@scale", this.Scale);

            // 			Add_Nullable_Parameter(Sql_Command, "@attack", this.Attack);
            // 			Add_Nullable_Parameter(Sql_Command, "@defence", this.Defence);
            // 			Add_Nullable_Parameter(Sql_Command,"@material", this.Matieral);

            // 			Sql_Command.CommandText = @"INSERT INTO ygo_main(passcode, name_en, name_jp, card_type, attribute, level_or_rank, scale, attack, defence, material) 
            // 			values (@passcode, @name_en, @name_jp, @card_type, @attribute, @level_rank_or_link, @scale, @attack, @defence, @material)";
            // 			Task main_Query =   Sql_Command.ExecuteNonQueryAsync();

            // 			Sql_Command.Parameters.Add("@Attribute_Type", DbType.String);


            // 			foreach (string Type in this.Attribute_Type_List)
            // 			{
            // 				Sql_Command.Parameters["@Attribute_Type"].Value = Type;
            // 				Sql_Command.CommandText = "INSERT INTO ygo_type_list(passcode, type) values (@passcode, @Attribute_Type)";
            // 				Sub_Query_List.Add( Sql_Command.ExecuteNonQueryAsync());
            // 			}

            // 			Sql_Command.Parameters.Add("@Effect_Keyword", DbType.String);
            // 			foreach (string Effect in effect_type_list ?? Enumerable.Empty<string>())
            // 			{
            // 				Sql_Command.Parameters["@Effect_Keyword"].Value = Effect;
            // 				Sql_Command.CommandText = "INSERT INTO ygo_effect_keyword_list(passcode, effect_keyword) values (@passcode, @Effect_Keyword)";
            // 				Sub_Query_List.Add( Sql_Command.ExecuteNonQueryAsync());
            // 			}

            // 			Sql_Command.Parameters.Add("@Link_Arrow", DbType.String);

            // 			if (this.Attribute_Type_List.Contains("Link"))
            // 			{
            // 				foreach (string Link_Arrow_item in Link_Arrows)
            // 				{
            // 					Sql_Command.Parameters["@Link_Arrow"].Value = Link_Arrow_item;

            // 					Sql_Command.CommandText = "INSERT INTO ygo_link_arrows(passcode, link_arrows) values(@passcode, @Link_Arrow)";
            // 					Sub_Query_List.Add( Sql_Command.ExecuteNonQueryAsync());
            // 				}
            // 			}
            // 			await main_Query; 
            // 		}
            // 		else{
            // 			 Sql_Command.CommandText = @"INSERT INTO ygo_main(passcode, name_en, name_jp, card_type) 
            // 			values (@passcode, @name_en, @name_jp, @card_type)";
            // 			await Sql_Command.ExecuteNonQueryAsync();
            // 		}
            // 		await Task.WhenAll(Sub_Query_List.ToArray());
            // 		Sql_Transaction.Commit();
            // 	}
            // 	catch (Exception e)
            // 	{
            // 		Sql_Transaction.Rollback();
            // 		Console.WriteLine(e.ToString());
            // 		Console.WriteLine("record was not written to database.");


            // 	}
            // 	finally
            // 	{
            // 		SqlConnection.Close();
            // 	}

            using (var card_database_context = new Card_Context())
            {
                //http://www.hexacta.com/2016/06/01/task-run-vs-async-await/
               
                    card_database_context.Main_Card_Data.Add(main_Card_Data);

                Task a = card_database_context.AddRangeAsync(Archtype_List);
                Task b = card_database_context.AddRangeAsync(Link_Arrows);
                Task c = card_database_context.AddRangeAsync(Archtype_List);
                
            }
        

        }
    }
}