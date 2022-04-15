using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Models
{
    public class ChefsAndDishesContext : DbContext
    {
        public ChefsAndDishesContext(DbContextOptions options) : base(options) {}

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Chef> Chefs { get; set; }
    }
}