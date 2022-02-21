using GodeGround.Modeling.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGround.Modeling
{
    public class InvoiceContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<RecurringInvoice> RecurringInvoices { get; set; }


        public string DbPath { get; }

        public InvoiceContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "invoice.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder x)
        {
            x.Owned<RecurringInvoiceScheduling>();

            x.Entity<Invoice>()
               .HasOne(i => i.RecurringInvoice)
               .WithMany(r => r.CreatedInvoices)
               .OnDelete(DeleteBehavior.NoAction);

            x.Entity<Invoice>()
                .HasOne(x => x.TemplateRecurringInvoice)
                .WithOne(x => x.TemplateInvoice)
                .HasForeignKey<RecurringInvoice>(f => f.TemplateInvoiceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
