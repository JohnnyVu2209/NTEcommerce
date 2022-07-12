using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NTEcommerce.CustomerSite.Services;
using NTEcommerce.SharedDataModel.Product;
using Refit;

namespace NTEcommerce.CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductController
        readonly IProductService productService = RestService.For<IProductService>("https://localhost:7012");
        private readonly ProductParameters param = new() { PageNumber = 1, PageSize = 9 };
        public ProductController()
        {
        }
        public async Task<IActionResult> Index()
        {
            var data = await productService.GetProducts(param);
            return View(data);
        }
        /*[HttpGet]
        public async Task<IActionResult> Index(int pageNumber)
        {
            param.PageNumber = pageNumber;
            var data = await productService.GetProducts(param);
            return View(data);
        }*/

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var data = await productService.GetProduct(id);
            return View(data);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
