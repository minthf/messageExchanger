using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MessageClient.Controllers;

public class ReaderByDateController(HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _backEndUrl = configuration["BackEndUrl"];

    [HttpGet]
    public async Task<IActionResult> Index(DateTime? start, DateTime? end)
    {
        if (!start.HasValue || !end.HasValue)
        {
            end = DateTime.UtcNow;
            start = end.Value.AddMinutes(-10);
        }

        try
        {
            var messages = await GetMessages(start, end);
            return View(messages);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
        }

        return View(Enumerable.Empty<MessageOutDto>());
    }

    private async Task<IEnumerable<MessageOutDto>> GetMessages(DateTime? start, DateTime? end)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<MessageOutDto>>($"{_backEndUrl}/Message?start={start:yyyy-MM-ddTHH:mm:ss}&end={end:yyyy-MM-ddTHH:mm:ss}");
        return response ?? Enumerable.Empty<MessageOutDto>();
    }
}
