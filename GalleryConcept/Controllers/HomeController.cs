using GalleryConcept.Models;
using Microsoft.AspNetCore.Mvc;
using PrintNodeNet;
using System.Diagnostics;
using GalleryConcept.Helpers;
using iText.Html2pdf;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GalleryConcept.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewRender _viewRender;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger,
            IViewRender viewRender,
            IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _logger = logger;
            _viewRender = viewRender;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Test()
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", "1.jpeg");
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            
            var model = new TestViewModel
            {
                Name = "test name",
                Image = base64ImageRepresentation
            };
            
            return View(model);
        }
        [HttpGet("print/{url}")]
        public async Task<IActionResult> Print(string url)
        {
            PrintNodeConfiguration.ApiKey = _configuration["Apikey"];
            var printer = await PrintNodePrinter.GetAsync(Convert.ToInt64(_configuration["PrinterId"]));
            var printJob = new PrintNodePrintJob
            {
                Title = "Hello, world!",
                Content = Convert.ToBase64String(ExportToPDF("Home/Test")),
                ContentType = "pdf_base64"
            };
            
            await printer.AddPrintJob(printJob);

            return Ok();
        }

        public byte[] ExportToPDF(string viewName)
        {
            //Task<byte[]> 
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", "1.jpeg");
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            
            var model = new TestViewModel
            {
                Name = "test name",
                Image = base64ImageRepresentation
            };
            var bodyTxt = _viewRender.RenderPartialViewToString(viewName, model);

            using MemoryStream ms = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(bodyTxt));
            using (MemoryStream pdfDest = new MemoryStream())
            {
                ConverterProperties converterProperties = new ConverterProperties();
                HtmlConverter.ConvertToPdf(ms, pdfDest, converterProperties);

                return pdfDest.ToArray();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}