using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.GeneralCategories.Queries.GetGeneralCategories
{
    public class GeneralCategoriesListVm
    {
        public IList<GeneralCategoryLookupDto> GeneralCategories { get; set; }
    }
}
