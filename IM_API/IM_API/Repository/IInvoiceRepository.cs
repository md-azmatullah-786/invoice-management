using IM_API.Models;

namespace IM_API.Repository
{
    public interface IInvoiceRepository
    {
        public Invoice GetInvoiceDetailsByInvoiceId(int invoiceId);
        public List<Invoice> GetInvoiceList(InvoiceFilter invoiceFilter);
        public bool SaveInvoice(Invoice invoice);
        public bool UpdateInvoice(Invoice invoice);
    }
}
