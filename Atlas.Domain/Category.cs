﻿using System;

namespace Atlas.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameRu { get; set; }

        public string NameEn { get; set; }

        public string NameUz { get; set; }

        public string ImageUrl { get; set; }

        public bool IsMainCategory { get; set; }

        public bool IsDeleted { get; set; }
    }
}
