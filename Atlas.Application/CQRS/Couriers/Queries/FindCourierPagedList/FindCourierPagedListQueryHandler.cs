using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Atlas.Application.CQRS.Couriers.Queries.GetCourierPagedList;
using Atlas.Application.Interfaces;
using Atlas.Application.Models;
using Atlas.Domain;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Application.CQRS.Couriers.Queries.FindCourierPagedList
{
    public class FindCourierPagedListQueryHandler : IRequestHandler<FindCourierPagedListQuery,
        PageDto<CourierLookupDto>>
    {
        private readonly IMapper         _mapper;
        private readonly IAtlasDbContext _dbContext;

        public FindCourierPagedListQueryHandler(IMapper mapper, IAtlasDbContext dbContext) =>
            (_mapper, _dbContext) = (mapper, dbContext);

        public async Task<PageDto<CourierLookupDto>> Handle(FindCourierPagedListQuery request, CancellationToken cancellationToken)
        {
            request.SearchQuery = request.SearchQuery.ToLower().Trim();

            var couriers = _dbContext.Couriers.Include(x => x.Vehicle).Include(x => x.User)
                .Where(x => x.IsDeleted == request.ShowDeleted)
                .AsQueryable();

            if (request.FilterStoreId != null)
            {
                couriers.Where(x => x.Vehicle.StoreId == request.FilterStoreId);
            }

            couriers = couriers.OrderByDescending(x => EF.Functions.TrigramsSimilarity((x.PhoneNumber + " " + x.User.Login + " " + x.User.FirstName + " " + x.User.LastName + " " + x.User.MiddleName).ToLower().Trim(),
                request.SearchQuery));

            var couriersCount = await couriers.CountAsync(cancellationToken);
            var pagedCouriers = await couriers.Skip(request.PageSize * request.PageIndex)
                .Take(request.PageSize).ToListAsync(cancellationToken);

            return new PageDto<CourierLookupDto>
            {
                PageIndex  = request.PageIndex,
                TotalCount = couriersCount,
                PageCount  = (int)Math.Ceiling((double)couriersCount / request.PageSize),
                Data       = _mapper.Map<List<Courier>, List<CourierLookupDto>>(pagedCouriers),
            };
        }
    }
}

