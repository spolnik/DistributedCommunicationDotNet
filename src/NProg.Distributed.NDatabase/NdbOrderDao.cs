using System;
using System.Linq;
using NDatabase;
using NProg.Distributed.Domain;
using NProg.Distributed.Service;

namespace NProg.Distributed.NDatabase
{
    public class NdbOrderDao : IHandler<Order>
    {
        private readonly string dbName;

        internal NdbOrderDao(string dbName)
        {
            this.dbName = dbName;
        }

        public void Add(Order item)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                odb.Store(item);
            }
        }

        public Order Get(Guid guid)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                return odb.QueryAndExecute<Order>().FirstOrDefault(x => x.OrderId.Equals(guid)) ?? new Order();
            }
        }

        public bool Remove(Guid guid)
        {
            using (var odb = OdbFactory.Open(dbName))
            {
                var orderToRemove = odb.QueryAndExecute<Order>().FirstOrDefault(x => x.OrderId.Equals(guid));

                if (orderToRemove == null)
                    return false;

                var oid = odb.GetObjectId(orderToRemove);

                odb.DeleteObjectWithId(oid);
                return true;
            }
        }
    }
}
