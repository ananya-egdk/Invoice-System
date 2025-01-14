using AutoMapper;
using Invoice.Data;
using Invoice.Data.Dto;
using Invoice.Models;
using Invoice.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(InvoiceDbContext context, IMapper mapper, IInvoiceService invoiceService)
        {
            _context = context;
            _mapper = mapper;
            _invoiceService = invoiceService;
        }

        [HttpPost("invoices")]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceDto createInvoice)
        {
            var invoice = _mapper.Map<InvoiceModel>(createInvoice);
            var result = await _invoiceService.CreateInvoice(invoice);
            return CreatedAtAction(nameof(CreateInvoice), _mapper.Map<InvoiceAddedDto>(result));
        }

        [HttpGet("invoices")]
        public async Task<List<InvoiceDto>> GetAllInvoices()
        {
            var result = await _invoiceService.GetAllInvoices();
            return _mapper.Map<List<InvoiceDto>>(result).ToList();
        }

        [HttpPut("{id}/payments")]
        public async Task<IActionResult> PayInvoice([FromRoute] int id, PayInvoiceDto payInvoice)
        {
            var result = await _invoiceService.UpdateInvoiceAmount(id, payInvoice.Amount);
            return Ok(result);
        }

        [HttpPost("process-overdue")]
        public async Task<List<InvoiceDto>> ProcessOverdueInvoices([FromBody] ProcessOverdueRequestDto request)
        {
            await _invoiceService.ProcessOverdueInvoicesAsync(request.LateFee, request.OverdueDays);
            var result = await _invoiceService.GetAllInvoices();
            return _mapper.Map<List<InvoiceDto>>(result).ToList();
        }
    }
}
