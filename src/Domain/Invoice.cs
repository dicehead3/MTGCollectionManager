using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Invoice
    {
        private string _address;
        private readonly IList<InvoiceLine> _invoiceLines = new List<InvoiceLine>();

        public Invoice(string address)
        {
            Address = address;
        }

        public string BankAccountNumber { get; set; }

        public IList<InvoiceLine> InvoiceLines
        {
            get { return _invoiceLines; }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Address is required");
                }
                _address = value;
            }
        }
    }
}
