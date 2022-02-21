using System;
using System.Collections.Generic;
using System.Text;

namespace GodeGround.Modeling.Models
{
    public class Invoice
    {

        public Guid Id { get; set; }
        public DateTime DueDate { get; set; }

        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }

        public decimal Sum { get; set; }
        public decimal VatRate { get; set; }
        public decimal Vat { get; set; }
        public decimal TotalSum { get; set; }
        public decimal Rounding { get; set; }

        public Company Company { get; set; }
        public Guid CompanyId { get; set; }

        public Guid? RecurringInvoiceId { get; set; }
        public RecurringInvoice RecurringInvoice { get; set; }

        public RecurringInvoice TemplateRecurringInvoice { get; set; }
    }
}
