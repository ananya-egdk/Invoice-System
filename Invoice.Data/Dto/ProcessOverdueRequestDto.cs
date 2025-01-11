using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Data.Dto
{
    public class ProcessOverdueRequestDto
    {
        public double LateFee { get; init; }
        public int OverdueDays { get; init; }
    }
}
