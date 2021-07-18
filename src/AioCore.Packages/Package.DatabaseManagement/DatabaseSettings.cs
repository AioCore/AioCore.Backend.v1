using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Package.DatabaseManagement
{
    public class DatabaseSettings
    {
        public string Server { get; set; }

        public string User { get; set; }

        public string Database { get; set; }

        public string Password { get; set; }

        public string Schema { get; set; }

        public DatabaseType DatabaseType { get; set; }

        public static DatabaseSettings Parse(string strInfo)
        {
            if (string.IsNullOrEmpty(strInfo)) return null;
            return JsonConvert.DeserializeObject<DatabaseSettings>(strInfo, CreateJsonSerializerSettings());
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, CreateJsonSerializerSettings());
        }

        private static JsonSerializerSettings CreateJsonSerializerSettings()
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }
    }
}
