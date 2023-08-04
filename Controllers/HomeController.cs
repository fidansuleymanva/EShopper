using EShopper.Models;
using EShopper.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EShopper.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            _dataContext = dataContext;

        }
        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                sliders = _dataContext.Slider.OrderBy(x => x.Order).ToList(),
            };
            return View(homeViewModel);
        }

       
    }
}