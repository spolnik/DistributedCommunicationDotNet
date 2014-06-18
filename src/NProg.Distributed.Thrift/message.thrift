namespace csharp NProg.Distributed.Thrift

struct ThriftMessage {
  1: string messageType,
  2: string body,
}

service MessageService {   
  ThriftMessage Send(1:ThriftMessage message)
}