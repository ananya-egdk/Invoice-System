using Invoice.Models;

namespace Invoice.Services.Interface
{
    public interface IInvoiceService
    {
        Task<List<InvoiceModel>> GetAllInvoices();
        Task<InvoiceModel> CreateInvoice(InvoiceModel invoice);
        Task<InvoiceModel> UpdateInvoiceAmount(int invoiceId, double amount);
        Task ProcessOverdueInvoicesAsync(double lateFee, int overdueDays);
    }
}
