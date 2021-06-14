using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Package.Elasticsearch
{
    public class ElasticsearchInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Index { get; set; }

        public static ElasticsearchInfo Parse(string strInfo)
        {
            if (string.IsNullOrEmpty(strInfo)) return null;
            return JsonConvert.DeserializeObject<ElasticsearchInfo>(strInfo, CreateJsonSerializerSettings());
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
