using System;
using System.Linq;
using Newtonsoft.Json;
using Xunit;
using Yu_Gi_Oh_Wiki_Bot;

namespace Database_Unit_Tests
{
    public class Database_Unit_Tests
    {
         
        // Database testDatabase =
        [Fact]
        public async void insert_Card_Into_Database()
        {
            var Wiki_Search = await YuGiOh_Wiki_Search.Create_YuGiOh_Wiki_Search("http://yugioh.wikia.com/wiki/Dark_Magician_(Arkana)", true);
            Card Normal_Monster = await Wiki_Search.Scrape_Card();
            Normal_Monster.insert_Into_Wiki_Database();

            using (var Card_Context = new Card_Context())
            {

                Card_Context.SaveChanges();
                Main_Card_Data Normal_Monster_From_Database_main_card_data = Card_Context.Main_Card_Data.First(x => x.passcode == Normal_Monster.Passcode);
                // var data = Card
                // var Normal_Monster_From_Database_Archtype_List = Card_Context.Archtype_Table.Where(x => x.passcode == Normal_Monster.Passcode).Select(x => x.name).ToList();
                //var Normal_Monster_From_Database_Link_Arrow_List  = Card_Context.link_arrow_Table.Where(x => x.passcode == Normal_Monster.Passcode).Select(x => x.name).ToList();
                //var Normal_Monster_From_Database_Foreign_Name_List = Card_Context.Foreign_Name_Table.Where(x => x.passcode == Normal_Monster.Passcode).ToList();

                Assert.Equal(JsonConvert.SerializeObject(Normal_Monster.Get_main_card_data()), JsonConvert.SerializeObject(Normal_Monster_From_Database_main_card_data));
                //Assert.Equal(Normal_Monster.Archtype_List.OrderBy(i => i),Normal_Monster_From_Database_Archtype_List.OrderBy(i => i));
                //Assert.Equal(Normal_Monster.Link_Arrows.OrderBy(i => i), Normal_Monster_From_Database_Link_Arrow_List.OrderBy(i => i));
                //Assert.Equal(Normal_Monster.Foreign_Name_Entrys.Select(i=> i.name).OrderBy(i => i), Normal_Monster_From_Database_Foreign_Name_List.Select(i => i.name).OrderBy(i => i));
            }
        }


    }
}
