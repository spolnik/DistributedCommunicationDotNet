﻿using System;
using Ice;
using NProg.Distributed.Service;
using Order;

namespace NProg.Distributed.Ice
{
    public class IceOrderClient : IHandler<Domain.Order>, IDisposable
    {
        private readonly Communicator communicator;
        private readonly OrderServicePrx proxy;

        public IceOrderClient(Uri serviceUri)
        {
            var address = string.Format("OrderService:tcp -p {1} -h {0}", serviceUri.Host, serviceUri.Port);

            communicator = Util.initialize();
            proxy = OrderServicePrxHelper.checkedCast(communicator.stringToProxy(address));
        }

        public void Add(Domain.Order item)
        {
            proxy.Add(OrderMapper.MapOrder(item));
        }

        public Domain.Order Get(Guid guid)
        {
            return OrderMapper.MapOrder(proxy.Get(guid.ToString()));
        }

        public bool Remove(Guid guid)
        {
            return proxy.Remove(guid.ToString());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && communicator != null)
                communicator.destroy();
        }
    }
}