using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

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

        public DbSet<Attribute_Table> Attribute_Table {get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            

            /* 
            var mySql_Connection_string = new  MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
            mySql_Connection_string.Server = "card.database.windows.net";
            mySql_Connection_string.UserID = "ll01";
            mySql_Connection_string.Password = "qmvWY@empKg8L@&d8JkSK";
            mySql_Connection_string.Database = "card_db";
            */


            optionsBuilder.UseSqlServer("Server=tcp:card.database.windows.net,1433;Initial Catalog=card_db;Persist Security Info=False;User ID=ll01;Password=qmvWY@empKg8L@&d8JkSK;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public Card_Context() {
            this.Database.EnsureCreated();
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
        public int id { get; set; }
        public int passcode { get; set; }
        public string type { get; set; }

    }

    public class Effect_keyword_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string effect_name { get; set; }
    }

    public class Foreign_Name_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string contry_code { get; set; }
        public string card_name { get; set; }

    }

    public class link_arrow_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string link_arrow { get; set; }

    }
    public class Archtype_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string archtype_name { get; set; }

    }


      public class Attribute_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string Attribute_Name{ get; set; }

    }


}