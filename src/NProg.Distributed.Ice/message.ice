module NProgDistributed
{	
	module TheIce
	{
		class MessageDto {
			string body;
			string messageType;
		};

		interface IMessageService {   
			MessageDto Send(MessageDto message);
		};
	};
};