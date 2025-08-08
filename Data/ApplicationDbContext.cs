using EmsalEWayBillSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmsalEWayBillSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<EWayBill> EWayBills { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure relationships and indexes
            modelBuilder.Entity<EWayBill>()
                .HasIndex(e => e.EwayBillNumber)
                .IsUnique();
                
            modelBuilder.Entity<EWayBill>()
                .HasIndex(e => e.InvoiceId);
                
            modelBuilder.Entity<EWayBill>()
                .HasIndex(e => e.Status);
                
            modelBuilder.Entity<Invoice>()
                .HasIndex(e => e.InvoiceNumber)
                .IsUnique();
                
            modelBuilder.Entity<Invoice>()
                .HasIndex(e => e.TripId);
                
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.EwayBill)
                .WithOne(e => e.Invoice)
                .HasForeignKey<Invoice>(i => i.EwayBillId);
                
            modelBuilder.Entity<InvoiceItem>()
                .HasIndex(i => i.InvoiceId);
                
            modelBuilder.Entity<Trip>()
                .HasIndex(t => t.TripNumber)
                .IsUnique();
        }
    }
}