using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductName { get; set; }
        public string Article { get; set; }
        public decimal MinimalPrice { get; set; }
    }
}
