using Atlas.OFD.Abstractions;
using Atlas.OFD.Models;

namespace Atlas.OFD
{
    public class OfdClient : IOfdClient
    {
        public Task AuthorizeAsync()
        {
            throw new NotImplementedException();
        }

        public Task RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendAsync(Receipt receipt)
        {
            throw new NotImplementedException();
        }
    }
}