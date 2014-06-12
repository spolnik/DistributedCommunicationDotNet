using System;
using System.Messaging;

namespace NProg.Distributed.Msmq.CreateQueues
{
    static class Program
    {
        static void Main()
        {
            CreateQueue(".\\private$\\nprog.messagequeue.add-order");
            CreateQueue(".\\private$\\nprog.messagequeue.get-order");
            CreateQueue(".\\private$\\nprog.messagequeue.remove-order");
        }

        private static void CreateQueue(string queueName)
        {
            MessageQueue.Create(queueName);
            Console.WriteLine("Queue created: {0}", queueName);
        }
    }
}
