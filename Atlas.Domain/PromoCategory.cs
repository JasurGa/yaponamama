using System;
namespace Atlas.Domain
{
    public class PromoCategory
    {
        public Guid Id { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }
    }
}

