using System.ComponentModel.DataAnnotations;

namespace IM_API.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
    }
}
