using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using aspnet2.Models;

namespace aspnet2.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarServiceContext dbContext;

        public HomeController(CarServiceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string brand, int invoice = 0, int page = 1,
            SortState sortOrder = SortState.InvoiceAsc)
        {
            int pageSize = 3;

            IQueryable<Invoice> invoices = dbContext.Invoices.Include(x => x.Car);

            if (invoice != 0)
            {
                invoices = invoices.Where(p => p.InvoiceId == invoice);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                if (brand != "All")
                    invoices = invoices.Where(p => p.Car.Brand!.Contains(brand));
            }

            switch (sortOrder)
            {
                case SortState.InvoiceDesc:
                    invoices = invoices.OrderByDescending(s => s.InvoiceId);
                    break;
                case SortState.BrandAsc:
                    invoices = invoices.OrderBy(s => s.Car!.Brand);
                    break;
                case SortState.BrandDesc:
                    invoices = invoices.OrderByDescending(s => s.Car!.Brand);
                    break;
                default:
                    invoices = invoices.OrderBy(s => s.InvoiceId);
                    break;
            }

            var count = await invoices.CountAsync();
            var items = await invoices.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new FilterViewModel(dbContext.Cars.ToList(), brand, invoice),
                new SortViewModel(sortOrder)
            );
            return View("~/Views/Invoice/Index.cshtml", viewModel);
        }
    }
}
