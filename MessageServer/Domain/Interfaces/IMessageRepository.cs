using Domain.Entities;

namespace Domain.Interfaces;

public interface IMessageRepository
{
    Task<int> AddMessageAsync(Message message);
    Task<List<Message>> GetMessagesAsync(DateTime start, DateTime end);
}
