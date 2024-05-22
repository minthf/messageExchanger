using Microsoft.AspNetCore.Mvc;

namespace MessageClient.Controllers;

public class ReaderController(IConfiguration config) : Controller
{
    private readonly string _signalRUrl = config["SignalRUrl"];

    public IActionResult Index()
    {
        ViewData["MessageHubUrl"] = _signalRUrl;
        return View();
    }
}
