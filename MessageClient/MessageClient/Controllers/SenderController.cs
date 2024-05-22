using Dtos;
using Microsoft.AspNetCore.Mvc;
namespace MessageClient.Controllers;

public class SenderController(HttpClient httpClient, IConfiguration configuration) : Controller
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _backEndUrl = configuration["BackEndUrl"];

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(MessageInDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"{_backEndUrl}/Message", dto);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Failed to send message. Server response: {errorContent}";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Failed to send message: {ex.Message}";
            return RedirectToAction("Index");
        }
        TempData["SuccessMessage"] = "Message sent successfully!";
        return RedirectToAction("Index");
    }
}
