namespace Ygo_Deck_Helper
{
    public class Database
    {
        private string host;
        private string user;
        private string password;
        private string name;

        public Database(string host, string user, string password, string name) {
            this.host = host;
            this.user = user;
            this.password = password;
            this.name = name;
        }

        public string GenerateConnectionString()  {

            var mySql_Connection_string = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
            mySql_Connection_string.CharacterSet = "utf8";
            mySql_Connection_string.Server = host;
            mySql_Connection_string.UserID = user;
            mySql_Connection_string.Password = password;
            mySql_Connection_string.Database = name;
            return mySql_Connection_string.ToString();
        }

    }
}