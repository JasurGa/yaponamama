using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreateManyPhotosToGoods
{
    public class CreateManyPhotosToGoodsCommand : IRequest
    {
        public List<CreateOnePhotoToGoodDto> PhotoToGoods { get; set; }
    }
}
