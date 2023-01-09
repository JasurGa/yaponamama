using System.Collections.Generic;

namespace Atlas.Application.CQRS.PhotoToGoods.Queries.GetPhotosByGoodId
{
    public class PhotoToGoodListVm
    {
        public ICollection<PhotoToGoodLookupDto> PhotosToGoods { get; set; }
    }
}