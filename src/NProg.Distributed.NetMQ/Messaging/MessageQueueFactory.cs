﻿using System.Collections.Generic;
using NProg.Distributed.Messaging.Spec;

namespace NProg.Distributed.NetMQ.Messaging
{
    public static class MessageQueueFactory
    {
        private static readonly Dictionary<string, IMessageQueue> Queues = new Dictionary<string, IMessageQueue>();

        public static IMessageQueue CreateInbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null)
        {
            var key = string.Format("{0}:{1}:{2}", Direction.Inbound, name, pattern);
            if (Queues.ContainsKey(key))
                return Queues[key];

            var queue = Create();
            queue.InitialiseInbound(name, pattern, properties);
            Queues[key] = queue;
            return Queues[key];
        }

        public static IMessageQueue CreateOutbound(string name, MessagePattern pattern,
            Dictionary<string, object> properties = null)
        {
            var key = string.Format("{0}:{1}:{2}", Direction.Outbound, name, pattern);
            if (Queues.ContainsKey(key))
                return Queues[key];

            var queue = Create();
            queue.InitialiseOutbound(name, pattern, properties);
            Queues[key] = queue;
            return Queues[key];
        }

        private static IMessageQueue Create()
        {
            return new NmqMessageQueue();
        }
    }
}