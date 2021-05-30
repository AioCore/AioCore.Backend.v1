namespace Package.DatabaseManagement
{
    public class DatabaseInfo
    {
        public string Server { get; set; }

        public string User { get; set; }

        public string Database { get; set; }

        public string Password { get; set; }

        public string Schema { get; set; }

        public DatabaseType DatabaseType { get; set; }
    }
}
