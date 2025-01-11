using Invoice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Services.Interface
{
    public interface IInvoiceService
    {
        Task<List<InvoiceModel>> GetAllInvoices();
        Task<InvoiceModel> CreateInvoice(InvoiceModel invoice);
        Task<InvoiceModel> UpdateInvoiceAmount(int invoiceId, double amount);
        //Task<IEnumerable<InvoiceModel>> ProcessOverdueInvoicesAsync(double lateFee, int overdueDays);
        Task ProcessOverdueInvoicesAsync(double lateFee, int overdueDays);
    }
}
