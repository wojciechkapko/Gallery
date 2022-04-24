using GalleryConcept.Models;
using Microsoft.AspNetCore.Mvc;
using PrintNodeNet;
using System.Diagnostics;
using GalleryConcept.Helpers;
using iText.Html2pdf;
using iText.Layout.Element;
using Newtonsoft.Json;

namespace GalleryConcept.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IViewRender _viewRender;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        private List<Exhibit> Exhibits = new List<Exhibit>
        {
            new Exhibit
            {
                Id = 1,
                Name = "ex1",
            },
            new Exhibit
            {
                Id = 2,
                Name = "ex2"
            },
            new Exhibit
            {
                Id = 3,
                Name = "ex3"
            },
            new Exhibit
            {
                Id = 4,
                Name = "ex4"
            }
        };

        public HomeController(
            ILogger<HomeController> logger,
            IViewRender viewRender,
            IWebHostEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _logger = logger;
            _viewRender = viewRender;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var exhibitId = string.Empty;
            if (Request.Query.Any())
            {
                exhibitId = Request.Query?.First().Value;  
            }

            
            var chosenExhibits = new List<string>();
            if (Request.Cookies["chosenExhibits"] is not null)
            {
                chosenExhibits = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            }

            if (string.IsNullOrWhiteSpace(exhibitId) == false)
            {
                chosenExhibits.Add(exhibitId);
            }
                
                
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(6);
            Response.Cookies.Append("chosenExhibits", JsonConvert.SerializeObject(chosenExhibits), options);
            
            Exhibits.Where(x=>chosenExhibits.Contains(x.Id.ToString())).ToList().ForEach(x=>x.IsSelected = true);
            var model = new HomeViewModel
            {
                Exhibits = Exhibits
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Test()
        {
            if (Request.Cookies["chosenExhibits"] is null)
            {
                return BadRequest();
            }
            
            var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();

            foreach (var exhibit in chosenExhibitsToPrint)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", $"{exhibit.Id}.jpg");
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                exhibit.Base64Image = Convert.ToBase64String(imageArray);
            }
            
            var model = new TestViewModel
            {
                Exhibits = chosenExhibitsToPrint
            };
            
            return View(model);
        }
        [HttpGet("print")]
        public async Task<IActionResult> Print(string url)
        {
            if (Request.Cookies["chosenExhibits"] is null)
            {
                return BadRequest();
            }
            
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
            var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();

            foreach (var exhibit in chosenExhibitsToPrint)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", $"{exhibit.Id}.jpg");
                byte[] imageArray = System.IO.File.ReadAllBytes(path);
                exhibit.Base64Image = Convert.ToBase64String(imageArray);
            }
            
            var model = new TestViewModel
            {
                Exhibits = chosenExhibitsToPrint
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