module NProgDistributed
{	
	module TheIce
	{
		class MessageDto {
			string body;
			string messageType;
		};

		interface MessageService {   
			MessageDto Send(MessageDto message);
		};
	};
};