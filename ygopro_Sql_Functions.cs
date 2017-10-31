using System;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Ygo_Deck_Helper
{
    internal class Sqlite_Functions
    {
        public Sqlite_Functions(string Database_File_Path)
        {
            Data_Source = "Data Source = W" + Database_File_Path + ";";
        }

        private string data_Source;
        static public Ygo_Pro_Card_Database ygoPro_Card_Database = new Ygo_Pro_Card_Database();

        internal string Data_Source
        {
            get
            {
                return data_Source;
            }

            set
            {
                data_Source = value;
            }
        }

        private struct ygopro_Tables
        {
            public string datas;
            public string texts;
        }
        private readonly ygopro_Tables ygopro_Database_Tables = new ygopro_Tables { datas = "datas", texts = "texts" };

        private struct YuGiOh_Wiki_Tables
        {
            public string main;
            public string names;
            public string type_list;
            public string link_arrows;

        }
        private readonly YuGiOh_Wiki_Tables YuGiOh_Wiki_Database_Tables = new YuGiOh_Wiki_Tables
        {
            main = "ygo_main",
            names = "ygo_names",
            type_list = "ygo_type_list",
            link_arrows = "ygo_link_arrows"
        };

        /// <summary>
        /// get the latest database and convert it to type dataview
        /// </summary>
        /// <param name="_Data_Grid_View"> The data view to be set </param>
        public DataTable Assign_DataTableFromDatabase(string Name_Of_Table)
        {
            DataTable Selected_Table = null;
            try
            {
                SQLiteConnection Card_Connection = new SQLiteConnection(Data_Source);
                Card_Connection.Open();
                string command = "SELECT * FROM " + Name_Of_Table;

                Selected_Table = ExecuteSqlCommand(command, Card_Connection);
                return Selected_Table;
            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        private DataTable ExecuteSqlCommand(string Sql_Command, SQLiteConnection Current_Connection)
        {
            DataTable _Data_Grid_View = null;

            SQLiteCommand Grab_Data_Command = new SQLiteCommand(Sql_Command, Current_Connection);
            SQLiteDataReader dr = Grab_Data_Command.ExecuteReader();
            _Data_Grid_View.Load(dr);
            return _Data_Grid_View;
        }

        public async Task<Ygo_Pro_Card_Database> Set_Up_YgoProDatabase()
        {
            ygoPro_Card_Database.texts = await Task.Run(() => Assign_DataTableFromDatabase(ygopro_Database_Tables.texts));
            ygoPro_Card_Database.datas = await Task.Run(() => Assign_DataTableFromDatabase(ygopro_Database_Tables.datas));
            return ygoPro_Card_Database;
        }

        public async Task<YuGiOh_Wiki_Database> Connect_To_Database()
        {
            YuGiOh_Wiki_Database YuGiOh_Wiki_Card_Database = new YuGiOh_Wiki_Database();

            YuGiOh_Wiki_Card_Database.Main = await Task.Run(() => Assign_DataTableFromDatabase(YuGiOh_Wiki_Database_Tables.main));
            YuGiOh_Wiki_Card_Database.Names = await Task.Run(() => Assign_DataTableFromDatabase(YuGiOh_Wiki_Database_Tables.names));
            YuGiOh_Wiki_Card_Database.Type_list = await Task.Run(() => Assign_DataTableFromDatabase(YuGiOh_Wiki_Database_Tables.type_list));
            YuGiOh_Wiki_Card_Database.Type_list = await Task.Run(() => Assign_DataTableFromDatabase(YuGiOh_Wiki_Database_Tables.type_list));
            return YuGiOh_Wiki_Card_Database;
        }


       // public async Task addMonsterToDatabase(string add )
    }
}