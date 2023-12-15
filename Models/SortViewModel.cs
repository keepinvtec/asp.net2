namespace aspnet2.Models
{
    public enum SortState
    {
        InvoiceAsc,
        InvoiceDesc,
        BrandAsc,
        BrandDesc
    }

    public class SortViewModel
    {
        public SortState InvoiceSort { get; }
        public SortState BrandSort { get; }
        public SortState Current { get; }

        public SortViewModel(SortState sortOrder)
        {
            InvoiceSort = sortOrder == SortState.InvoiceAsc ? SortState.InvoiceDesc : SortState.InvoiceAsc;
            BrandSort = sortOrder == SortState.BrandAsc ? SortState.BrandDesc : SortState.BrandAsc;
            Current = sortOrder;
        }
    }
}
