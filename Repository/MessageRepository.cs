using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.IReporsitory;

namespace Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly TaskAppContext _context;

        public MessageRepository(TaskAppContext context)
        {
            _context = context;
        }

        public async Task<List<Message>> GetMessagesForUserAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToListAsync();
        }

        public async Task<List<Message>> GetAllMessagesForUserAsync(int userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task AddMessageAsync(Message message)
        {
            _context.Messages.Add(message);
            int result = await _context.SaveChangesAsync();
            if (result == 0)
            {
                Console.WriteLine("Mesaj veritabanına kaydedilemedi.");
            }
        }

        public async Task DeleteMessageAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Message> GetMessageByIdAsync(int messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Message>> GetUnreadMessagesForUserAsync(int userId)
        {
            return await _context.Messages
                .Where(m => (m.ReceiverId == userId) && !m.IsRead)
                .ToListAsync();
        }

        public async Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2)
        {
            return await _context.Messages
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) || (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
