using System;
using System.Threading.Tasks;

namespace Atlas.Eskiz.Abstractions
{
    public interface IEskizClient
    {
        Task AuthorizeAsync();

        Task RefreshTokenAsync();

        Task SendAsync(string mobilePhone, string message);
    }
}

