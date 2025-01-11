using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Data.Dto
{
    public class CreateInvoiceDto
    {
        public double amount { get; set; }
        public DateTime due_date { get; set; }
    }
}
