namespace Invoice.Data.Dto
{
    public class CreateInvoiceDto
    {
        public double Amount { get; set; }
        public DateOnly Due_date { get; set; }
    }
}
