using System;
using System.Collections.Generic;

namespace Atlas.Application.CQRS.Languages.Queries
{
    public class LanguageListVm
    {
        public IList<LanguageLookupDto> Languages { get; set; }
    }
}
