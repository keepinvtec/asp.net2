using Microsoft.AspNetCore.Mvc.Rendering;

namespace aspnet2.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Car> cars, string brand, int invoice)
        {
            cars.Insert(0, new Car { Brand = "All" });
            Cars = new SelectList(cars, "Brand", "Brand", brand);
            SelectedBrand = brand;
            SelectedInvoice = invoice;
        }
        public SelectList Cars { get; }
        public string SelectedBrand { get; }
        public int SelectedInvoice { get; }
    }
}
