using System.ComponentModel.DataAnnotations;

namespace Invoice.Data.Entity
{
    public class InvoiceEntity
    {
        [Key]
        public int id { get; set; }
        public double amount { get; set; }
        public double paid_amount { get; set; }
        public DateTime due_date { get; set; }
        public string status { get; set; }
    }
}
