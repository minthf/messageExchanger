using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class MessageService(IMessageRepository messageRepository)
{
    private readonly IMessageRepository _messageRespository = messageRepository;

    public async Task<MessageOutDto> SendMessage(MessageInDto messageDto)
    {
        var message = messageDto.Adapt<Message>();
        message.Date = DateTime.UtcNow;
        message.Id = await _messageRespository.AddMessageAsync(message);
        return message.Adapt<MessageOutDto>();
    }

    public async Task<IEnumerable<MessageOutDto>> GetMessages(DateTime start, DateTime end)
    {
        var messages = await _messageRespository.GetMessagesAsync(start, end);

        return messages.Adapt<IEnumerable<MessageOutDto>>();
    }

}
