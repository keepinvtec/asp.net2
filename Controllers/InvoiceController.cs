using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using aspnet2.Models;

namespace aspnet2.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly CarServiceContext dbContext;

        public InvoiceController(CarServiceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Create()
        {
            return View(new InvoiceModel(dbContext.Cars.ToList(), new Invoice()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Invoice invoice)
        {
            var temp = false;
            foreach (Car car in dbContext.Cars.ToList())
            {
                if (car.VINcode == invoice.CarVINcode)
                    temp = true;
            }
            if (!temp)
                invoice.Car.VINcode = invoice.CarVINcode;
            else
            {
                var temp1 = invoice.InvoiceId;
                var temp2 = invoice.CarVINcode;
                invoice = new Invoice { InvoiceId = temp1, CarVINcode = temp2 };
            }
            dbContext.Invoices.Add(invoice);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Invoice? invoice = await dbContext.Invoices.FirstOrDefaultAsync(p => p.InvoiceId == id);
                if (invoice != null)
                    return View(new InvoiceModel(dbContext.Cars.ToList(), invoice));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Invoice invoice)
        {
            dbContext.Invoices.Update(invoice);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Invoice? invoice = await dbContext.Invoices.FirstOrDefaultAsync(p => p.InvoiceId == id);
                if (invoice != null)
                {
                    dbContext.Invoices.Remove(invoice);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return NotFound();
        }
    }
}
