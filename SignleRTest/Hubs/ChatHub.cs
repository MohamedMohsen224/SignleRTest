using Microsoft.AspNetCore.SignalR;
using SignleRTest.Context;
using SignleRTest.Models;

namespace SignleRTest.Hubs
{
    public class ChatHub :Hub
    {
        private readonly ILogger<ChatHub> logger;
        private readonly SignleRContext context;

        public ChatHub(ILogger<ChatHub> logger,SignleRContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task Send(string user, string message)
        {
            await Clients.Others.SendAsync("Resiviemessage", user, message);
            Message msg = new Message()
            {
                User = user,
                Text = message
            };
            context.messages.Add(msg);
            await context.SaveChangesAsync();

        }
        public async Task JoinGroup(string groupName,string Username )
        {
            await Groups.AddToGroupAsync(Context.ConnectionId,groupName);
            await Clients.OthersInGroup(groupName).SendAsync("JoinGroup",Username,groupName);
            logger.LogInformation(Context.ConnectionId);
        }
        public async Task SendToGroup(string Groupname,string sender, string message)
        {
            await Clients.Group(Groupname).SendAsync("ResiviemessageFromGroup", sender, message);
            Message msg = new Message()
            {
                User = sender,
                Text = message
            };
            context.messages.Add(msg);
            await context.SaveChangesAsync();
        }

    }
}
