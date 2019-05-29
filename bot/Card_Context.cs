using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Configuration;

namespace Yu_Gi_Oh_Wiki_Bot
{


    public class Card_Context : DbContext
    {
        public DbSet<Main_Card_Data> Main_Card_Data { get; set; }

        public DbSet<Effect_keyword_Table> Effect_keyword_Table { get; set; }

        public DbSet<Foreign_Name_Table> Foreign_Name_Table { get; set; }
        public DbSet<Archtype_Table> Archtype_Table { get; set; }

        public DbSet<Attribute_Table> Attribute_Table { get; set; }
        // public static Database CurrentDatabase { get => currentDatabase; set => currentDatabase = value; }
        static public Database currentDatabase;
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {  
               var db =new Yu_Gi_Oh_Wiki_Bot.Database( "127.0.0.1", "x", "x", "card_db_test");
            optionsBuilder.UseMySql( db.GenerateConnectionString());
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

        
        public Card_Context()
        {
            this.Database.EnsureCreated();
        }


    }
    public class Main_Card_Data
    {

        public int cardID { get; set; } = 1;
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
        public virtual ICollection<Effect_keyword_Main_Table> Effect_keyword_Main { get; set; }
        public virtual ICollection<Foreign_Name_Main_Table> Foreign_Name_Main { get; set; }
        public virtual ICollection<Archtype_Main_Table> Archtype_Main { get; set; }
        public virtual ICollection<Attribute_Main_Table> Attribute_Main { get; set; }

    }


    public class Effect_keyword_Table
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Effect_keyword_Main_Table
    {
        [Key, Column(Order = 0)]
        public int keywordID{ get; set; }
        [Key, Column(Order = 1)]
        public int cardID{ get; set; }

        public virtual Effect_keyword_Table Effect_keyword_Table { get; set; }
        public virtual Main_Card_Data Main_Card_Data { get; set; }
    }

    public class Foreign_Name_Table
    {
        public int id { get; set; }

        public string contry_code { get; set; }
        [Column(TypeName = "VARCHAR(255)")]
        public string name { get; set; }

    }
    public class Foreign_Name_Main_Table
    {
        [Key, Column(Order = 0)]
        public int nameID{ get; set; }
        [Key, Column(Order = 1)]
        public int cardID{ get; set; }

        public virtual Foreign_Name_Table Foreign_Name_Table { get; set; }
        public virtual Main_Card_Data Main_Card_Data { get; set; }
    }
    public class Archtype_Table
    {
        public int archtypeID { get; set; }

        public string name { get; set; }

    }
    public class Archtype_Main_Table
    {
        [Key, Column(Order = 0)]
        public int archtypeID{ get; set; }
        [Key, Column(Order = 1)]
        public int cardID{ get; set; }
        public virtual Archtype_Table Archtype_Table { get; set; }
        public virtual Main_Card_Data Main_Card_Data { get; set; }
    }

    public class Attribute_Table
    {
        public int id { get; set; }
        public string name { get; set; }

    }
    public class Attribute_Main_Table
    {
        [Key, Column(Order = 0)]
        public int attributeID{ get; set; }
        [Key, Column(Order = 1)]
        public int cardID{ get; set; }
        public virtual Attribute_Table Attribute_Table { get; set; }
        public virtual Main_Card_Data Main_Card_Data { get; set; }
    }

}