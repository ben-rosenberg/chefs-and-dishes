using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ChefsAndDishes.Models;


namespace ChefsAndDishes.Controllers
{
    public class ChefController : Controller
    {
        public ChefController(ChefsAndDishesContext context) { _db = context; }

        [HttpGet("chefs")]
        public IActionResult All()
        {
            List<Chef> Chefs = _db.Chefs.ToList();
            return View("All", Chefs);
        }

        [HttpGet("chefs/new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost("chefs/create")]
        public IActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid) { return View("New"); }

            _db.Chefs.Add(chef);
            _db.SaveChanges();
            
            return RedirectToAction("All");
        }

        private ChefsAndDishesContext _db;
    }
}