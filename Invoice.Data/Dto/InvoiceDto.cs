namespace Invoice.Data.Dto
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public double Paid_amount { get; set; }
        public DateOnly Due_date { get; set; }
        public string? Status { get; set; }
    }
}
