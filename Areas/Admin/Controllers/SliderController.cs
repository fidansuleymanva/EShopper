using EShopper.Helpers;
using EShopper.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShopper.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;

        public SliderController(DataContext dataContext, IWebHostEnvironment env)
        {
            _dataContext = dataContext;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliderList = _dataContext.Slider.ToList();
            return View(sliderList);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.FromFile.ContentType != "image/png" && slider.FromFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                return View();
            }
            if (slider.FromFile.Length > 3145728)
            {
                ModelState.AddModelError("ImageFile", "It can be 3 Mb!");
                return View();
            }
            slider.Img = FileManager.SaveFile(_env.WebRootPath, "uploads/sliders", slider.FromFile);
            _dataContext.Slider.Add(slider);
            _dataContext.SaveChanges();
            return RedirectToAction("index");



        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slider = _dataContext.Slider.Find(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            Slider existslider = _dataContext.Slider.Find(slider.Id);
            if (existslider == null) return NotFound();
            if (slider.FromFile != null)
            {

                if (slider.FromFile.ContentType != "image/png" && slider.FromFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "But it can be png and jpeg!");
                    return View();
                }
                if (slider.FromFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "It can be 3 Mb!");
                    return View();
                }

                string name = FileManager.SaveFile(_env.WebRootPath, "uploads/sliders", slider.FromFile);
                FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders", existslider.Img);
                existslider.Img = name;
            }
            existslider.Title = slider.Title;
            existslider.Description = slider.Description;
            existslider.URL = slider.URL;
            existslider.Order = slider.Order;
            _dataContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Slider slider = _dataContext.Slider.Find(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Slider slider = _dataContext.Slider.Find(id);
            if (slider == null) return NotFound();
            if (slider.Img != null)
            {
                FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders", slider.Img);
            }

            _dataContext.Slider.Remove(slider);
            _dataContext.SaveChanges();
            return Ok();
        }
    }
}
