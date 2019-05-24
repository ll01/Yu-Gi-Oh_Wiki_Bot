using System.Data;
namespace Yu_Gi_Oh_Wiki_Bot
{
	public class Ygo_Pro_Card_Database
	{
		private DataTable Datas;
		private DataTable Texts;

		public DataTable datas
		{
			get => Datas;
			set => Datas = value;
		}

		public DataTable texts
		{
			get => Texts;
			set => Texts = value;
		}
	}

	public class YuGiOh_Wiki_Database  
	{
		private DataTable  main;
		private DataTable  names;
		private DataTable  type_list;
		private DataTable  link_arrows;


		public DataTable Main { get => main  ; set => main = value;}
		public DataTable Names {get => names; set =>  names = value;}

		public DataTable Type_list {get => type_list; set =>  type_list = value;}
		public DataTable Link_arrows {get => link_arrows; set =>  link_arrows = value;}
 	}
}