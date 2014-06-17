using System;
using System.Linq;
using NDatabase;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class NdbOdbDao : IHandler<Guid, Order>
    {
        private readonly string dbName;

        internal NdbOdbDao(string dbName)
        {
            this.dbName = dbName;
        }

        public void Add(Guid key, Order value)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                odb.Store(value);
            }
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
