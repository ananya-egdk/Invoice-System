using Invoice.Data.Entity;

namespace Invoice.Data.Repository.Interface
{
    public interface IInvoiceRepository
    {
        Task<InvoiceEntity> GetInvoiceByIdAsync(int invoiceId);
        Task<List<InvoiceEntity>> GetAllInvoicesAsync();
        Task<InvoiceEntity> CreateInvoiceAsync(InvoiceEntity invoice);
        Task UpdateInvoiceAsync(InvoiceEntity invoice);
    }
}
