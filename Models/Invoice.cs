using System.ComponentModel.DataAnnotations.Schema;

namespace aspnet2.Models
{
    public class Invoice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        public string CarVINcode { get; set; } = null!;

        public virtual Car Car { get; set; } = null!;
    }
}
