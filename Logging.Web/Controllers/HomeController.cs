using System.Diagnostics;
using Logging.Interfaces.Data;
using Logging.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logging.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly ILogger<HomeController> logger;

        public HomeController(
            IBookService bookService,
            IAuthorService authorService,
            ILogger<HomeController> logger)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var authors = this.authorService.GetAllAuthors();
            var books = this.bookService.GetAllBooks();

            using (this.logger.BeginScope("GettingMoreBooks"))
            {
                this.bookService.GetAllBooks();
            }

            //this.logger.LogInformation("Hello from the Home/Index page");
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
