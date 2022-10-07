using System;
using Atlas.Payme.MerchantApi.Models;
using EdjCase.JsonRpc.Router.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Payme.MerchantApi.Exceptions;
using Atlas.Domain;
using Atlas.Application.Enums;
using InfluxDB.Client.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Atlas.Payme.MerchantApi.Services
{
    public interface IMerchantService
    {
        Task<CheckPerformTransactionResult> CheckPerformTransaction(int amount, AccountDto account);

        Task<CreateTransactionResult> CreateTransaction(string id, ulong time, int amount, AccountDto account);

        Task<PerformTransactionResult> PerformTransaction(string id);

        Task<CancelTransactionResult> CancelTransaction(string id, int reason);

        Task<CheckTransactionResult> CheckTransaction(string id);

        Task<GetStatementResult> GetStatement(ulong from, ulong to);
    }
}

