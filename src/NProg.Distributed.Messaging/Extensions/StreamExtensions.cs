﻿using System.IO;
using Newtonsoft.Json;

namespace NProg.Distributed.Messaging.Extensions
{
    internal static class StreamExtensions
    {
        internal static T ReadFromJson<T>(this Stream stream)
        {
            var json = stream.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static string ReadToEnd(this Stream stream)
        {
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
