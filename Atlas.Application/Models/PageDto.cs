using System;
using System.Collections.Generic;

namespace Atlas.Application.Models
{
    public class PageDto<TResponse> where TResponse : class
    {
        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int PageCount { get; set; }

        public IList<TResponse> Data { get; set; }
    }
}
