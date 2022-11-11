﻿using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class PromoAdvertisePage
    {
        public Guid Id { get; set; }

        public Guid PromoAdvertiseId { get; set; }

        public PromoAdvertise PromoAdvertise { get; set; }

        public string BadgeColor { get; set; }

        public string BadgeTextRu { get; set; }

        public string BadgeTextEn { get; set; }

        public string BadgeTextUz { get; set; }

        public string TitleColor { get; set; }

        public string TitleRu { get; set; }

        public string TitleEn { get; set; }

        public string TitleUz { get; set; }

        public string DescriptionColor { get; set; }

        public string DescriptionRu { get; set; }

        public string DescriptionEn { get; set; }

        public string DescriptionUz { get; set; }

        public string ButtonColor { get; set; }

        public string Background { get; set; }

        public ICollection<PromoAdvertiseGood> PromoAdvertiseGoods { get; set; }
    }
}

