using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {

        private crudContext dbContext;
        public HomeController(crudContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            // List<Dish> AllDishes = dbContext.dishes.ToList();
            List<Dish> ReturnedValues = dbContext.dishes.ToList();

            return View(ReturnedValues);
        }
        [HttpGet("addDish")]
        public IActionResult addDish()
        {
            return View();
        }
        [HttpPost("createDish")]
        public IActionResult createDish(Dish newDish)
        {
            
            if(ModelState.IsValid){
                
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                return View("addDish");

            }
        }
        [HttpGet("{id}")]
        public IActionResult displayDish(int id){
            var ReturnedValues = dbContext.dishes.Where(Dish => Dish.id == id).ToList();
            return View(ReturnedValues);
        }
        [HttpGet("delete/{id}")]
        public IActionResult deleteDish(int id){
             var RetrievedUser = dbContext.dishes.SingleOrDefault(user => user.id == id);
             dbContext.dishes.Remove(RetrievedUser);
             dbContext.SaveChanges();
             return RedirectToAction("Index");
        }
        [HttpGet("edit/{id}")]
        public IActionResult editDish(int id){
            
            var EditValues = dbContext.dishes.Where(Dish => Dish.id == id).ToList();
            
            return View(EditValues[0]);                            
        }  
        [HttpPost("editDish/{id}")]
        public IActionResult editingDish(Dish editDish, int id) 
        {
            
            
            if(ModelState.IsValid){
                Dish currentDish = dbContext.dishes.FirstOrDefault(user => user.id == id);
                currentDish.chefName = editDish.chefName;
                currentDish.dishName = editDish.dishName;
                currentDish.calories = editDish.calories;
                currentDish.tasteLevel = editDish.tasteLevel;
                currentDish.description = editDish.description;
                // dbContext.Update(editDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else{
                return View("editDish", editDish);
                
            }
        }       
    }
}
