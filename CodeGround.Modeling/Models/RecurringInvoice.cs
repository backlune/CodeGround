using System;
using System.Collections.Generic;
using System.Text;

namespace GodeGround.Modeling.Models
{
    public enum OccurranceType
    {
        Monthly = 0,
        Weekly = 1,
        Daily = 2,

        Yearly = 3,
        Quarterly = 4,

        Custom = 100,
    }

    public class RecurringInvoice
    {
        public RecurringInvoice()
        {
            RecurringInvoiceScheduling = new RecurringInvoiceScheduling();
        }

        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Guid? TemplateInvoiceId { get; set; }
        public Invoice TemplateInvoice { get; set; }

        public ICollection<Invoice> CreatedInvoices { get; set; }

        public RecurringInvoiceScheduling RecurringInvoiceScheduling { get; set; }

        // TODO: Delivery Method?
    }

    public class RecurringInvoiceScheduling
    {
        public DateTime StartDate { get; set; }
        public OccurranceType Occurrance { get; set; }

        public int NumberOfOccurrances { get; set; }
        public int CreateDaysBeforePublish { get; set; }
    }
}
