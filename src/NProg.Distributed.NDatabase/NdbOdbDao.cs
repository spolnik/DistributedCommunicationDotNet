using System;
using System.Linq;
using NDatabase;
using NProg.Distributed.OrderService.Api;
using NProg.Distributed.OrderService.Domain;

namespace NProg.Distributed.NDatabase
{
    public sealed class NdbOdbDao : IOrderApi
    {
        private readonly string dbName;

        internal NdbOdbDao(string dbName)
        {
            this.dbName = dbName;
        }

        public bool Add(Guid key, Order value)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                odb.Store(value);
            }

            return true;
        }

        public Order Get(Guid key)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                return odb.QueryAndExecute<Order>().FirstOrDefault(x => x.OrderId.Equals(key)) ?? new Order();
            }
        }

        public bool Remove(Guid key)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var orderToRemove = odb.QueryAndExecute<Order>().FirstOrDefault(x => x.OrderId.Equals(key));

                if (orderToRemove == null)
                    return false;

                var oid = odb.GetObjectId(orderToRemove);

                odb.DeleteObjectWithId(oid);
                return true;
            }
        }
    }
}
