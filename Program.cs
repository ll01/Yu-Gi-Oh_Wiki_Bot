using System.Threading.Tasks;

namespace Ygo_Deck_Helper
{
	public class Program
	{
       static async Task Main(string[] args)
       {
           var db = new Ygo_Deck_Helper.Database("127.0.0.1", "x","x","card_db");
           await YuGiOh_Wiki_Search.Scrape_All_Cards(db);
       }
	}
}