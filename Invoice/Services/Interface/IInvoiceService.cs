using Invoice.Models;

namespace Invoice.Services.Interface
{
    public interface IInvoiceService
    {
        /// <summary>
        /// This method is used to get all the invoices
        /// </summary>
        /// <returns></returns>
        Task<List<InvoiceModel>> GetAllInvoices();

        /// <summary>
        /// This method is used to create a new invoice
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        Task<InvoiceModel> CreateInvoice(InvoiceModel invoice);

        /// <summary>
        /// This method is used to update the amount of an invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<InvoiceModel> UpdateInvoiceAmount(int invoiceId, double amount);

        /// <summary>
        /// This method is used to process overdue invoices
        /// </summary>
        /// <param name="lateFee"></param>
        /// <param name="overdueDays"></param>
        /// <returns></returns>
        Task ProcessOverdueInvoicesAsync(double lateFee, int overdueDays);
    }
}
