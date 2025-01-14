using AutoMapper;
using Invoice.Data.Entity;
using Invoice.Data.Repository.Interface;
using Invoice.Enums;
using Invoice.Models;
using Invoice.Services.Interface;

namespace Invoice.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IMapper mapper;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IMapper mapper, IInvoiceRepository _invoiceRepository)
        {
            this.mapper = mapper;
            this._invoiceRepository = _invoiceRepository;
        }
        public async Task<InvoiceModel> CreateInvoice(InvoiceModel invoice)
        {
            if (invoice.Amount <= 0)
                throw new Exception("Amount should be greater than 0");
            if (invoice.Due_date < DateTime.Now.Date)
                throw new Exception("Due date cannot be in the past");

            invoice.Due_date = DateTime.SpecifyKind(invoice.Due_date, DateTimeKind.Local);
            var invoiceEntity = mapper.Map<InvoiceEntity>(invoice);
            var createdInvoice = await _invoiceRepository.CreateInvoiceAsync(invoiceEntity);
            return mapper.Map<InvoiceModel>(createdInvoice);
        }

        public async Task<List<InvoiceModel>> GetAllInvoices()
        {
            var invoiceData = await _invoiceRepository.GetAllInvoicesAsync();

            if (invoiceData == null || !invoiceData.Any())
                return new List<InvoiceModel>();

            return mapper.Map<List<InvoiceModel>>(invoiceData);
        }

        public async Task<InvoiceModel> UpdateInvoiceAmount(int invoiceId, double amount)
        {
            if (amount <= 0)
                throw new Exception("Amount should be greater than 0");

            var invoiceData = await _invoiceRepository.GetInvoiceByIdAsync(invoiceId);

            if (invoiceData == null)
                throw new Exception("Invoice not found");

            if (invoiceData.status == PaymentTypeEnum.Paid.ToString() || invoiceData.status == PaymentTypeEnum.Void.ToString())
                throw new Exception("Invoice not payable");

            if (invoiceData.amount < amount)
                throw new Exception("Amount paid should not be greater than total amount");

            invoiceData.paid_amount += amount;
            invoiceData.amount -= amount;

            if (invoiceData.amount == 0)
                invoiceData.status = PaymentTypeEnum.Paid.ToString();

            await _invoiceRepository.UpdateInvoiceAsync(invoiceData);
            return mapper.Map<InvoiceModel>(invoiceData);
        }

        public async Task ProcessOverdueInvoicesAsync(double lateFee, int overdueDays)
        {
            var invoices = await _invoiceRepository.GetAllInvoicesAsync();

            foreach (var invoice in invoices.Where(i => i.due_date.Date < DateTime.Now.Date && i.status == PaymentTypeEnum.Pending.ToString()))
            {
                if (invoice.paid_amount == 0)
                    // Invoice never paid
                    invoice.status = PaymentTypeEnum.Void.ToString();
                else if (invoice.paid_amount > 0)
                    // Partially paid invoice
                    invoice.status = PaymentTypeEnum.Paid.ToString();

                // new invoice
                var newInvoice = new InvoiceModel
                {
                    Amount = invoice.amount + lateFee,
                    Paid_amount = 0,
                    Due_date = invoice.due_date.AddDays(overdueDays),
                    Status = PaymentTypeEnum.Pending.ToString()
                };

                await CreateInvoice(newInvoice);
                await _invoiceRepository.UpdateInvoiceAsync(invoice);
            }
        }
    }
}
