using Integration.Application;
using Integration.Web.Models;
using Integration.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace Integration.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeServices _employeeServices;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IEmployeeServices employeeServices)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _employeeServices = employeeServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel homeViewModel)
        {
            var file = homeViewModel.formFile;
            string filePath = string.Empty;

            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files");
                filePath = Path.Combine(uploadFolder, fileName); // Change this to your desired directory.

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
            }

            int mudifiedRowsNumber = _employeeServices.ImportFile(filePath);

            return View(new HomeViewModel
            {
                UpdatedRowsNumber = mudifiedRowsNumber
            });
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