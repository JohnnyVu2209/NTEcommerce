using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NTEcommerce.CustomerSite.Models;
using NTEcommerce.CustomerSite.Services;
using NTEcommerce.SharedDataModel.Category;
using Refit;

namespace NTEcommerce.CustomerSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    /*private readonly ICategoryService _service;*/
    ICategoryService categoryService = RestService.For<ICategoryService>("https://localhost:7012/api");

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var data = await categoryService.GetCategories(new CategoryParameters{PageNumber=1, PageSize = 10});
        return View(data);
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
