using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ChefsAndDishes.Models;

namespace ChefsAndDishes.Controllers
{
    public class DishController : Controller
    {
        public DishController(ChefsAndDishesContext context) { _db = context; }

        [HttpGet("dishes")]
        public IActionResult All()
        {
            List<Dish> Dishes = _db.Dishes
                .Include(dish => dish.Creator)
                .OrderBy(dish => dish.CreatedAt)
                .ToList();

            return View("All", Dishes);
        }
        
        [HttpGet("dishes/new")]
        public IActionResult New()
        {
            ViewBag.Chefs = _db.Chefs.ToList();
            return View("New");
        }

        [HttpPost("dishes/create")]
        public IActionResult Create(Dish newDish)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Chefs = _db.Chefs.ToList();
                return View("New");
            }

            Chef dbChef = _db.Chefs.FirstOrDefault(chef => chef.ChefId == newDish.ChefId);
            newDish.Creator = dbChef;

            _db.Dishes.Add(newDish);
            _db.SaveChanges();

            return RedirectToAction("All");
        }


        private ChefsAndDishesContext _db;
    }
}