using System;

namespace BlazorChat.Services
{
	public class ChatMessage
	{
		public string Name    { get; set; }
		public string Message { get; set; }
		public Guid MessageId { get; internal set; }
		public Guid ClientId { get; set; }
		public DateTime Received { get; internal set; }
		public DateTime Sent { get; set; }

		public ChatMessage(string name, string message, Guid clientId)
		{
			if (clientId == null || clientId.Equals(Guid.Empty))
				throw new ApplicationException("Invalid ClientId");
			Name = name;
			Message = message;
			ClientId = clientId;
			Sent = DateTime.UtcNow;
		}

		internal ChatMessage(string name, string message)
		{
			Name = name;
			Message = message;
			ClientId = Guid.Empty;
			Sent = DateTime.UtcNow;
		}


	}
}