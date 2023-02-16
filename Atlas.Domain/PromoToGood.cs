using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Domain
{
    public class PromoToGood
    {
        public Guid Id { get; set; }

        public Guid PromoId { get; set; }

        public Promo Promo { get; set; }

        public Guid GoodId { get; set; }

        public Good Good { get; set; }
    }
}
