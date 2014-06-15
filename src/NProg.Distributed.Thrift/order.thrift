namespace csharp NProg.Distributed.Thrift

struct ThriftOrder {
  1: string orderId,
  2: i32 count,
  3: i64 orderDate,
  4: double unitPrice,
  5: string userName,
}

service OrderService {   
   void Add(1:ThriftOrder order)
   ThriftOrder Get(1:string orderId)
   bool Remove(1:string orderId)
}