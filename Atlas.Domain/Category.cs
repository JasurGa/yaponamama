﻿using System;
namespace Atlas.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid GeneralCategoryId { get; set; }

        public GeneralCategory GeneralCategory { get; set; }

        public bool IsDeleted { get; set; }
    }
}
