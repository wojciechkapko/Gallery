using GalleryConcept.Models;
using Microsoft.AspNetCore.Mvc;
using PrintNodeNet;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using GalleryConcept.Helpers;
using iText.Html2pdf;
using iText.Kernel.Pdf;
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
                Name = "Liczba siedem i nuty w teorii Newtona",
                Description = @"Newton w swoich pierwszych wykładach optycznych wspomina tylko o pięciu kolorach. Są nimi kolejno: czerwony, żółty, zielony, niebieski i fioletowy. W książce Optics wydanej dużo później, bo dopiero w 1704 roku, zdecydował się na podział na aż siedem kolorów. Do już istniejących pięciu dodał pomarańczowy i indygo. Czy dodanie tych dwóch kolorów było aż tak ważne? Pomarańczowy faktycznie można dostrzec gołym okiem, jednak indygo bardzo płynnie miesza się z niebieskim i fioletowym w sposób niemal niezauważalny dla ludzkiego oka. Czy mamy tu jakąś teorie spiskową? Dlaczego sir Isaacowi zależało tak bardzo na tym, żeby spektrum posiadało siedem kolorów? Wszystko rozbija się o teorię harmonii, której był zwolennikiem. Musiał być z nią zapoznany, ponieważ została ona skrupulatnie opisana w książce pod tytułem Harmonia światów napisanej przez Johannesa Keplera. Zawiera ona bowiem trzecie prawo Keplera, które posłużyło jako podwaliny pod trzecią zasadę dynamiki, z której tak znany jest Newton. Sam podkreśla w swoich rozważaniach, że nie wybrał siedmiu kolorów z powodu tego, że lepiej wyjaśniają jego teorię, a z powodu tego, że prawdopodobnie sugerują analogię pomiędzy harmonią kolorów, a harmonią dźwięków, w której występuje siedem oktaw od A do G. Należy dodać, że liczba siedem występuje bardzo często w naszej historii. Przypisuje się jej nawet specjalne właściwości. Psycholog Alex Bellos twierdzi, że liczba siedem jest ulubioną liczbą ludzi. Liczba strzelców w filmie Siedmiu wspaniałych nie jest przypadkiem. Ile dni ma tydzień? Ile mamy grzechów głównych?"
            },
            new Exhibit
            {
                Id = 2,
                Name = "Czym jest powidok",
                Description = @"Newton w swoich pierwszych wykładach optycznych wspomina tylko o pięciu kolorach. Są nimi kolejno: czerwony, żółty, zielony, niebieski i fioletowy. W książce Optics wydanej dużo później, bo dopiero w 1704 roku, zdecydował się na podział na aż siedem kolorów. Do już istniejących pięciu dodał pomarańczowy i indygo. Czy dodanie tych dwóch kolorów było aż tak ważne? Pomarańczowy faktycznie można dostrzec gołym okiem, jednak indygo bardzo płynnie miesza się z niebieskim i fioletowym w sposób niemal niezauważalny dla ludzkiego oka. Czy mamy tu jakąś teorie spiskową? Dlaczego sir Isaacowi zależało tak bardzo na tym, żeby spektrum posiadało siedem kolorów? Wszystko rozbija się o teorię harmonii, której był zwolennikiem. Musiał być z nią zapoznany, ponieważ została ona skrupulatnie opisana w książce pod tytułem Harmonia światów napisanej przez Johannesa Keplera. Zawiera ona bowiem trzecie prawo Keplera, które posłużyło jako podwaliny pod trzecią zasadę dynamiki, z której tak znany jest Newton. Sam podkreśla w swoich rozważaniach, że nie wybrał siedmiu kolorów z powodu tego, że lepiej wyjaśniają jego teorię, a z powodu tego, że prawdopodobnie sugerują analogię pomiędzy harmonią kolorów, a harmonią dźwięków, w której występuje siedem oktaw od A do G. Należy dodać, że liczba siedem występuje bardzo często w naszej historii. Przypisuje się jej nawet specjalne właściwości. Psycholog Alex Bellos twierdzi, że liczba siedem jest ulubioną liczbą ludzi. Liczba strzelców w filmie Siedmiu wspaniałych nie jest przypadkiem. Ile dni ma tydzień? Ile mamy grzechów głównych?"

            },
            new Exhibit
            {
                Id = 3,
                Name = "Liczba siedem i nuty w teorii Newtona",
                Description = @"Newton w swoich pierwszych wykładach optycznych wspomina tylko o pięciu kolorach. Są nimi kolejno: czerwony, żółty, zielony, niebieski i fioletowy. W książce Optics wydanej dużo później, bo dopiero w 1704 roku, zdecydował się na podział na aż siedem kolorów. Do już istniejących pięciu dodał pomarańczowy i indygo. Czy dodanie tych dwóch kolorów było aż tak ważne? Pomarańczowy faktycznie można dostrzec gołym okiem, jednak indygo bardzo płynnie miesza się z niebieskim i fioletowym w sposób niemal niezauważalny dla ludzkiego oka. Czy mamy tu jakąś teorie spiskową? Dlaczego sir Isaacowi zależało tak bardzo na tym, żeby spektrum posiadało siedem kolorów? Wszystko rozbija się o teorię harmonii, której był zwolennikiem. Musiał być z nią zapoznany, ponieważ została ona skrupulatnie opisana w książce pod tytułem Harmonia światów napisanej przez Johannesa Keplera. Zawiera ona bowiem trzecie prawo Keplera, które posłużyło jako podwaliny pod trzecią zasadę dynamiki, z której tak znany jest Newton. Sam podkreśla w swoich rozważaniach, że nie wybrał siedmiu kolorów z powodu tego, że lepiej wyjaśniają jego teorię, a z powodu tego, że prawdopodobnie sugerują analogię pomiędzy harmonią kolorów, a harmonią dźwięków, w której występuje siedem oktaw od A do G. Należy dodać, że liczba siedem występuje bardzo często w naszej historii. Przypisuje się jej nawet specjalne właściwości. Psycholog Alex Bellos twierdzi, że liczba siedem jest ulubioną liczbą ludzi. Liczba strzelców w filmie Siedmiu wspaniałych nie jest przypadkiem. Ile dni ma tydzień? Ile mamy grzechów głównych?"

            },
            new Exhibit
            {
                Id = 4,
                Name = "Czym jest powidok",
                Description = @"Newton w swoich pierwszych wykładach optycznych wspomina tylko o pięciu kolorach. Są nimi kolejno: czerwony, żółty, zielony, niebieski i fioletowy. W książce Optics wydanej dużo później, bo dopiero w 1704 roku, zdecydował się na podział na aż siedem kolorów. Do już istniejących pięciu dodał pomarańczowy i indygo. Czy dodanie tych dwóch kolorów było aż tak ważne? Pomarańczowy faktycznie można dostrzec gołym okiem, jednak indygo bardzo płynnie miesza się z niebieskim i fioletowym w sposób niemal niezauważalny dla ludzkiego oka. Czy mamy tu jakąś teorie spiskową? Dlaczego sir Isaacowi zależało tak bardzo na tym, żeby spektrum posiadało siedem kolorów? Wszystko rozbija się o teorię harmonii, której był zwolennikiem. Musiał być z nią zapoznany, ponieważ została ona skrupulatnie opisana w książce pod tytułem Harmonia światów napisanej przez Johannesa Keplera. Zawiera ona bowiem trzecie prawo Keplera, które posłużyło jako podwaliny pod trzecią zasadę dynamiki, z której tak znany jest Newton. Sam podkreśla w swoich rozważaniach, że nie wybrał siedmiu kolorów z powodu tego, że lepiej wyjaśniają jego teorię, a z powodu tego, że prawdopodobnie sugerują analogię pomiędzy harmonią kolorów, a harmonią dźwięków, w której występuje siedem oktaw od A do G. Należy dodać, że liczba siedem występuje bardzo często w naszej historii. Przypisuje się jej nawet specjalne właściwości. Psycholog Alex Bellos twierdzi, że liczba siedem jest ulubioną liczbą ludzi. Liczba strzelców w filmie Siedmiu wspaniałych nie jest przypadkiem. Ile dni ma tydzień? Ile mamy grzechów głównych?"

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
            return View();
        }

        [HttpGet("exhibit/{id}")]
        public IActionResult ExhibitDetails(int id)
        {
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", $"{id}.jpg");
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            var image = Convert.ToBase64String(imageArray);
            var exhibit = Exhibits.FirstOrDefault(x => x.Id.Equals(id));

            var model = new ExhibitDetailsViewModel()
            {
                Id = id,
                Name = exhibit.Name,
                Image = image,
                Description = exhibit.Description
            };
            
            return View(model);
        }

        [HttpGet("zwiedzanie")]
        public IActionResult Zwiedzanie()
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
                if (chosenExhibits?.Contains(exhibitId) == false)
                {
                    chosenExhibits.Add(exhibitId);
                }
            }
                
                
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(48);
            Response.Cookies.Append("chosenExhibits", JsonConvert.SerializeObject(chosenExhibits), options);

            foreach (var exhibit in Exhibits.Where(exhibit => chosenExhibits.Contains(exhibit.Id.ToString())))
            {
                exhibit.IsSelected = true;
            }
            
            var model = new ZwiedzanieViewModel
            {
                Exhibits = Exhibits
            };


            return View(model);
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
            
            var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();
            
            foreach (var exhibit in chosenExhibitsToPrint)
            {
                var path = Path.Combine(_hostingEnvironment.WebRootPath, "documents", $"{exhibit.Id}.pdf");
                var pdfDocument = System.IO.File.ReadAllBytes(path);
                
                var printJob = new PrintNodePrintJob
                {
                    Title = exhibit.Name,
                    Content = Convert.ToBase64String(pdfDocument),
                    ContentType = "pdf_base64"
                };
            
                await printer.AddPrintJob(printJob);
            }
            
            return View();
        }

        [HttpGet("email")]
        public async Task<IActionResult> SendEmail([FromQuery] string userEmail)
        {
            if (Request.Cookies["chosenExhibits"] is null)
            {
                return BadRequest();
            }
            
            PrintNodeConfiguration.ApiKey = _configuration["Apikey"];
            var printer = await PrintNodePrinter.GetAsync(Convert.ToInt64(_configuration["PrinterId"]));
            
            var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();


            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("markscodingspot@gmail.com", "replace-password"),
                EnableSsl = true,
            });

            Email.DefaultSender = sender;
            
            var email = Email.From("twokknmwt@twokknmwt.com")
                .To(userEmail)
                .Subject("Wybrane Eksponaty").Body("Dziekujemy");
            
            foreach (var exhibit in chosenExhibitsToPrint)
            {
                email.AttachFromFilename(Path.Combine(_hostingEnvironment.WebRootPath, "documents", $"{exhibit.Id}.pdf"),
                    "application/pdf", exhibit.Name);
            }

            await email.SendAsync();
            return View();
        }

        public byte[] ExportToPDF(string viewName, int id)
        {
            
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "img", $"{id}.jpg");
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            var image  = Convert.ToBase64String(imageArray);

            var chosenExhibitToPrint = Exhibits.FirstOrDefault(x => x.Id.Equals(id));
            
            var model = new ExhibitDetailsViewModel()
            {
                Image = image,
                Description = chosenExhibitToPrint.Description,
                Id = chosenExhibitToPrint.Id,
                Name = chosenExhibitToPrint.Name
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