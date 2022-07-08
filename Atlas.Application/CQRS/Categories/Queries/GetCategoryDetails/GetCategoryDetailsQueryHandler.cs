using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;
using Atlas.Application.Helpers;

namespace Atlas.Application.CQRS.Categories.Queries.GetCategoryDetails
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery,
        CategoryDetailsVm>
    {
        private readonly IMapper _mapper;
        private readonly IDriver _driver;

        public GetCategoryDetailsQueryHandler(IMapper mapper, IDriver driver) =>
            (_mapper, _driver) = (mapper, driver);

        public async Task<CategoryDetailsVm> Handle(GetCategoryDetailsQuery request,
            CancellationToken cancellationToken)
        {
            Category category;

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{Id: $Id}) RETURN c", new
                {
                    Id = request.Id.ToString()
                });

                category = await cursor.ConvertAsync<Category>();
            }
            finally
            {
                await session.CloseAsync();
            }

            return _mapper.Map<Category, CategoryDetailsVm>(category);
        }
    }
}
