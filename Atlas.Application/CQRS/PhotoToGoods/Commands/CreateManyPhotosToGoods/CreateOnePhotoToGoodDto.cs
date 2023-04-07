using System;

namespace Atlas.Application.CQRS.PhotoToGoods.Commands.CreateManyPhotosToGoods
{
    public class CreateOnePhotoToGoodDto
    {
        public Guid GoodId { get; set; }

        public string PhotoPath { get; set; }
    }
}