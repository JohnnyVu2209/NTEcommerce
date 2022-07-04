using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NTEcommerce.CustomerSite.Models;
using NTEcommerce.SharedDataModel.Category;

namespace NTEcommerce.CustomerSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryService _service;

    public HomeController(ILogger<HomeController> logger, ICategoryService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        // var data = await _service.GetCategories(new CategoryParameters{PageNumber=1, PageSize = 10});
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
