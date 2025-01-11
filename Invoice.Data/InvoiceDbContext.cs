using Invoice.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {
        }

        public DbSet<InvoiceEntity> Invoices { get; set; }
    }
}
