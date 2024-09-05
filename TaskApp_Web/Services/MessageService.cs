﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TaskApp_Web.Models;
using TaskApp_Web.Repositories.IRepositories;
using TaskApp_Web.Services.IServices;

namespace TaskApp_Web.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            return await _messageRepository.GetMessagesBetweenUsersAsync(userId1, userId2);
        }

        public async Task AddMessageAsync(Message message)
        {
            await _messageRepository.AddMessageAsync(message);
        }

        public async Task<List<Message>> GetUnreadMessagesForUserAsync(int userId)
        {
            return await _messageRepository.GetUnreadMessagesForUserAsync(userId);
        }
    }
}