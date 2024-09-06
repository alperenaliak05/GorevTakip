using Data;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace TaskApp_Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly TaskAppContext _context;

        public ChatHub(TaskAppContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int senderId, int receiverId, string content)
        {
            try
            {
                var message = new Message
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Content = content,
                    Timestamp = DateTime.UtcNow,
                    IsRead = false
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", new
                {
                    senderId = message.SenderId,
                    receiverId = message.ReceiverId,
                    content = message.Content,
                    timestamp = message.Timestamp
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
