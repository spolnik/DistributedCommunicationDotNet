module Order
{
	class OrderDto {
		string orderId;
		int count;
		long orderDate;
		double unitPrice;
		string userName;
	};

	interface OrderService {   
		void Add(string orderId, OrderDto order);
		OrderDto Get(string orderId);
		bool Remove(string orderId);
	};	
};