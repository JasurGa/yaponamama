using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Domain
{
    public class Correction
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid StoreToGoodId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int Count { get; set; }

        public string CauseBy { get; set; }
 
        public StoreToGood StoreToGood { get; set; }

        public User User { get; set; }
    }
}
