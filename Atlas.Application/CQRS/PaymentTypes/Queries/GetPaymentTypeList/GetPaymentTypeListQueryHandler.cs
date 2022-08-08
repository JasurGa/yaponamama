using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.PaymentTypes.Queries.GetPaymentTypeList
{
    public class GetPaymentTypeListQueryHandler : IRequestHandler<GetPaymentTypeListQuery,
        PaymentTypeListVm>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public GetPaymentTypeListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PaymentTypeListVm> Handle(GetPaymentTypeListQuery request,
            CancellationToken cancellationToken)
        {
            var paymentTypes = await _dbContext.PaymentTypes
                .ProjectTo<PaymentTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PaymentTypeListVm { PaymentTypes = paymentTypes };
        }
    }
}
