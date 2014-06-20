using Newtonsoft.Json;

namespace NProg.Distributed.Service.Extensions
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.String.</returns>
        internal static string GetMessageType(this object obj)
        {
            return obj.GetType().AssemblyQualifiedName;
        }

        /// <summary>
        /// To the json string.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>System.String.</returns>
        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Ases the specified object.
        /// </summary>
        /// <typeparam name="TType">The type of the t type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>TType.</returns>
        public static TType As<TType>(this object obj) where TType : class
        {
            return obj as TType;
        }
    }
}
