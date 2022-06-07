using System;
using System.Collections.Generic;

namespace Atlas.Domain
{
    public class GeneralCategory
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Category> Categories { get; set; }

        public bool IsDeleted { get; set; }
    }
}
