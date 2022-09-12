using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Categories.Queries.GetCategoryChildren;
using Atlas.Application.CQRS.Goods.Queries.GetGoodListByCategory;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Neo4j.Driver;

namespace Atlas.Application.CQRS.Goods.Queries.GetTopGoods
{
    public class GetTopGoodsQueryHandler : IRequestHandler<GetTopGoodsQuery,
        TopGoodListVm>
    {
        private readonly IDriver         _driver;
        private readonly IMapper         _mapper;
        private readonly IMediator       _mediator;
        private readonly IAtlasDbContext _dbContext;

        public GetTopGoodsQueryHandler(IDriver driver, IMapper mapper, IMediator mediator, IAtlasDbContext dbContext) =>
            (_driver, _mapper, _mediator, _dbContext) = (driver, mapper, mediator, dbContext);

        public async Task<TopGoodListVm> Handle(GetTopGoodsQuery request,
            CancellationToken cancellationToken)
        {
            var result = new List<TopGoodDetailsVm>();

            var childCategories = await _mediator.Send(new GetCategoryChildrenQuery
            {
                Id          = request.CategoryId,
                ShowDeleted = false
            });

            var session = _driver.AsyncSession();
            try
            {
                foreach (var c in childCategories.Categories)
                {
                    var goodIds = new List<Guid>();
                    var cursor = await session.RunAsync("MATCH (m:Category{Id: $Id})<-[:BELONGS_TO]-(g:Good) RETURN rand() as r, g.Id ORDER BY r", new
                    {
                        Id = c.Id.ToString()
                    });

                    var records = await cursor.ToListAsync();
                    foreach (var record in records)
                    {
                        goodIds.Add(Guid.Parse(record[1].As<string>()));
                    }

                    var goods = await _dbContext.Goods
                        .Where(x => goodIds.Contains(x.Id))
                        .ProjectTo<GoodLookupDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                    result.Add(new TopGoodDetailsVm
                    {
                        Category = c,
                        Goods    = goods
                    });
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return new TopGoodListVm
            {
                Categories = result
            };
        }
    }
}
