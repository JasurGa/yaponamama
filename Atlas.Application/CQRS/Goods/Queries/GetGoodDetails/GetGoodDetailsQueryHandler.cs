using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Atlas.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;
using Atlas.Domain;
using Neo4j.Driver;
using System.Collections.Generic;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryList;
using Atlas.Application.Common.Helpers;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GetGoodDetailsQueryHandler : IRequestHandler<GetGoodDetailsQuery,
        GoodDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IDriver         _driver;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodDetailsQueryHandler(IMapper mapper, IDriver driver, IAtlasDbContext dbContext) =>
            (_mapper, _driver, _dbContext) = (mapper, driver, dbContext);

        public async Task<GoodDetailsVm> Handle(GetGoodDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.Include(x => x.Provider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            var categories = new List<CategoryLookupDto>();

            var session = _driver.AsyncSession();
            try
            {
                var cursor = await session.RunAsync("MATCH (c:Category{IsDeleted: false})<-[:BELONGS_TO]-(p:Good{Id: $Id}) OPTIONAL MATCH (c)<-[:BELONGS_TO]-(ch:Category{IsDeleted: false}) OPTIONAL MATCH (c)<-[:BELONGS_TO*]-(g:Good) RETURN {ImageUrl: c.ImageUrl, IsDeleted: c.IsDeleted, Id:c.Id, IsMainCategory: c.IsMainCategory, Name: c.Name, ChildCategoriesCount: COUNT(DISTINCT ch), GoodsCount: COUNT(DISTINCT g)} AS r", new
                {
                    Id = request.Id.ToString(),
                });

                categories = _mapper.Map<List<Category>, List<CategoryLookupDto>>(
                    await cursor.ConvertDictManyAsync<Category>());
            }
            finally
            {
                await session.CloseAsync();
            }

            return _mapper.Map<Good, GoodDetailsVm>(good, opt =>
            {
                opt.AfterMap((src, dst) =>
                {
                    dst.Categories = categories;
                });
            });
        }
    }
}
