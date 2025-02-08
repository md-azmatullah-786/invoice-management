namespace IM_API.Models
{
    public class InvoiceFilter
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
