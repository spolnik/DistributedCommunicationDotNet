namespace NProg.Distributed.Service.Messaging
{
    public class SimpleMessageMapper : IMessageMapper
    {
        public Message Map(object customMessage)
        {
            return customMessage as Message;
        }
        
        public object Map(Message message)
        {
            return message;
        }
    }
}