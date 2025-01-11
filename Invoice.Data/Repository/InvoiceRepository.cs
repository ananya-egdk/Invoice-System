using Invoice.Data.Entity;
using Invoice.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext invoiceDb;

        public InvoiceRepository(InvoiceDbContext invoiceDb)
        {
            this.invoiceDb = invoiceDb;
        }

        public async Task<InvoiceEntity> CreateInvoiceAsync(InvoiceEntity invoice)
        {
            await invoiceDb.Invoices.AddAsync(invoice);
            await invoiceDb.SaveChangesAsync();
            return invoice;
        }

        public async Task<List<InvoiceEntity>> GetAllInvoicesAsync()
        {
            var invoiceData = await invoiceDb.Invoices.ToListAsync();
            if (invoiceData == null)
                return null;
            return invoiceData;
        }

        public async Task<InvoiceEntity> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoiceData = await invoiceDb.Invoices.FirstOrDefaultAsync(i => i.id == invoiceId);
            if (invoiceData == null)
                return null;
            return invoiceData;
        }

        public async Task UpdateInvoiceAsync(InvoiceEntity invoice)
        {
            await invoiceDb.SaveChangesAsync();
        }
    }
}
