using System;
using System.Diagnostics;
using Logging.Interfaces.Data;
using Logging.LoggerExtensions;
using Logging.Web.Models;
using Microsoft.AspNetCore.Diagnostics;
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
            //var authors = this.authorService.GetAllAuthors();
            //var books = this.bookService.GetAllBooks();

            //using (this.logger.BeginScope("GettingMoreBooks"))
            //{
            //    this.bookService.GetAllBooks();
            //}



            //High performance logging
            using (logger.GetBooksScoped("123"))
            {
                logger.RepoGetBooks();
                this.bookService.GetMoreBooks();
                logger.RepoGetMoreBooks("usp_GetMoreBooks");
                this.bookService.GetMoreBooks();
            }



            //this.logger.LogInformation("Hello from the Home/Index page");
            return View();
        }

        public IActionResult Privacy()
        {
            Exception ex = new Exception("Users should not see this!");

            //Add custom data to the exception, it can be something from the request as well
            //Not automatically included in the exception log, we should handle this ourselves
            //by adding log entries or by using the serilog exception logging package
            ex.Data.Add("CreditCardNumber", 234);

            throw ex;
        }

        public string ApiCall()
        {
            return "abc";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //Get the exception
            string creditCartNumber = null;
            var exceptionPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = exceptionPathFeature?.Error;

            if (ex != null && ex.Data.Contains("CreditCardNumber"))
            {
                creditCartNumber = ex.Data["CreditCardNumber"].ToString();
            }

            //Anyway, be aware of not exposing sensitive data
            var error = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = $"Data added to the exception: {creditCartNumber}"
            };

            return View(error);
        }
    }
}
