using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Yu_Gi_Oh_Wiki_Bot
{


    public class Card_Context : DbContext
    {
        public DbSet<Main_Card_Data> Main_Card_Data { get; set; }

        public DbSet<Effect_keyword_Table> Effect_keyword_Table { get; set; }

        public DbSet<Foreign_Name_Table> Foreign_Name_Table { get; set; }
        public DbSet<Archtype_Table> Archtype_Table { get; set; }

        public DbSet<Attribute_Table> Attribute_Table { get; set; }

        Database currentDatabase;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(currentDatabase.GenerateConnectionString());
        }


        public void ClearAllTables()
        {
            Main_Card_Data.RemoveRange(Main_Card_Data);
            Effect_keyword_Table.RemoveRange(Effect_keyword_Table);
            Foreign_Name_Table.RemoveRange(Foreign_Name_Table);
            Archtype_Table.RemoveRange(Archtype_Table);
            Attribute_Table.RemoveRange(Attribute_Table);
            SaveChanges();
        }


        public Card_Context(Database database)
        {
            currentDatabase = database;
            this.Database.EnsureCreated();
          
        }


    }
    public class Main_Card_Data
    {

        public int id  { get; set; }  = 1;
        public int passcode { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string name_en { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string name_jp { get; set; }
        public string card_type { get; set; }
        public string attribute { get; set; }
        public string material { get; set; }
        public int level_or_rank { get; set; }
        public int? scale { get; set; }
        public int? attack { get; set; }
        public int? defence { get; set; }


    }


    public class Effect_keyword_Table
    {
        public int id { get; set; }
        public int passcode { get; set; }
        public string name { get; set; }
    }
    public class Effect_keyword_Main_Table
    {
         [Key, Column(Order = 0)]
        public int keywordID;
         [Key, Column(Order = 1)]
        public int cardID;
    }

    public class Foreign_Name_Table
    {
        public int id { get; set; }
      
        public string contry_code { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string name { get; set; }

    }
    class Foreign_Name_Main_Table
    {
         [Key, Column(Order = 0)]
        public int nameID;
         [Key, Column(Order = 1)]
        public int cardID;
    }
    public class Archtype_Table
    {
        public int id { get; set; }
      
        public string name { get; set; }

    }
    class Archtype_Main_Table
    {
         [Key, Column(Order = 0)]
        public int archtypeID;
         [Key, Column(Order = 1)]
        public int cardID;
    }

    public class Attribute_Table
    {
        public int id { get; set; }
        public string name { get; set; }

    }
    class Attribute_Main_Table
    {
         [Key, Column(Order = 0)]
        public int attributeID;
         [Key, Column(Order = 1)]
        public int cardID;

    }

}