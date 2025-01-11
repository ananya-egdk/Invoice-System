using Invoice.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
