using System;
using MediatR;

namespace Atlas.Application.CQRS.Languages.Queries
{
    public class GetLanguageListQuery : IRequest<LanguageListVm>
    {

    }
}
