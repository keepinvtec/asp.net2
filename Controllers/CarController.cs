using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using aspnet2.Models;

namespace aspnet2.Controllers
{
    public class CarController : Controller
    {
        private readonly CarServiceContext dbContext;

        public CarController(CarServiceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(new CarModel(dbContext.Cars.ToList()));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            dbContext.Cars.Add(car);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Car");
        }

        public async Task<IActionResult> Edit(string? vincode)
        {
            if (!String.IsNullOrEmpty(vincode))
            {
                Car? car = await dbContext.Cars.FirstOrDefaultAsync(p => p.VINcode == vincode);
                if (car != null)
                    return View(car);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            dbContext.Cars.Update(car);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Car");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string? vincode)
        {
            if (!String.IsNullOrEmpty(vincode))
            {
                Car? car = await dbContext.Cars.FirstOrDefaultAsync(p => p.VINcode == vincode);
                if (car != null)
                {
                    dbContext.Cars.Remove(car);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Car");
                }
            }
            return NotFound();
        }
    }
}
