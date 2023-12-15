using CallCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using ServerService.Models;
using System.Diagnostics;

namespace CallCenter.Controllers
{
    
    public class HomeController : Controller
    {
        HubConnection? _hubConnection;

        public double Latitude = 10.762679;
        public double Longitude = 106.682586;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Latitude"] = Latitude;
            ViewData["Longitude"] = Longitude;
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
        [HttpPost]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var order = new DatXe
                {
                    KhTen = Request.Form["fullname"].ToString(),
                    KhPhone = Request.Form["tel"].ToString(),
                    DxDiadiemdon = Request.Form["address"].ToString(),
                    DxGpsLat = (decimal) Latitude,
                    DxGpsLon = (decimal) Longitude,
                };
                var message = JsonConvert.SerializeObject(order, Formatting.Indented);
                await _hubConnection.InvokeAsync("SendOrder", message);
                ViewData["Latitude"] = Latitude;
                ViewData["Longitude"] = Longitude;
                ViewData["address"] = Request.Form["address"];
                return View("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("Index");
            }
        }
    }
}