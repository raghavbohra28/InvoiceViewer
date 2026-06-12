using Microsoft.EntityFrameworkCore;
using static BuggyApp.Controllers.InvoiceController;

namespace BuggyApp.Data
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("InvoiceItems");
        }
    }

    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string? CustomerName { get; set; }
    }
}