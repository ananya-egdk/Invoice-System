using AutoMapper;
using Invoice.Data.Entity;
using Invoice.Data.Repository.Interface;
using Invoice.Enums;
using Invoice.Models;
using Invoice.Services;
using Moq;

namespace Invoice.Test
{
    
    public class InvoiceServiceTests
    {
        private readonly Mock<IMapper> mockMapper = new Mock<IMapper>();
        private readonly Mock<IInvoiceRepository> mockInvoiceRepository = new Mock<IInvoiceRepository>();
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _invoiceService = new InvoiceService(mockMapper.Object, mockInvoiceRepository.Object);
        }

        [Fact]
        public async Task CreateInvoice_ShouldReturnInvoiceWithValidDetails()
        {
            // Arrange
            var invoiceModel = new InvoiceModel { Amount = 1000, Due_date = DateTime.Now.Date };
            var invoiceEntity = new InvoiceEntity { id = 1, amount = 1000, due_date = DateTime.Now.Date };

            mockMapper.Setup(m => m.Map<InvoiceEntity>(invoiceModel)).Returns(invoiceEntity);
            mockMapper.Setup(m => m.Map<InvoiceModel>(invoiceEntity)).Returns(invoiceModel);
            mockInvoiceRepository.Setup(repo => repo.CreateInvoiceAsync(invoiceEntity)).ReturnsAsync(invoiceEntity);

            // Act
            var createdInvoice = await _invoiceService.CreateInvoice(invoiceModel);

            // Assert
            Assert.NotNull(createdInvoice);
            Assert.Equal(invoiceModel.Amount, createdInvoice.Amount);
            Assert.Equal(invoiceModel.Due_date, createdInvoice.Due_date);
        }

        [Fact]
        public async Task CreateInvoice_ShouldThrowExceptionForInvalidAmount()
        {
            // Arrange
            var invoiceModel = new InvoiceModel { Amount = 0, Due_date = DateTime.Now.Date };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _invoiceService.CreateInvoice(invoiceModel));
            Assert.Equal("Amount should be greater than 0", exception.Message);
        }

        [Fact]
        public async Task CreateInvoice_ShouldThrowExceptionForPastDueDate()
        {
            // Arrange
            var invoiceModel = new InvoiceModel { Amount = 1000, Due_date = DateTime.Now.AddDays(-1).Date };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _invoiceService.CreateInvoice(invoiceModel));
            Assert.Equal("Due date cannot be in the past", exception.Message);
        }

        [Fact]
        public async Task GetAllInvoices_ShouldReturnListOfInvoices()
        {
            // Arrange
            var invoiceEntities = new List<InvoiceEntity>
        {
            new InvoiceEntity { id = 1, amount = 500, due_date = DateTime.Now.Date },
            new InvoiceEntity { id = 2, amount = 1000, due_date = DateTime.Now.Date }
        };
            var invoiceModels = new List<InvoiceModel>
        {
            new InvoiceModel { Amount = 500, Due_date = DateTime.Now.Date },
            new InvoiceModel { Amount = 1000, Due_date = DateTime.Now.Date }
        };

            mockMapper.Setup(m => m.Map<List<InvoiceModel>>(invoiceEntities)).Returns(invoiceModels);
            mockInvoiceRepository.Setup(repo => repo.GetAllInvoicesAsync()).ReturnsAsync(invoiceEntities);

            // Act
            var invoices = await _invoiceService.GetAllInvoices();

            // Assert
            Assert.NotNull(invoices);
            Assert.Equal(2, invoices.Count);
        }

        [Fact]
        public async Task GetAllInvoices_ShouldReturnEmptyListWhenNoInvoicesExist()
        {
            // Arrange
            var invoiceEntities = new List<InvoiceEntity>();
            mockInvoiceRepository.Setup(repo => repo.GetAllInvoicesAsync()).ReturnsAsync(invoiceEntities);

            // Act
            var invoices = await _invoiceService.GetAllInvoices();

            // Assert
            Assert.NotNull(invoices);
            Assert.Empty(invoices);
        }

        [Fact]
        public async Task UpdateInvoiceAmount_ShouldUpdateAmountCorrectly()
        {
            // Arrange
            var invoiceEntity = new InvoiceEntity { id = 1, amount = 500, paid_amount = 100, status = PaymentTypeEnum.Pending.ToString() };
            var updatedInvoiceModel = new InvoiceModel { Amount = 300, Paid_amount = 200, Status = PaymentTypeEnum.Pending.ToString() };

            mockInvoiceRepository.Setup(repo => repo.GetInvoiceByIdAsync(1)).ReturnsAsync(invoiceEntity);
            mockMapper.Setup(m => m.Map<InvoiceModel>(invoiceEntity)).Returns(updatedInvoiceModel);

            // Act
            var updatedInvoice = await _invoiceService.UpdateInvoiceAmount(1, 200);

            // Assert
            Assert.NotNull(updatedInvoice);
            Assert.Equal(300, updatedInvoice.Amount);
            Assert.Equal(200, updatedInvoice.Paid_amount);
        }

        [Fact]
        public async Task UpdateInvoiceAmount_ShouldThrowExceptionForInvalidAmount()
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _invoiceService.UpdateInvoiceAmount(1, -100));
            Assert.Equal("Amount should be greater than 0", exception.Message);
        }

        [Fact]
        public async Task UpdateInvoiceAmount_ShouldThrowExceptionWhenInvoiceNotFound()
        {
            // Arrange
            mockInvoiceRepository.Setup(repo => repo.GetInvoiceByIdAsync(1)).ReturnsAsync((InvoiceEntity)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _invoiceService.UpdateInvoiceAmount(1, 100));
            Assert.Equal("Invoice not found", exception.Message);
        }

        [Fact]
        public async Task ProcessOverdueInvoices_ShouldMarkInvoicesAsVoidAndCreateNewOnes()
        {
            // Arrange
            var overdueInvoice = new InvoiceEntity
            {
                id = 1,
                amount = 1000,
                paid_amount = 0,
                due_date = DateTime.Now.AddDays(-5).Date,
                status = PaymentTypeEnum.Pending.ToString()
            };
            var newInvoiceEntity = new InvoiceEntity
            {
                id = 2,
                amount = 1050,
                paid_amount = 0,
                due_date = DateTime.Now.AddDays(30).Date,
                status = PaymentTypeEnum.Pending.ToString()
            };

            var invoices = new List<InvoiceEntity> { overdueInvoice };

            mockInvoiceRepository.Setup(repo => repo.GetAllInvoicesAsync()).ReturnsAsync(invoices);

            // Act
            await _invoiceService.ProcessOverdueInvoicesAsync(50, 30);

            // Assert
            Assert.Equal(PaymentTypeEnum.Void.ToString(), overdueInvoice.status);
            mockInvoiceRepository.Verify(repo => repo.UpdateInvoiceAsync(overdueInvoice), Times.Once);
            mockInvoiceRepository.Verify(repo => repo.CreateInvoiceAsync(It.IsAny<InvoiceEntity>()), Times.Once);
        }
    }
}