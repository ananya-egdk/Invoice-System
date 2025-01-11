namespace Invoice.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Paid_amount { get; set; }
        public DateTime Due_date { get; set; }
        public string Status { get; set; } = "pending";
    }
}
