using System;
using System.Threading.Tasks;
using InfluxDB.Client;

namespace Atlas.Application.Services
{
    public interface IAtlasInfluxDbService
    {
        Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action);

        void Write(Action<WriteApi> action);
    }
}
