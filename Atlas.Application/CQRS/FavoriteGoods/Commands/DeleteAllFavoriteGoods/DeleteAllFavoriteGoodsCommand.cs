using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.FavoriteGoods.Commands.DeleteAllFavoriteGoods
{
    public class DeleteAllFavoriteGoodsCommand : IRequest
    {
        public Guid ClientId { get; set; }
    }
}
