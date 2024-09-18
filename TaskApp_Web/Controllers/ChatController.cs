using DTOs.TaskApp_WebDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using Repositories.IReporsitory;
using TaskApp_Web.Hubs;

namespace TaskApp_Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatController(IUserRepository userRepository, IMessageRepository messageRepository, IHubContext<ChatHub> chatHubContext)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _chatHubContext = chatHubContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser != null)
            {
                ViewBag.UserDepartment = currentUser.Department?.Name;
            }
            else
            {
                ViewBag.UserDepartment = "Bilinmiyor";
            }

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDto)
        {
            var sender = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (sender == null)
            {
                return Unauthorized();
            }

            var message = new Message
            {
                SenderId = sender.Id,
                ReceiverId = messageDto.ReceiverId,
                Content = messageDto.MessageContent,
                Timestamp = DateTime.UtcNow,
                IsRead = false
            };

            await _messageRepository.AddMessageAsync(message);
            await _chatHubContext.Clients.User(messageDto.ReceiverId.ToString()).SendAsync("ReceiveMessage", message);

            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetMessageHistory(int userId)
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            var messages = await _messageRepository.GetMessagesBetweenUsersAsync(currentUser.Id, userId);

            var messageDTOs = new List<MessageDTO>();

            foreach (var message in messages)
            {
                var sender = await _userRepository.GetUserByIdAsync(message.SenderId);
                var receiver = await _userRepository.GetUserByIdAsync(message.ReceiverId);

                messageDTOs.Add(new MessageDTO
                {
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    SenderName = sender?.FirstName + " " + sender?.LastName,
                    ReceiverName = receiver?.FirstName + " " + receiver?.LastName,
                    MessageContent = message.Content,
                    Timestamp = message.Timestamp
                });
            }

            return Json(messageDTOs);
        }






        [HttpGet]
        public async Task<IActionResult> GetChatList()
        {
            var currentUser = await _userRepository.GetUserByEmailAsync(User.Identity.Name);

            if (currentUser == null)
            {
                return Unauthorized();
            }

            var messages = await _messageRepository.GetMessagesForUserAsync(currentUser.Id);

            var chatUsers = new List<object>();

            foreach (var group in messages.GroupBy(m => m.SenderId == currentUser.Id ? m.ReceiverId : m.SenderId))
            {
                var userId = group.Key;
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user != null)
                {
                    chatUsers.Add(new
                    {
                        UserId = userId,
                        UserName = $"{user.FirstName} {user.LastName}"
                    });
                }
            }

            return Json(chatUsers);
        }



    }
}
