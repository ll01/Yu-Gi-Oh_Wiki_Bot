using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Ygo_Deck_Helper
{
	internal class Deck
	{
		private string Deck_Name;
		private List<Card> main_deck;
		private List<Card> extra_deck;
		private List<Card> side_deck;
		public Dictionary<string, int> Faild_Search_Cards = new Dictionary<string, int>();
		public List<Card> Overflow_Cards = new List<Card>();

		static public int Main_Max_Card_Count = 60;
		static public int Extra_Max_Card_Count = 15;
		static public int Side_Max_Card_Count = 15;
		private static readonly int[] Main_Deck_Type_ids = new int[] { /*Main Deck Monster*/ 17, 33, 129, 161, 1057, 2081, 4113, 4129, 2097185, /*Spell*/ 2, 130, 65538, 131074, 262146, 524290, /*Trap*/ 4, 131076, 1048580 };
		private static readonly int[] Extra_Deck_Type_ids = new int[] { /*Fusion*/ 65, 97, /*Syncro*/ 8193, 8225, 12321, /*xyz*/ 8388609, 8388641 };

		internal List<Card> Main_Deck
		{
			get
			{
				return main_deck;
			}

			set
			{
				main_deck = value;
			}
		}

		internal List<Card> Extra_deck
		{
			get
			{
				return extra_deck;
			}

			set
			{
				extra_deck = value;
			}
		}

		internal List<Card> Side_deck
		{
			get
			{
				return side_deck;
			}

			set
			{
				side_deck = value;
			}
		}

		/// <summary>
		/// creates new deck object
		/// </summary>
		/// <param name="Deck_Name">name of the new deck</param>
		public Deck(string Deck_Name)
		{
			this.Deck_Name = Deck_Name;
			main_deck = new List<Card>();
			extra_deck = new List<Card>();
			side_deck = new List<Card>();
		}

		/// <summary>
		/// Using a list of strings of card names to build a deck via the card database
		/// </summary>
		/// <param name="List_Of_Cards">list of card names</param>
		/// <returns></returns>
		public async static Task<Deck> Create_Deck(List<string> List_Of_Main_Extra_Cards, List<string> list_of_Side_Cards, string Deck_Name)
		{
			Deck New_Deck = new Deck(Deck_Name);

			New_Deck = await Query_List_Of_Cards(List_Of_Main_Extra_Cards, New_Deck, false);

			New_Deck = await Query_List_Of_Cards(list_of_Side_Cards, New_Deck, true);
			return New_Deck;
		}

		public static async Task<Deck> Query_List_Of_Cards(List<string> Card_Name_List, Deck New_Deck, bool isSide)
		{
			foreach (string Card_Name in Card_Name_List)
			{
				var New_Card_Query = Deck.Query_Single_Card(Card_Name);
				if (New_Card_Query.isSucsessful == false)
				{
					New_Deck.Add_To_Deck(New_Card_Query.New_Card, isSide);
				}
				else
				{
					var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search(Card_Name, false);

					var Wikia_Card_Number_Query = await Wiki_Search.Scrape_Card_Field_Infomation(YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.Passcode);
					if (Wikia_Card_Number_Query.isSucsesful == true)
					{
						await Task.Run(() => { Card New_Card = Deck.Query_Single_Card(Wikia_Card_Number_Query.Infomation).New_Card;
						New_Deck.Add_To_Deck(New_Card, isSide);
						});
					}
					else
					{
						if (New_Deck.Faild_Search_Cards.ContainsKey(Card_Name) == false)
							New_Deck.Faild_Search_Cards.Add(Card_Name, 1);
						else
							New_Deck.Faild_Search_Cards[Card_Name]++;
					}
				}
			}
			return New_Deck;
		}

		/// <summary>
		/// serches for a card using the sql database
		/// </summary>
		/// <param name="Card_Name">the card name of the card to search for</param>
		/// <returns>a kevalue pair where the key is the card to be searched and the value is wether the search was sucsessful</returns>
		public static (Card New_Card, bool isSucsessful) Query_Single_Card(string Card_Name)
		{
			Card New_Card;
			string Get_id_Command;
			string Get_Type_Command;
			bool Faild_Search;

			try
			{
				Get_id_Command = "name = '" + Card_Name + "'";
				DataRow[] Searching_Card_Texts = Sqlite_Functions.ygoPro_Card_Database.texts.Select(Get_id_Command);
				var id_Object = Searching_Card_Texts[0][0];
				int id = Convert.ToInt32(id_Object);
				

				Get_Type_Command = "id = '" + id + "'";
				DataRow[] Searching_Card_datas = Sqlite_Functions.ygoPro_Card_Database.datas.Select(Get_Type_Command);
				var Type_Number_Object = Searching_Card_datas[0][4];
				int Type_Number = Convert.ToInt32(Type_Number_Object);


				New_Card = new Card(id, Type_Number, Card_Name);
				Faild_Search = false;
			}
			catch (IndexOutOfRangeException e)
			{
				Faild_Search = true;
				New_Card = null;
				Console.WriteLine(e.Message);
			}
			return (New_Card, Faild_Search);
		}

		/// <summary>
		/// erches for a card using the sql database
		/// </summary>
		/// <param name="Card_Id">the card id of the card to search for</param>
		/// <returns>a kevalue pair where the key is the card to be searched and the value is wether the search was sucsessful</returns>
		public static KeyValuePair<Card, bool> Query_Single_Card(int Card_Id)
		{
			Card New_Card;
			string Get_Type_Command;
			bool Faild_Search;

			try
			{
				Get_Type_Command = "id = '" + Card_Id + "'";
				DataRow[] Searching_Card_datas = Sqlite_Functions.ygoPro_Card_Database.datas.Select(Get_Type_Command);
				DataRow[] Searching_Card_Texts = Sqlite_Functions.ygoPro_Card_Database.texts.Select(Get_Type_Command);
				var Type_Number_Object = Searching_Card_datas[0][4];
				int Type_Number = Convert.ToInt32(Type_Number_Object);
				string Card_Name = Searching_Card_Texts[0][1].ToString();

				New_Card = new Card(Card_Id, Type_Number, Card_Name);
				Faild_Search = false;
			}
			catch (IndexOutOfRangeException e)
			{
				Faild_Search = true;
				New_Card = null;
				Console.WriteLine(e.Message);
			}
			return new KeyValuePair<Card, bool>(New_Card, Faild_Search);
		}

		/// <summary>
		/// add a card object to a deck object
		/// </summary>
		/// <param name="Card_Added"></param>
		public void Add_To_Deck(Card Card_Added, bool isSide)
		{
			if (isSide == false)
			{
				// fix numbers
				int[] Main_Deck_Type_ids = new int[] { /*Main Deck Monster*/ 17, 33, 129, 161, 1057, 2081, 4113, 4129, 2097185, /*Pendulum*/ 16777233, 16777249, 16781313, 16785409, 16777281, /*Spell*/ 2, 130, 65538, 131074, 262146, 524290, /*Trap*/ 4, 131076, 1048580 };
				int[] Extra_Deck_Type_ids = new int[] { /*Fusion*/ 65, 97, /*Syncro*/ 8193, 8225, 12321, /*xyz*/ 8388609, 8388641 };

				if (Main_Deck_Type_ids.Contains(Card_Added.Get_Card_Type_Number()))
					Main_Deck.Add(Card_Added);
				if (Extra_Deck_Type_ids.Contains(Card_Added.Get_Card_Type_Number()))
					extra_deck.Add(Card_Added);
			}
			else
			{
				this.side_deck.Add(Card_Added);
			}
		}

		/// <summary>
		/// writes out the deck in to a text format to be recognised by ygo pro
		/// </summary>
		/// <param name="File_Name">the path where the deck should be saved </param>
		/// <returns>wehether or not the deck was successfuly saved </returns>
		public bool Write_deck(string File_Name)
		{
			TextWriter Text_Out = null;
			try
			{
				Text_Out = new StreamWriter(File_Name);
				Text_Out.WriteLine("#Main");
				foreach (Card Card_To_write in main_deck)
				{
					Text_Out.WriteLine(Card_To_write.Passcode);
				}
				Text_Out.WriteLine("#Extra");
				foreach (Card Card_To_write in extra_deck)
				{
					Text_Out.WriteLine(Card_To_write.Passcode);
				}
				Text_Out.WriteLine("!Side");
				foreach (Card Card_To_write in side_deck)
				{
					Text_Out.WriteLine(Card_To_write.Passcode);
				}
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				Text_Out.Close();
			}
		}

		//public async void Search_faild_Search_Cards(string Card_N)
		//{
		//    foreach
		//}
	}
}