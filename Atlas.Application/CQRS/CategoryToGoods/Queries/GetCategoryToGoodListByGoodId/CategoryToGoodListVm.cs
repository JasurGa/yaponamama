﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.CategoryToGoods.Queries.GetCategoryToGoodListByGoodId
{
    public class CategoryToGoodListVm
    {
        public List<CategoryToGoodLookupDto> CategoryToGoods { get; set; }
    }
}