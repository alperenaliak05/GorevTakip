using Models;

namespace Repositories.IReporsitory
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetMessagesForUserAsync(int userId);
        Task<List<Message>> GetAllMessagesForUserAsync(int userId);
        Task AddMessageAsync(Message message);
        Task DeleteMessageAsync(int messageId);
        Task<Message> GetMessageByIdAsync(int messageId);
        Task MarkAsReadAsync(int messageId);
        Task<List<Message>> GetUnreadMessagesForUserAsync(int userId);
        Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
    }
}
