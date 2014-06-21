using System;
using Newtonsoft.Json;

namespace NProg.Distributed.Core.Service.Extensions
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Reads from json.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns>System.Object.</returns>
        public static object ReadFromJson(this string json, string messageType)
        {
            var type = Type.GetType(messageType);
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// Reads from json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">The json.</param>
        /// <returns>T.</returns>
        internal static T ReadFromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
