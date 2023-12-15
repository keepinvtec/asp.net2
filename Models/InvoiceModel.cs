using Microsoft.AspNetCore.Mvc.Rendering;

namespace aspnet2.Models
{
    public class InvoiceModel
    {
        public InvoiceModel(List<Car> cars, Invoice invoice)
        {
            Cars = new SelectList(cars, "VINcode", "VINcode");
            this.invoice = invoice;
        }
        public SelectList Cars { get; }

        public Invoice invoice { get; }
    }
}
