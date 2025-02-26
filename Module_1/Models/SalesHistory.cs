using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1.Models
{
    public class SalesHistory
    {
        public int SaleID { get; set; }
        public int PartnerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
