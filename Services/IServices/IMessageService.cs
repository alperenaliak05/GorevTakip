using Models;

namespace Services.IServices
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesBetweenUsersAsync(int userId1, int userId2);
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetUnreadMessagesForUserAsync(int userId);
    }
}
