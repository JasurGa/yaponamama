using System;
using System.Threading.Tasks;
using Atlas.Application.Interfaces;
using Atlas.Payme.MerchantApi.Enums;
using Atlas.Payme.MerchantApi.Exceptions;
using Atlas.Payme.MerchantApi.Models;
using Atlas.Payme.MerchantApi.Services;
using EdjCase.JsonRpc.Router;
using EdjCase.JsonRpc.Router.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Payme.MerchantApi.Controllers
{
    public class MerchantController : RpcController
    {
        private readonly IMerchantService _merchantService;

        public MerchantController(IMerchantService merchantService) =>
            _merchantService = merchantService;

        public async Task<IRpcMethodResult> CheckPerformTransaction(int amount, AccountDto account)
        {
            try
            {
                var result = await _merchantService.CheckPerformTransaction(amount, account);
                return Ok(result);
            }
            catch (OrderNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongOrderId, "Wrong order id", "order");
            }
            catch (IncorrectAmountException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongAmount, "Wrong amount!", "amount");
            }
        }

        public async Task<IRpcMethodResult> CreateTransaction(string id, ulong time, int amount, AccountDto account)
        {
            try
            {
                var result = await _merchantService.CreateTransaction(id, time, amount, account);
                return Ok(result);
            }
            catch (OrderNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongOrderId, "Wrong order id", "order");
            }
            catch (IncorrectAmountException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongAmount, "Wrong amount!");
            }
            catch (UnableCompleteException)
            {
                return Error((int)PaycomRpcErrorCodes.UnableComplete, "Unable to complete the operation!");
            }
        }

        public async Task<IRpcMethodResult> PerformTransaction(string id)
        {
            try
            {
                var result = await _merchantService.PerformTransaction(id);
                return Ok(result);
            }
            catch (TransactionNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongTransactionId, "Wrong transaction id!");
            }
            catch (UnableCompleteException)
            {
                return Error((int)PaycomRpcErrorCodes.UnableComplete, "Unable to complete the operation!");
            }
            catch (OrderNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongOrderId, "Wrong order id", "order");
            }
        }

        public async Task<IRpcMethodResult> CancelTransaction(string id, int reason)
        {
            try
            {
                var result = await _merchantService.CancelTransaction(id, reason);
                return Ok(result);
            }
            catch (TransactionNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongTransactionId, "Wrong transaction id!");
            }
            catch (UnableCancelTransactionException)
            {
                return Error((int)PaycomRpcErrorCodes.UnableComplete, "Unable to cancel the operation!");
            }
        }

        public async Task<IRpcMethodResult> CheckTransaction(string id)
        {
            try
            {
                var result = await _merchantService.CheckTransaction(id);
                return Ok(result);
            }
            catch (TransactionNotFoundException)
            {
                return Error((int)PaycomRpcErrorCodes.WrongTransactionId, "Wrong transaction id!");
            }
        }

        public async Task<IRpcMethodResult> GetStatement(ulong from, ulong to)
        {
            var result = await _merchantService.GetStatement(from, to);
            return Ok(result);
        }
    }
}