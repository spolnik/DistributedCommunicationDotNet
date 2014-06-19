namespace NProg.Distributed.Service.Messaging
{
    public interface IMessageMapper
    {
        Message Map(object customMessage);
        
        object Map(Message message);
    }
}