using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using souq02.Models;

namespace souq02.Controllers
{
    
    public class HomeController : Controller
    {

     

        NewsContext db;

        public HomeController(NewsContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var Result=db.Categories.ToList();

            return View(Result);
        }
        public IActionResult News(int? Id)
        {
            var query = db.News.AsQueryable();

            if (Id.HasValue)
            {
                query = query.Where(y => y.CategoryId == Id.Value);
            }

            var Result = query.OrderByDescending(x => x.Date).ToList();
            return View(Result);
        }


        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult SaveContact(ContactUs model)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(model);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Your message has been sent. Thank you!";
                return RedirectToAction("Contact");
            }

            return View("Contact", model);
        }
        public IActionResult DeleteNews(int id)
        {
            var News = db.News.Find(id);
            db.News.Remove(News);
            db.SaveChanges();
            return View();
        }


        public IActionResult Messages()
        {
            return View(db.Contacts.ToList());


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
