using Newtonsoft.Json;

namespace NProg.Distributed.Service.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetMessageType(this object obj)
        {
            return obj.GetType().AssemblyQualifiedName;
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static TType As<TType>(this object obj) where TType : class
        {
            return obj as TType;
        }
    }
}
