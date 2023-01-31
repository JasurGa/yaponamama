using Atlas.OFD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.OFD.Abstractions
{
    public interface IOfdClient
    {
        Task AuthorizeAsync();

        Task RefreshTokenAsync();

        Task SendAsync(Receipt receipt);
    }
}
