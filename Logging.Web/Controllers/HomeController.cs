using System.Diagnostics;
using Logging.Interfaces.Data;
using Logging.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logging.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IBookService bookService, ILogger<HomeController> logger)
        {
            this._bookService = bookService;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            var books = this._bookService.GetBooks();

            this._logger.LogInformation("Hello from the Home/Index page");
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
}
