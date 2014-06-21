using Newtonsoft.Json;
using NUnit.Framework;

namespace NProg.Distributed.Transport.Tests
{
    public class TestJsonSerialization
    {
        [Test]
        public void CheckSerializationOfString()
        {
            var json = JsonConvert.SerializeObject("True");
            var deserializedObject = JsonConvert.DeserializeObject(json, typeof(string));

            Assert.That(deserializedObject, Is.EqualTo("True"));
        }
    }
}
