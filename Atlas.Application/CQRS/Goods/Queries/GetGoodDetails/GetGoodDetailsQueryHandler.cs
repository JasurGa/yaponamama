using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Atlas.Application.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Atlas.Application.Common.Exceptions;
using Atlas.Domain;

namespace Atlas.Application.CQRS.Goods.Queries.GetGoodDetails
{
    public class GetGoodDetailsQueryHandler : IRequestHandler<GetGoodDetailsQuery,
        GoodDetailsVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetGoodDetailsQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<GoodDetailsVm> Handle(GetGoodDetailsQuery request, CancellationToken cancellationToken)
        {
            var good = await _dbContext.Goods.FirstOrDefaultAsync(x =>
                x.Id == request.Id,cancellationToken);

            if (good == null)
            {
                throw new NotFoundException(nameof(Good), request.Id);
            }

            return _mapper.Map<GoodDetailsVm>(good);
        }
    }
}
