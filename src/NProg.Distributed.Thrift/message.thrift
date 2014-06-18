namespace csharp NProg.Distributed.Thrift

struct Message {
  1: string messageType,
  2: string body,
}

service MessageService {   
  Message Send(1:Message message)
}