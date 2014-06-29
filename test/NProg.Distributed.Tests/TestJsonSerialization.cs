using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NProg.Distributed.Transport.Tests.Annotations;
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

        [Test]
        public void SerializingWithExtensionDataUsage()
        {
            const string json = @"{
              'HourlyRate': 150,
              'Hours': 40,
              'TaxRate': 0.125
            }";

            var customerInvoice = JsonConvert.DeserializeObject<CustomerInvoice>(json);

            // increase tax to 15%
            customerInvoice.TaxRate = 0.15m;

            var customerInvoiceJson = JsonConvert.SerializeObject(customerInvoice);

            var fullCustomerInvoice = JsonConvert.DeserializeObject<FullCustomerInvoice>(customerInvoiceJson);

            Assert.That(fullCustomerInvoice.HourlyRate, Is.EqualTo(150m));
            Assert.That(fullCustomerInvoice.Hours, Is.EqualTo(40m));
            Assert.That(fullCustomerInvoice.TaxRate, Is.EqualTo(0.15m));
        }

        [Test]
        public void SerializingWithoutExtensionDataUsage()
        {
            const string json = @"{
              'HourlyRate': 150,
              'Hours': 40,
              'TaxRate': 0.125
            }";

            var basicCustomerInvoice = JsonConvert.DeserializeObject<BasicCustomerInvoice>(json);

            // increase tax to 15%
            basicCustomerInvoice.TaxRate = 0.15m;

            var customerInvoiceJson = JsonConvert.SerializeObject(basicCustomerInvoice);

            var fullCustomerInvoice = JsonConvert.DeserializeObject<FullCustomerInvoice>(customerInvoiceJson);

            Assert.That(fullCustomerInvoice.HourlyRate, Is.EqualTo(0m));
            Assert.That(fullCustomerInvoice.Hours, Is.EqualTo(0m));
            Assert.That(fullCustomerInvoice.TaxRate, Is.EqualTo(0.15m));
        }

        public class CustomerInvoice
        {
            public decimal TaxRate { get; set; }

            [JsonExtensionData, UsedImplicitly]
            private IDictionary<string, JToken> additionalData;
        }

        public class FullCustomerInvoice
        {
            public decimal TaxRate { get; set; }

            public decimal HourlyRate { get; set; }

            public decimal Hours { get; set; }

            [JsonExtensionData, UsedImplicitly]
            private IDictionary<string, JToken> additionalData;
        }

        public class BasicCustomerInvoice
        {
            public decimal TaxRate { get; set; }
        }
    }
}
