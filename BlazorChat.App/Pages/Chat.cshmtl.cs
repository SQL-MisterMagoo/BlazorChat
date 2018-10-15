using BlazorChat.Services;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.App.Pages
{
	public class ChatModel : BlazorComponent
	{
		[Inject] internal ChatService ChatService { get; set; }
		public string UserName { get; set; }
		
		internal string message;
		internal IEnumerable<ChatMessage> Messages;
		private Guid clientId;

		protected override void OnInit()
		{
			UserName = "";
			base.OnInit();
			clientId = Guid.NewGuid();
			ChatService.NotifyChange += ChatService_NotifyChange;
			Messages = ChatService.GetChatMessages();
		}

		private void ChatService_NotifyChange(object sender, EventArgs e)
		{
			var MessagesNew = ChatService.GetChatMessages();
			Messages = new List<ChatMessage>(MessagesNew);
			StateHasChanged();
		}
		
		internal async Task<bool> SetUserName(UIEventArgs args)
		{			
			return false;
		}
		internal async Task<bool> InputChanged(UIEventArgs args)
		{
			ChatMessage cm = new ChatMessage(UserName, message, clientId);
			bool result = await ChatService.ClientSendAsync(cm);
			if (result == true)
			{
				message = "";
				StateHasChanged();
			}
			return false;
		}
	}
}
