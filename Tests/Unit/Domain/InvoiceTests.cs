using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NUnit.Framework;

namespace Tests.Domain
{
    [TestFixture]
    class InvoiceTests
    {
        [Test]
        public void CanCreateInvoice()
        {
            const string address = "Elsweg 14";
            
            var invoice = new Invoice(address);

            Assert.AreEqual(address, invoice.Address);
        }

        [Test]
        [ExpectedException(typeof (Exception))]
        public void CannotCreateInvalidInvoice()
        {
            var invoice = new Invoice(null);
        }
    }
}
