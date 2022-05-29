

using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using GalleryConcept.Helpers;
using GalleryConcept.Models;
using iText.Html2pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrintNodeNet;

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
                Name = "Siódemka i nuty w teorii Newtona",
                Description = @"Isaac Newton w swoich pierwszych wykładach optycznych wspomina tylko o pięciu kolorach. Są nimi: czerwony, żółty, zielony, niebieski i fioletowy. W książce Optics, wydanej dużo później, bo dopiero w 1704 roku, zdecydował się na podział na siedem kolorów. Do już istniejących pięciu dodał pomarańczowy i indygo. Czy dodanie tych dwóch kolorów było aż tak ważne?
                                Wszystko rozbija się o teorię harmonii. 
                                Newton musiał ją znać – została ona skrupulatnie opisana w książce Johannesa Keplera Harmonia światów. (Książka zawiera w sobie trzecie prawo Keplera, które posłużyło jako podwaliny pod trzecią zasadę dynamiki, z której tak znany jest Newton.) Sam podkreśla w swoich rozważaniach, że nie wybrał siedmiu kolorów, ponieważ lepiej wyjaśniają jego teorię, lecz dlatego, że prawdopodobnie sugerują analogię pomiędzy harmonią barw, a harmonią dźwięków, w której występuje dokładnie siedem oktaw od A do G.
                                Należy dodać, że liczba siedem występuje bardzo często w naszej historii. Przypisuje się jej nawet specjalne właściwości. Psycholog Alex Bellos twierdzi, że siódemka jest ulubioną liczbą ludzi. Liczba strzelców w filmie Siedmiu wspaniałych nie jest przypadkiem. Ile mamy grzechów głównych? Ile dni ma tydzień?"
            },
            new Exhibit
            {
                Id = 2,
                Name = "Czym jest powidok?",
                Description = @"Powidok, czyli kontrast następczy, to zjawisko optyczne pojawiające się po dłuższym wpatrywaniu się w pewien kształt o jednolitym kolorze. Gdy przesuniemy wzrok pojawia się zamazana figura w kolorze dopełniającym. Wrażenie powidoku utrzymuje się przez chwilę, a następnie znika.
                                Przykładem kontrastu następczego jest zielone kółko, które znalazło się na tej wystawie. Po około 30 sekundach intensywnego wpatrywania się w nie, zobaczymy kolor, którego szukamy. Elementy siatkówki odpowiedzialne za odbiór barwy zielonej uległy zmęczeniu, przez co oko nie jest w stanie przez krótki czas wygenerować prawidłowych sygnałów na nowej powierzchni. Jest to rodzaj powidoku negatywnego, w którym tworzy się kolor dopełniający.
                                Istnieje również powidok pozytywny. W tym przypadku nie dochodzi do zmiany kolorów. Utrzymują się barwy z oryginalnego obrazu. Powidok ten jest dobrze znany, ale jego dokładne działanie nie jest wyjaśnione. Może mieć to związek z bezwładnością siatkówki. Powidok pozytywny jest rzadziej zauważalny, mimo iż występuje dość często. Spowodowane jest to długością jego trwania. Powidok pozytywny może trwać zaledwie 500 milisekund, co powoduje, że go nie zauważamy.
                                "

            },
            new Exhibit
            {
                Id = 3,
                Name = "Bardziej szczegółowa historia",
                Description = @"William Perkin, urodzony w Londynie, syn stolarza, rozpoczął studia chemiczne w wieku 15 lat u znanego uczonego Augusta Wilhelma von Hofmanna. Pomagał on swojemu profesorowi w odnalezieniu syntetycznej chininy, która miała posłużyć jako lekarstwo na malarię. Zamiast tego młody chemik przez przypadek odkrywa moweinę, pierwszy syntetyczny barwnik anilinowy o zabarwieniu czerwono-fioletowym. Odkrycie to jest ważnie nie tylko dla rozwoju barwników syntetycznych, ale również dla rozwoju przemysłowej chemii organicznej, która w dzisiejszych czasach dostarcza nam materiałów wybuchowych, polimerów ai przede wszystkim farmaceutyków. Barwnik, który odkrył został nazwany potocznie fioletem tyryjskim lub fioletem Perkina. Jak się okazuje londyńczyk nie był tylko utalentowanym chemikiem, był również błyskotliwym przedsiębiorcą. Lubił również malarstwo, co pomogło mu w zrozumieniu i znalezieniu zastosowania dla substancji, która pozornie była bezużyteczna. W zaciszu domowym przeprowadza próby farbowania jedwabiu, utrzymując wszystko w tajemnicy przed Hofmannem. Następnie rozesłał próbki do wielu farbiarni czekając na pozytywne reakcje i zamówienia tego nowego barwnika.
                                Osiemnastoletni Perkin patentuje sposób wytwarzania moweiny w sierpniu 1856 roku. Szybko zaczyna sprzedawać syntetyczny barwnik. Niski koszt i szybki czas produkcji sprawiają, że moweina jako barwnik szybko zaczyna dominować na rynku. Naturalne fioletowe barwniki, które były wytwarzane w bardzo skomplikowany i czasochłonny sposób nie mogą obronić się w obliczu niskiej ceny moweiny. Dzięki zmysłowi do interesów Perkin w błyskawiczny sposób dorobił się fortuny.
                                Odkrycie budzi nie lada zaciekawienie wśród chemików. W całej Europie zaczęto prowadzić badania z zamiarem wynalezienia nowych barwników, które można uzyskać z aniliny i odkrycia innych sposobów uzyskania moweiny tak, by ominąć patent Perkina. Jednocześnie cały czas trwały badania nad uzyskaniem syntetycznej chininy i zsyntetyzowaniem aminy.
                                I tu wreszcie dochodzimy do polskiego wątku w naszej historii. W 1856 roku Jakub Natanson, warszawski chemik studiujący na Uniwersytecie w Dorpacie, przeprowadza eksperyment. Podgrzewa anilinę do temperatury 200°C, w wyniku czego otrzymuje maź w kolorze krwistej czerwieni. Niestety uznaje eksperyment za nieudany. Nie dostrzega możliwości stworzenia barwnika. Odkrywa jeden ze sposobów produkcji magenty, jednak nie uświadamia sobie potencjału tego wynalazku.
                                Już wcześniej wspomniany Hofmann również bierze udział w pogoni za nowymi sztucznymi kolorami. Analizuje on prace Polaka i francuskiego chemika F.S. Cloeza. Przeprowadza serię eksperymentów, w których wspaniały szkarłatny kolor poddaje obróbce termicznej tworząc czarną maź. W swoim raporcie z badań stwierdza z przykrością, że pomimo wielu wysiłków nie udało mu się uzyskać nowej substancji, która nadawałaby się do analizy i dalszego badania. Rezygnuje z kolejnych badań.
                                W końcu, w 1859 roku, francuski chemik Francois-Emmanuel Verguin użył chlorku cynowego jako alternatywnego środka utleniającego w metodzie Perkina i uzyskał nowy kolor: długo oczekiwaną magentę. Francuz szybko sprzedał proces wytwarzania braciom Renard z Lyonu, którzy szybko, bo już 8 kwietnia 1859 roku, opatentowali proces wytwarzania barwnika. Nazwali nowy kolor fuksyną, nawiązując do koloru kwiatu fuksji. Ale nazwa ta może skrywać również grę słowną, ponieważ reynard po francusku to lis, a po niemiecku to Fuchs – mogło to być nawiązanie do tego, że bracia byli przekonani że przechytrzyli resztę chemików w wyścigu o patent w produkcji nowego barwnika.
                                W tym samym roku dwóch byłych uczniów Hofmanna: Edward Chambers Nichols i Henry Medlock ulepszyli proces byłego nauczyciela zamieniając jeden składnik na kwas arszenikowy. Dzięki temu uzyskali inną metodę otrzymywania tego samego barwnika co bracia Renard. Nowy kolor nazwali rozeiną. Osobno zgłosili swoje patenty: Nichols 25 stycznia 1859, a Medlock 18 stycznia 1859. Rozpoczęli również współpracę z firmą Simpson & Co, która dzięki temu stała się drugą co do wielkości w Anglii firmą produkującą barwniki ustępującą tylko firmie samego Perkina – wynalazcy moweiny.
                                Sama magenta, zwana wtedy jeszcze fuksyną lub rozeiną, jako barwnik okazała się rewelacją w świecie mody, zdobywając szybko wielką popularność. Spowodowało to spór między braćmi Renard a firmą Simpsona. Francuzi uważali, że metoda Anglików jest pochodną ich sposobu wytwarzania fuksyny i wystąpili na drogę sądową. W tej sprawie bardzo ważną rolę odegrał raport sporządzony przez Hofmanna z jego, jak dotychczas sądził, nieudanych badań z roku poprzedzającego odkrycie metody Francuzów. Był to dowód na to, że Anglicy bazowali zupełnie na innych badaniach i nie inspirowali się odkryciem Francuzów. Jednak wydany w trakcie procesu przez Hofmanna artykuł sugerujący, że to Anglikom powinno przypisać się wszelkie zasługi w odkryciu tego barwnika miał negatywny wpływ na wyrok. Ostatecznie w 1863 roku sąd francuski przyznał rację braciom Renard. A sąd angielski przyznał rację firmie Simpson & Co.
                                Ostatnią rzeczą, której poświęcimy uwagę będzie kwestia nazwania tego koloru magentą. Po stronie francuskiej nazywano go fuksyną, a po angielskiej przez pewien czas był znany jako rozeina. Kto wymyślił, żeby nazwać ten kolor od nazwy wioski pod Mediolanem, gdzie w 1859 roku odbyła się bitwa, z której zwycięską ręką wyszli Francuzi? Prawdopodobnie tą osobą był sam Simpson, który stwierdził, że w ówczesnym świecie zdominowanym przez język francuski nazwa ta będzie się lepiej sprzedawać i pomoże w zwiększeniu zysków. A może był to ukłon w stronę francuskiej strony, z którą walczyli w sądzie o prawa do procesu wytwarzania tego jakże ciekawego koloru? To już są moje domysły poparte jedynie szczątkowymi informacjami, które próbowałem połączyć w całość, by przedstawić tę elektryzującą historię.
                                "

            },
            new Exhibit
            {
                Id = 4,
                Name = "Złośliwa, zła i brzydka",
                Description = @"Rewolucja w kolorze niepokoiła, wielu krytyków, artystów, a przede wszystkim… ogrodników. Zanieczyszczenie powietrza zaczęło być dużym problemem nie tylko z powodu zagrożenia zdrowia, ale i zmiany koloru zachodów słońca oraz stłumienia naturalnych barw przez smog. Według książki o życiu Ruskina w 1880 roku widoczność słońca była ograniczona aż o 60% z powodu zanieczyszczeń powietrza, które przekraczały przynajmniej dwukrotnie wszystkie przyjęte normy. John Ruskin był jedną z pierwszych osób, które zaczęły kojarzyć magentę z negatywnymi skutkami rewolucji przemysłowej. Uważał on, że kolor ten zdominował ogrodnictwo, modę i inne dziedziny życia. Według Ruskina kolor ten był stosowany nierozważnie i przesadnie, a fabryki wytwarzające go zanieczyszczały niebo, tworząc szarą chmurę smogu, która zniekształcała kolory całego miasta. Uważał on, że chmura ta była stworzona z dusz martwych ludzi.
                                Według amerykańskiej pisarki Alice Morse Earle magenta jest kolorem złośliwym, sztucznym produktem chemicznym, symbolem wulgarności nowych sztucznych barwników. W książce Old Time Gardens autorka zwraca uwagę na postrzeganie danego koloru w zależności od epoki. Według niej przed rewolucją przemysłową magenta była postrzegana jako radosny kolor między purpurą a różowym i nikomu nie przeszkadzała. Jednak w obecnych czasach (w trakcie rewolucji przemysłowej) najchętniej by go usunęła ze wszystkich roślin i zamieniła na różowy, by uniknąć negatywnych skojarzeń, jakie kolor ten wywoływał przez wiele lat prowadzonej na niego nagonki. Inni pisarze unikali całkowicie pisania o tym kolorze. Przykładem tego jest książka Louise Yeomans King Dobrze przemyślany ogród (1915), w której autorka celowo przy opisie wielu kolorów pomija magentę.
                                Jak widać kolor ten, obecnie niewywołujący u zwykłego zjadacza chleba żadnych skojarzeń, zwłaszcza negatywnych, w dobie rewolucji przemysłowej był kojarzony z negatywnymi skutkami zmian zachodzących w społeczeństwie. Dlaczego akurat magenta została tak potraktowana? Odpowiedź na to pytanie może być prostsza niż nam się wydaje. Mianowicie, był to jedyny barwnik, którego wcześniej nie wytwarzano w naturalny sposób. Był zatem czymś najbardziej wyróżniającym się. Został więc skojarzony z nowymi technologiami, których do tej pory świat nie znał i nie widział. Nie przysłużyła mu się na pewno także masowa produkcja tego barwnika. Szybko stał się bardzo popularnym kolorem i stracił na swojej wyjątkowości. Kwiaty w tym kolorze, które też były uprawiane na masową skalę, dzięki nowym technologiom jeszcze bardziej przyczyniły się do negatywnego odbioru magenty. Na szczęście w obecnych czasach osobom niewtajemniczonym w świat barw i ogrodów kolor ten może co najwyżej kojarzyć się futurystycznie. Niekoniecznie nawet może łączyć go z rewolucją przemysłową, lecz z prostym faktem, że jest to dość nowy kolor w palecie barw, bo używany dopiero od 1859 roku! 
                                "

            },
            new Exhibit
            {
                Id = 5,
                Name = "Niesamowita krewetka i inne zwierzęta",
                Description = @"Zwierzęta postrzegają świat w różnych kolorach. Na przykład oczy motyli są zbudowane inaczej niż ludzkie, dzięki czemu widzą one dodatkowe pasmo widma – ultrafioletowe. Nasze ulubione zwierzęta domowe – psy również widzą inaczej, ponieważ mają w oczach tylko dwa czopki odpowiedzialne za widzenie dwóch kolorów: żółtego i niebieskiego, dlatego świat postrzegają w barwach znanej piosenki zespołu 2 plus 1 Chodź, pomaluj mój świat.
                                Zwierzęciem o najciekawszych oczach jest krewetka modliszkowa. Każde jej oko potrafi symultaniczne generować trzy obrazy i interpretować je w tym samym czasie. Dodatkowo posiada ona największą w całym królestwie zwierząt liczbę pałeczek odpowiedzialnych za widzenie różnych fal elektromagnetycznych. Na przykład psy posiadają dwie pałeczki, natomiast krewetka modliszkowa może ich mieć od 12 do 16. Niestety w żaden sposób nie możemy sobie wyobrazić palety barw, które może widzieć to niesamowite zwierzę. Dzięki sześciu obrazom na raz, na których może skupić się jednocześnie jest jednym z najniebezpieczniejszych drapieżników w przyrodzie.
                                "

            },
            new Exhibit
            {
                Id = 6,
                Name = "Dlaczego artyści niechętnie korzystali z magenty?",
                Description = @"Co było przyczyną niechęci artystów do magenty? Teorie dotyczące koloru i koła barw omijały magentę szerokim łukiem. Wszyscy twórcy, nieważne z jakiej szkoły, uczyli się kół barw, w których nie było magenty koloru. Klasyczne pigmenty wciąż dominowały w obrazach. Barwa ta nie pasowała w żaden sposób do opanowanych przez nich schematów mieszania i nakładania farb. Jednak nawet artyści próbujący trzymać się starych sprawdzonych metod muszą w pewnym momencie ugiąć się w obliczu zmieniającego się świata. Magenta zaczęła powoli przenikać do świata sztuki dzięki portretom. Znanymi malarzami, którzy jako pierwsi użyli magenty, są: Paul Gauguin, William-Adolphe Bouguereau czy John Humphreys Johnston. Są to właśnie portrety kobiet w strojach o barwie magenty.
                                Przełom nastąpił dopiero w 1905 roku, kiedy to magenta zaczęła pojawiać się częściej wśród artystów reprezentujących nowy nurt sztuki zwanym fowizmem. Nurt ten charakteryzował się bardzo bogatą i niestandardową kolorystyką. Artyści w kręgach tego nurtu nie stworzyli manifestu ani programu. Chcieli za to całkowitej swobody i zerwania z naśladowaniem natury. Fowiści czerpali inspiracje z dzieł Van Gogha, Gauguina i innych impresjonistów. Najczęściej tworzyli pejzaże przy użyciu płaskich plam i nierealistycznych kolorów. Najbardziej znanym przedstawicielem tej grupy był Henri Matisse. W tym przypadku łatwo wyjaśnić dlaczego malarze sięgali po magentę. Jeśli ktoś chciał znaleźć kolor, który w dobie rewolucji przemysłowej był przeciwieństwem natury, nie musiał szukać daleko.
                                "
            },
            new Exhibit
            {
                Id = 7,
                Name = "Odsłuchaj w różnych językach",
                Description = @"Oto TOP 10 najpopularniejszych języków na świecie. Przesłuchaj jak w każdym z nich brzmi nazwa magenty",
                HasAudio = true,
                AudioFiles = new List<string>
                {
                    "Angielski", "Arabski", "Bengalski", "Francuski", "Hindi", "Hiszpański", "Indonezyjski", "Mandarynski", "Portugalski", "Rosyjski"
                }
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
        public IActionResult Zwiedzanie(bool validationError = false)
        {
            // var exhibitId = string.Empty;
            // if (Request.Query.Any())
            // {
            //     exhibitId = Request.Query?.First().Value;  
            // }
            
            var chosenExhibits = new List<string>();
            if (Request.Cookies["chosenExhibits"] is not null)
            {
                chosenExhibits = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
            }

            // if (string.IsNullOrWhiteSpace(exhibitId) == false)
            // {
            //     if (chosenExhibits?.Contains(exhibitId) == false)
            //     {
            //         chosenExhibits.Add(exhibitId);
            //     }
            // }
                
                
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddHours(48);
            Response.Cookies.Append("chosenExhibits", JsonConvert.SerializeObject(chosenExhibits), options);

            foreach (var exhibit in Exhibits.Where(exhibit => chosenExhibits.Contains(exhibit.Id.ToString())))
            {
                exhibit.IsSelected = true;
            }
            
            var model = new ZwiedzanieViewModel
            {
                Exhibits = Exhibits,
                ValidationError = validationError
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

            try
            {
                PrintNodeConfiguration.ApiKey = _configuration["Apikey"];
                var printer = await PrintNodePrinter.GetAsync(Convert.ToInt64(_configuration["PrinterId"]));
            
                var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
                var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();
            
                var infoPath = Path.Combine(_hostingEnvironment.WebRootPath, "documents", $"wystawa.pdf");
                var infoPdfDocument = System.IO.File.ReadAllBytes(infoPath);
                
                var infoPrintJob = new PrintNodePrintJob
                {
                    Title = "Informacje o wystawie",
                    Content = Convert.ToBase64String(infoPdfDocument),
                    ContentType = "pdf_base64"
                };
            
                await printer.AddPrintJob(infoPrintJob);
                
                
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
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
                
                _logger.LogInformation(PrintNodeConfiguration.ApiKey);
                _logger.LogInformation(_configuration["PrinterId"]);
            }
            return BadRequest();
        }

        [HttpGet("email")]
        public async Task<IActionResult> SendEmail(string userEmail)
        {
            if (Request.Cookies["chosenExhibits"] is null)
            {
                return BadRequest();
            }

            try
            {
                var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
                var chosenExhibitsToPrint = Exhibits.Where(x => exhibitsFromCookies.Contains(x.Id.ToString())).ToList();
                
                // var emailToUse = MailboxAddress.Parse(userEmail);
            // var ourEmail = MailboxAddress.Parse("twoj-ekatalog@cotozakolor.pl");
            // using var smtp = new SmtpClient();
            // await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            // await smtp.AuthenticateAsync("twoj-ekatalog@cotozakolor.pl", "vxdeetucjffgchwg");
            // await smtp.SendAsync(new MimeMessage
            // {
            //     From = { ourEmail },
            //     To = { emailToUse },
            //     Subject = "Wybrane Eksponaty",
            //     Body = new TextPart(TextFormat.Plain) { Text = "Dziekujemy" }
            // });
            // await smtp.DisconnectAsync(true);
            
            
            // var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            // {
            //     UseDefaultCredentials = false,
            //     Port = 587,
            //     Credentials = new NetworkCredential("twoj-ekatalog@cotozakolor.pl", "vxdeetucjffgchwg"),
            //     EnableSsl = true
            // });
            
            var sender = new SmtpSender(() => new SmtpClient("smtp.gmail.com")
            {
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential("twoj-ekatalog@cotozakolor.pl", _configuration["MailPass"]),
                EnableSsl = true,
            });
            
            Email.DefaultSender = sender;
            
            var email = Email.From("twoj-ekatalog@cotozakolor.pl")
                .To(userEmail)
                .Subject("Wybrane Eksponaty").Body("Dziekujemy");
            
            email.AttachFromFilename(Path.Combine(_hostingEnvironment.WebRootPath, "documents", $"wystawa.pdf"),
                "application/pdf", "Informacje o wystawie");
            foreach (var exhibit in chosenExhibitsToPrint)
            {
                email.AttachFromFilename(Path.Combine(_hostingEnvironment.WebRootPath, "documents", $"{exhibit.Id}.pdf"),
                    "application/pdf", exhibit.Name);
            }
            
            await email.SendAsync();
            return View();
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
                _logger.LogInformation(e.StackTrace);
                
                _logger.LogInformation(_configuration["MailPass"]);
            }
            
            return BadRequest();
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
        
        [HttpPost("Send")]
        public async Task<IActionResult> Send(IFormCollection data, string? id)
        {
            var isValid = true;
            try
            {
                var exhibitsFromCookies = JsonConvert.DeserializeObject<List<string>>(Request.Cookies["chosenExhibits"]);
                if (Request.Cookies["chosenExhibits"] == null || exhibitsFromCookies.Any() == false || data.ContainsKey("email") == false || string.IsNullOrWhiteSpace(data["email"].ToString()))
                {
                    isValid = false;
                }
            }
            catch (Exception e)
            {
                isValid = false;
            }

            if (isValid == false)
            {
                return RedirectToAction(nameof(Zwiedzanie), new { validationError = true });
            }
            
            
            return RedirectToAction(nameof(SendEmail), new { userEmail = data["email"] });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}