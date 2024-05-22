using Application.Dtos;
using Application.Services;
using MessageServer.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace MessageServer.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController(MessageService messageService, IHubContext<MessageHub> hubContext) : ControllerBase
{
    private readonly MessageService _messageService = messageService;
    private readonly IHubContext<MessageHub> _hubContext = hubContext;

    [HttpPost]
    public async Task<ActionResult> PostMessage(MessageInDto message)
    {
        var messageToHub = await _messageService.SendMessage(message);
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", messageToHub);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageOutDto>>> MessagesByDate([Required] DateTime start, [Required] DateTime end)
    {
        var messages = await _messageService.GetMessages(start, end);
        return Ok(messages);
    }
}
