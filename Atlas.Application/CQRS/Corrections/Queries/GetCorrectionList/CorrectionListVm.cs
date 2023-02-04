using System.Collections.Generic;

namespace Atlas.Application.CQRS.Corrections.Queries.GetCorrectionList
{
    public class CorrectionListVm
    {
        public IList<CorrectionLookupDto> Corrections { get; set; }
    }
}
