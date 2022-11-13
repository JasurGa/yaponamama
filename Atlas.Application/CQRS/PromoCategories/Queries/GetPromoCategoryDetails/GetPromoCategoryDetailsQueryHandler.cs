using System;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Common.Exceptions;
using Atlas.Application.Interfaces;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PromoCategories.Queries.GetPromoCategoryDetails
{
    public class GetPromoCategoryDetailsQueryHandler : IRequestHandler<GetPromoCategoryDetailsQuery,
        PromoCategoryDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPromoCategoryDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PromoCategoryDetailsVm> Handle(GetPromoCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            var promoCategory = await _dbContext.PromoCategories.FirstOrDefaultAsync(x =>
                x.Id == request.Id, cancellationToken);

            if (promoCategory == null)
            {
                throw new NotFoundException(nameof(PromoCategory), request.Id);
            }

            return _mapper.Map<PromoCategory, PromoCategoryDetailsVm>(promoCategory);
        }
    }
}

