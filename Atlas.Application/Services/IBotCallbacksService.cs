using System;
using System.Threading.Tasks;

namespace Atlas.Application.Services
{
    public interface IBotCallbacksService
    {
        Task<string> UpdateStatusAsync(int telegramUserId, bool isDevVersionBot, Guid orderId, int status);
    }
}

