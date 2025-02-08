using IM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IM_API.Repository
{
    public class InvoiceRepository: IInvoiceRepository
    {
        private readonly InvoiceContext _context;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(InvoiceRepository));
        public InvoiceRepository(InvoiceContext context)
        {
            _context = context;
        }

        public Invoice GetInvoiceDetailsByInvoiceId(int invoiceId)
        {
            return _context.Invoice
                .Include(x => x.InvoiceItems)
                .Include(x => x.Payments)
                .FirstOrDefault(x => x.InvoiceId == invoiceId) ?? new Invoice();
        }
        public List<Invoice> GetInvoiceList(InvoiceFilter invoiceFilter)
        {
            List<Invoice> resData = new List<Invoice>();
            if (!string.IsNullOrWhiteSpace(invoiceFilter.InvoiceNumber))
            {
                resData = _context.Invoice.Where(x => x.InvoiceNumber == invoiceFilter.InvoiceNumber)
                    .Include(x => x.InvoiceItems)
                    .Include(x => x.Payments)
                    .Skip(invoiceFilter.Skip)
                    .Take(invoiceFilter.Take)
                    .ToList();
            }
            else
            {
                resData = _context.Invoice
                    .Include(x => x.InvoiceItems)
                    .Include(x => x.Payments)
                    .Skip(invoiceFilter.Skip)
                    .Take(invoiceFilter.Take)
                    .ToList();
            }

            return resData;
        }
        public bool SaveInvoice(Invoice invoice)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Add(invoice);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                object logMessage = ex.Message + " | " + ex.StackTrace;
                _log4net.Error(logMessage);
                transaction.Rollback();
                return false;
            }
        }
        public bool UpdateInvoice(Invoice invoice)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Update(invoice);
                _context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                object logMessage = ex.Message + " | " + ex.StackTrace;
                _log4net.Error(logMessage);
                transaction.Rollback();
                return false;
            }
        }
    }
}
