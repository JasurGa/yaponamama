using System;
using System.Threading.Tasks;

namespace Atlas.Application.Services
{
    public interface IBotCallbacksService
    {
        Task<string> UpdateStatusAsync(long telegramUserId, bool isDevVersionBot, Guid orderId, int status);

        Task<string> SendPaymentAsync(long telegramUserId, bool isDevVersionBot, Guid orderId);
    }
}

