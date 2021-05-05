using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Package.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        public static T ParseTo<T>(this string json)
        {
            return string.IsNullOrEmpty(json) ? default : JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}