using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Yu_Gi_Oh_Wiki_Bot
{
	internal class Data_Check
	{
		public struct Format_Settings
		{
			public string Card_Database_Filepath;
			public string Deck_Save_Path;
			public bool isReversed;

			/// <summary>
			/// creates a new format settings object
			/// </summary>
			/// <param name="New_Card_Database_Filepath">database file path</param>
			/// <param name="New_Deck_Save_Path">deck to saves file path</param>
			/// <param name="isReversed">whether the card number has been reversed</param>
			public Format_Settings(string New_Card_Database_Filepath, string New_Deck_Save_Path, bool isReversed)
			{
				this.Card_Database_Filepath = New_Card_Database_Filepath;
				this.Deck_Save_Path = New_Deck_Save_Path; ;
				this.isReversed = isReversed;
			}
		}

		/// <summary>
		/// takes a list of card names and takes out the card frequency number and then add that card equal to the frequency
		/// </summary>
		/// <param name="Raw_List">List to be parsed </param>
		/// <returns></returns>
		public static List<string> card_frequency_parse(List<string> Raw_List, bool isReversed)
		{
			List<string> Parsed_List = new List<string>();
			int Card_Count;

			foreach (string sr in Raw_List)
			{
				if (sr.Count() == 0)
				{
					//Raw_List.RemoveAll(x => x == sr);
				}
				else
				{
					// TODO: you only need the first 2 characters regardless of mCard_Frequency_Format. re-figure the logic to take advantage of this.
					string New_Card_Text = sr;

					// key is the name of the card and the value is the potential frequency (defaults to 1 if no frequency is found)
					var Frequency_Name_Data = Extract_Frequency(New_Card_Text, isReversed);
					New_Card_Text = Frequency_Name_Data.Name;
					Card_Count = Frequency_Name_Data.potential_Frequency;

					New_Card_Text = Punctuation_Check(New_Card_Text);
					//remove line break
					New_Card_Text = Regex.Replace(New_Card_Text, "\r\n", "");
					for (int i = 0; i < Card_Count; i++)
						Parsed_List.Add(New_Card_Text);
				}
			}
			return Parsed_List;
		}

		/// <summary>
		/// takes a string that contains a card name and a frequency and extracts the name and frequency
		/// </summary>
		/// <param name="Card_Text"> string that contains the frequency and the card name </param>
		/// <param name="isReversed">bool that signifies whether the frequency is at the start or the end of the string </param>
		/// <returns>a key value par where the key is the name of the card and the value is the potential frequency </returns>
		private static (string Name, int potential_Frequency) Extract_Frequency(string Card_Text, bool isReversed)
		{
			int removeammount = 2;
			string Frequency_Data_Unformated;
			int Card_Frequency;

			if (isReversed == false)
				Frequency_Data_Unformated = Card_Text.Substring(0, removeammount);
			else
				Frequency_Data_Unformated = Card_Text.Substring(Card_Text.Length - removeammount, removeammount);
			var RegMach = Regex.Match(Frequency_Data_Unformated, "[0-9]");
			if (RegMach.Success)
			{
				string Frequency_Data_formated = Regex.Replace(Frequency_Data_Unformated.Trim(), "[^0-9]", "");
				Card_Frequency = int.Parse(Frequency_Data_formated);
				Card_Text = Regex.Replace(Card_Text, Frequency_Data_Unformated, "").Trim();
			}
			else
			{
				Card_Frequency = 1;
			}
			return (Card_Text, Card_Frequency);
		}

		/// <summary>
		/// gets rid of the line break from the raw data in the text box
		/// </summary>
		/// <param name="sr">string to remove the line break</param>
		/// <returns>string with no line break</returns>
		internal static string Remove_Line_Break(string sr)
		{
			sr = Regex.Replace(sr, "\r\n", "");
			return sr;
		}

		/// <summary>
		/// tries to filter out special punctuation from card names and replaces them with database friendly ones
		/// </summary>
		/// <param name="sr">card name to be checked </param>
		/// <returns></returns>
		internal static string Punctuation_Check(string sr)
		{
			string[] Speach_Mark_Filter = new string[2] { "“", "”", };
			string Apostrophy = "’";

			string Checked_Name = sr;

			foreach (string Filter in Speach_Mark_Filter)
			{
				if (Checked_Name.Contains(Filter))
				{
					Checked_Name = Regex.Replace(Checked_Name, Filter, "\"");
				}
			}
			Checked_Name = Regex.Replace(Checked_Name, Apostrophy, "'");
			Checked_Name = Regex.Replace(Checked_Name, "'", "\''");

			return Checked_Name;
		}

		/// <summary>
		/// separates the raw list in to the side deck and the cards to be imputed into the extra and main deck
		/// </summary>
		/// <param name="Raw_List">list of card name strings </param>
		/// <returns> a keyvaluepair key = extra and main deck names value = side deck names  </returns>
		internal static (List<string> Extra_Main_List, List<string> Side_List) Deck_Header_Parse(List<string> Raw_List)
		{
			List<String> Side_Deck_cards = new List<string>();
			string[] Main_Extra_Deck_Header_Filter = new string[] { "MAIN", "main", "#main", "Main", "Main Deck,", "main deck", "MAIN DECK", "EXTRA", "extra", "#extra", "Extra", "Extra Deck", "extra deck", "EXTRA DECK" };
			//string[] Extra_Deck_Header_Filter = new string[] { "EXTRA", "extra", "#extra", "Extra", "Extra Deck", "extra deck", "EXTRA DECK" };
			string[] Side_Deck_Header_Filter = new string[] { "SIDE", "side", "!side", "Side", "Side Deck", "side deck", "SIDE DECK", "SIDE-DECK" };

			foreach (string Filter in Main_Extra_Deck_Header_Filter)
			{
				Raw_List.RemoveAll(header => header == Filter);
			}

			foreach (string Side_Deck_Header in Side_Deck_Header_Filter)
			{
				if (Raw_List.Contains(Side_Deck_Header))
				{
					int Side_Deck_Index = Raw_List.FindIndex(x => x == Side_Deck_Header);
					for (int i = Side_Deck_Index + 1; i < Raw_List.Count; i++)
					{
						Side_Deck_cards.Add(Raw_List[i]);
					}

					Raw_List.RemoveRange(Side_Deck_Index, Raw_List.Count - Side_Deck_Index);
				}
			}
			return (Raw_List, Side_Deck_cards);
		}

		/// <summary>
		/// checks wether the main deck has a max of 60 card and the extra/side deck has 15 (this method trunkates the deck to size)
		/// </summary>
		// /// <param name="Deck_To_Check">the deck to check </param>
		// internal static void Deck_Card_Count_check(Deck Deck_To_Check)
		// {
		// 	const int Main_Max_Card_Count = 60;
		// 	const int Extra_Max_Card_Count = 15;
		// 	const int Side_Max_Card_Count = 15;
		// 	if (Deck_To_Check.Main_Deck.Count > Main_Max_Card_Count)
		// 	{
		// 		int Amount_To_Remove = Deck_To_Check.Main_Deck.Count - Main_Max_Card_Count;
		// 		for (int i = Main_Max_Card_Count; i < Deck_To_Check.Main_Deck.Count; i++)
		// 		{
		// 			if (Deck_To_Check.Overflow_Cards.Contains(Deck_To_Check.Main_Deck[i]) == false)
		// 				Deck_To_Check.Overflow_Cards.Add(Deck_To_Check.Main_Deck[i]);
		// 		}

		// 		Deck_To_Check.Main_Deck.RemoveRange(Main_Max_Card_Count, Amount_To_Remove);
		// 	}

		// 	if (Deck_To_Check.Extra_deck.Count > Extra_Max_Card_Count)
		// 	{
		// 		int Amount_To_Remove = Deck_To_Check.Extra_deck.Count - Extra_Max_Card_Count;
		// 		for (int i = Extra_Max_Card_Count; i < Deck_To_Check.Extra_deck.Count; i++)
		// 		{
		// 			if (Deck_To_Check.Overflow_Cards.Contains(Deck_To_Check.Extra_deck[i]) == false)
		// 				Deck_To_Check.Overflow_Cards.Add(Deck_To_Check.Extra_deck[i]);
		// 		}

		// 		Deck_To_Check.Extra_deck.RemoveRange(Extra_Max_Card_Count, Amount_To_Remove);
		// 	}

		// 	if (Deck_To_Check.Side_deck.Count > Side_Max_Card_Count)
		// 	{
		// 		int Amount_To_Remove = Deck_To_Check.Side_deck.Count - Side_Max_Card_Count;
		// 		for (int i = Side_Max_Card_Count; i < Deck_To_Check.Side_deck.Count; i++)
		// 		{
		// 			if (Deck_To_Check.Overflow_Cards.Contains(Deck_To_Check.Side_deck[i]) == false)
		// 				Deck_To_Check.Overflow_Cards.Add(Deck_To_Check.Side_deck[i]);
		// 		}

		// 		Deck_To_Check.Side_deck.RemoveRange(Side_Max_Card_Count, Amount_To_Remove);
		// 	}
		// }

		/// <summary>
		/// remove weird characters from the card names
		/// </summary>
		/// <param name="Card_Names">list of card names to check</param>
		/// <param name="Current_Settings">the name settings to check by</param>
		/// <returns>a kevalue pair where the key is the list of main and extra deck card names and the value is the list of side deck names</returns>
		public static (List<string> Main_Extra_Card_List, List<String> Side_Card_List) Clense_Card_Names(List<string> Card_Names, Format_Settings Current_Settings)
		{
			var Unseperated_Card_Names = Deck_Header_Parse(Card_Names);
			List<string> Main_Extra_Card_Names = Unseperated_Card_Names.Extra_Main_List;
			List<string> Side_Card_Names = Unseperated_Card_Names.Side_List;

			Main_Extra_Card_Names = Data_Check.card_frequency_parse(Main_Extra_Card_Names, Current_Settings.isReversed);
			Side_Card_Names = Data_Check.card_frequency_parse(Side_Card_Names, Current_Settings.isReversed);

			return (Main_Extra_Card_Names, Side_Card_Names);
		}

		/// <summary>
		/// reformats the card names from ygo wiki to ygo database format
		/// </summary>
		/// <param name="Card_Name">the name of the card to check</param>
		/// <returns>the new card name formated to ygo database format</returns>
		public static string Format_Card_Name_For_Wiki_Search(string Card_Name)
		{
			string Space = " ";
			string Speach = "''";
			string Hyphen = "–";
			if (Card_Name.Contains(Space))
			{
				Card_Name = Regex.Replace(Card_Name, Space, "_");
			}
			if (Card_Name.Contains(Speach))
			{
				Card_Name = Regex.Replace(Card_Name, Speach, "'");
			}

			if (Card_Name.Contains(Hyphen))
			{
				Card_Name = Regex.Replace(Card_Name, Hyphen, "-");
			}
			return Card_Name;
		}

		static public string Clense_YuGiOh_Wiki_Search_Infomation(string Raw_Data, YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field Data_Field)
		{
			string Return_Object = null;
			switch (Data_Field)
			{
				case YuGiOh_Wiki_Search.YuGiOh_Wiki_Data_Field.ArchType:

					break;

				default:
					Return_Object = Raw_Data;
					break;
			}
			return Raw_Data;
		}

		static public List<String> Split_Card_Effect_List(string Raw_List)
		{
			return Regex.Split(Raw_List, "\\n ").Select(x => x.Trim()).ToList();
		}
	}
}