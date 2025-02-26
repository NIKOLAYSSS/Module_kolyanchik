
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1.Models
{
    public class Partner
    {
        public int PartnerID { get; set; }
        public int PartnerTypeID { get; set; }
        public PartnerType PartnerType { get; set; }  // Добавлено свойство PartnerType
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LegalAddress { get; set; }
        public string INN { get; set; }
        public int? Rating { get; set; }  // Обновлено на nullable int?
    }
}
