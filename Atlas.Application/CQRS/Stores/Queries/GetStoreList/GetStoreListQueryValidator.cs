using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Atlas.Application.CQRS.Stores.Queries.GetStoreList
{
    public class GetStoreListQueryValidator : AbstractValidator<GetStoreListQuery>
    {
        public GetStoreListQueryValidator()
        {

        }
    }
}
