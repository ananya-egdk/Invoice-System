namespace Invoice.Data.Dto
{
    public class ProcessOverdueRequestDto
    {
        public double LateFee { get; init; }
        public int OverdueDays { get; init; }
    }
}
