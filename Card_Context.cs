using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using MySql.Data.MySqlClient;

namespace Ygo_Deck_Helper
{

   
        public class Card_Context : DbContext
        {
            public DbSet<Main_Card_Data> Main_Card_Data { get; set; }
            public DbSet<Type_Table> Type_Table { get; set; }

            public DbSet<Effect_keyword_Table> Effect_keyword_Table { get; set; }

            public DbSet<Foreign_Name_Table> Foreign_Name_Table { get; set; }
            public DbSet<link_arrow_Table> link_arrow_Table { get; set; }
            public DbSet<Archtype_Table> Archtype_Table { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                var mySql_Connection_string = new MySqlConnectionStringBuilder();
                mySql_Connection_string.Server = "08240newyear.000webhostapp.com";
                mySql_Connection_string.UserID = "id447835_x";
                mySql_Connection_string.Password = "r5JJq?U^vp-XWSPeL?9dEH!xJ%We-@TC";
                mySql_Connection_string.Database = "id447835_card_db";

                optionsBuilder.UseMySQL(mySql_Connection_string.ToString());
     		}


        }
        public class Main_Card_Data
        {

            public int id { get; set; }
            public int passcode { get; set; }
            public string name_en { get; set; }
            public string name_jp { get; set; }
            public string card_type { get; set; }
            public string attribute { get; set; }
            public string material { get; set; }
            public int level_or_rank { get; set; }
            public int? scale { get; set; }
            public int? attack { get; set; }
            public int? defence { get; set; }
            public int link { get; set; }

        }

        public class Type_Table
        {
            public int passcode { get; set; }
            public string type { get; set; }

        }

         public class Effect_keyword_Table
        {
            public int passcode { get; set; }
            public string effect_name { get; set; }
        }

         public class Foreign_Name_Table
        {
            public int passcode { get; set; }
            public string contry_code { get; set; }
            public string card_name { get; set; }

        }

        public class link_arrow_Table
        {
            public int passcode { get; set; }
            public string link_arrow { get; set; }

        }
        public class Archtype_Table
        {
            public int passcode { get; set; }
            public string archtype_name { get; set; }

        }

    
}