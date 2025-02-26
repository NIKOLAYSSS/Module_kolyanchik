using Module_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1.Repositories
{
    public interface IPartnerRepository
    {
        List<Partner> GetAllPartners();
        Partner GetPartnerById(int partnerId);
        void UpdatePartner(Partner partner);
        void AddPartner(Partner partner);
        void DeletePartner(int partnerId);
        List<PartnerType> GetPartnerTypes();
        decimal GetTotalSalesQuantity(int partnerId);
        List<SalesHistory> GetSalesHistory(int? partnerId = null);
    }
}
