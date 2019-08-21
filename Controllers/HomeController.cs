using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
            .Include(chef => chef.CreatedDishes)
            .ToList();

            return View(AllChefs);
        }

        [HttpGet("new")]
        public IActionResult New()
        {
            return View("NewChef");
        }

        [HttpPost("newChef")]
        public IActionResult newChef(Chef newChef)
        {
            if(ModelState.IsValid)
            {
                DateTime today = DateTime.Today;
                int age = today.Year - newChef.Date_Birth.Year;
                if (newChef.Date_Birth.Date > today.AddYears(-age))
                {
                    age --;
                }
                newChef.age= age;
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }else{
                return View("NewChef");
            }
        }

        [HttpGet("dishes")]
        public IActionResult dishes()
        {
            List<Dish> allDishes = dbContext.Dishes
            .Include(dish => dish.Creator)
            .ToList();
            return View("Dishes", allDishes);
        }

        [HttpGet("dishes/new")]
        public IActionResult newDish()
        {
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.Chefs = AllChefs;
            return View("NewDish");
        }

        [HttpPost("createDish")]
        public IActionResult createDish(Dish newDish)
        {
            List<Chef> AllChefs = dbContext.Chefs.ToList();
            ViewBag.AllChefs = AllChefs;
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }else{
                return View("NewDish");
            }
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
