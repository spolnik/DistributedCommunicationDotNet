﻿using NProg.Distributed.Service.Extensions;
using NProg.Distributed.Service.Messaging;
using NProgDistributed.TheIce;

namespace NProg.Distributed.Ice
{
    public class IceMessageMapper : IMessageMapper
    {
        public Message Map(object message)
        {
            var messageDto = message.As<MessageDto>();

            return new Message
            {
                Body = messageDto.body.ReadFromJson(messageDto.messageType)
            };
        }

        public object Map(Message order)
        {
            return new MessageDto
            {
                body = order.Body.ToJsonString(),
                messageType = order.MessageType
            };
        }
    }
}