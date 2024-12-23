using Kogan.Domain.BusinessPartners.Enums;
using Kogan.Domain.BusinessPartners.Interfaces;
using Kogan.Domain.Common.Interfaces;
using Kogan.Domain.SAP.Interfaces;

namespace Kogan.Domain.BusinessPartners
{
    public abstract class BusinessPartner : IEntity, IBusinessPartner, ISapSynchronizable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ObjectType { get; set; }

        public string ObjectKey { get; set; }

        public BusinessPartnerTypeEnum Type { get; }

        protected BusinessPartner(BusinessPartnerTypeEnum type)
        {
            this.Type = type;
        }
    }
}
