using Newtonsoft.Json;

namespace NProg.Distributed.ZeroMQ
{
    public static class JsonExtensions
    {
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        
        public static T ReadFromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}