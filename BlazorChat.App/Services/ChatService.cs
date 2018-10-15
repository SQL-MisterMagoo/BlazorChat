using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.Services
{
	public class ChatService
	{
		List<ChatMessage> ChatMessages { get; set; }
		public event EventHandler NotifyChange;

		public ChatService()
		{
			ChatMessages = new List<ChatMessage>();
			AddChatMessage(new ChatMessage("Server", "Welcome - please be family friendly")).ConfigureAwait(false);
		}

		public async Task<bool> ClientSendAsync(ChatMessage chatMessage)
		{
			try
			{
				bool result = await AddChatMessage(chatMessage);
				OnNotifyChange(null);
				return result;
			}catch(Exception e)
			{
				Console.Write(e);
			}
			return false;
		}

		protected virtual void OnNotifyChange(EventArgs args)
		{
			NotifyChange?.Invoke(this, args);
		}

		private Task<bool> AddChatMessage(ChatMessage chatMessage)
		{
			return Task.Run(() =>
			 {
				 chatMessage.Received = DateTime.UtcNow;
				 lock (ChatMessages)
				 {
					 ChatMessages.Add(chatMessage);
				 }
				 return true;
			 });			
		}

		public IEnumerable<ChatMessage> GetChatMessages()
		{
			return ChatMessages;
		}
	}
}
