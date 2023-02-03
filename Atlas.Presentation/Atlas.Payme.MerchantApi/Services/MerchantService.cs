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
using Atlas.Payme.MerchantApi.Extensions;

namespace Atlas.Payme.MerchantApi.Services
{
    public class MerchantService : IMerchantService
    {
        private long TIME_EXPIRED = 43200000;

        private readonly IAtlasDbContext _dbContext;

        public MerchantService(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CheckPerformTransactionResult> CheckPerformTransaction(int amount, AccountDto account)
        {
            Guid orderId = Guid.Empty;
            if (!Guid.TryParse(account.Order, out orderId))
            {
                throw new OrderNotFoundException();
            }
            
            var order = await _dbContext.Orders.Include(x => x.GoodToOrders).ThenInclude(x => x.Good)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            var totalPrice = order.ShippingPrice + order.SellingPrice;
            if (totalPrice * 100 != amount)
            {
                throw new IncorrectAmountException();
            }

            var isTransactionAllowed = true;
            if (order.IsPrePayed)
            {
                isTransactionAllowed = false;
            }

            return new CheckPerformTransactionResult
            {
                Allow  = isTransactionAllowed,
                Detail = new DetailsLookupDto
                {
                    Shipping = new ShippingLookupDto
                    {
                        Title = "Доставка",
                        Price = (int)order.ShippingPrice * 100,
                    },
                    Items = order.GoodToOrders.Select(x => new ItemLookupDto
                    {
                        Title       = x.Good.NameRu,
                        Price       = x.Good.SellingPrice * 100,
                        Count       = x.Count,
                        Code        = x.Good.CodeIkpu,
                        VatPercent  = x.Good.SaleTaxPercent,
                        PackageCode = x.Good.PackageCode
                    })
                }
            };
        }

        public async Task<CreateTransactionResult> CreateTransaction(string id, ulong time, int amount, AccountDto account)
        {
            Guid orderId = Guid.Empty;
            if (!Guid.TryParse(account.Order, out orderId))
            {
                throw new OrderNotFoundException();
            }

            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x =>
                x.PaycomId == id);

            if (transaction == null)
            {
                var allowObj = await CheckPerformTransaction(amount, account);
                if (allowObj.Allow)
                {
                    var newTransaction = new Transaction
                    {
                        Id           = Guid.NewGuid(),
                        OrderId      = orderId,
                        PaycomId     = id,
                        PaycomTime   = time,
                        PaycomAmount = amount,
                        CreatedAt    = DateTime.UtcNow,
                        CanceledAt   = DateTime.UnixEpoch,
                        PerformedAt  = DateTime.UnixEpoch,
                        State        = (int)TransactionStatus.STATE_IN_PROGRESS
                    };

                    await _dbContext.Transactions.AddAsync(newTransaction);
                    _dbContext.SaveChanges();

                    return new CreateTransactionResult
                    {
                        CreateTime  = newTransaction.CreatedAt.ToUnixTime(),
                        Transaction = newTransaction.Id.ToString(),
                        State       = newTransaction.State,
                    };
                }
            }
            else
            {
                if (transaction.State == (int)TransactionStatus.STATE_IN_PROGRESS)
                {
                    if (DateTime.UtcNow.ToUnixTime() - ((long)transaction.PaycomTime) > TIME_EXPIRED)
                    {
                        throw new UnableCompleteException();
                    }
                    else
                    {
                        return new CreateTransactionResult
                        {
                            CreateTime  = transaction.CreatedAt.ToUnixTime(),
                            Transaction = transaction.Id.ToString(),
                            State       = transaction.State
                        };
                    }
                }
                else
                {
                    throw new UnableCompleteException();
                }
            }

            throw new UnableCompleteException();
        }

        public async Task<PerformTransactionResult> PerformTransaction(string id)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x =>
                x.PaycomId == id);

            if (transaction == null)
            {
                throw new TransactionNotFoundException();
            }

            if (transaction.State == (int)TransactionStatus.STATE_IN_PROGRESS)
            {
                if (DateTime.UtcNow.ToUnixTime() - ((long)transaction.PaycomTime) > TIME_EXPIRED)
                {
                    transaction.State = (int)TransactionStatus.STATE_CANCELED;
                    _dbContext.SaveChanges();
                    throw new UnableCompleteException();
                }
                else
                {
                    var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                        x.Id == transaction.OrderId);

                    order.IsPrePayed = true;

                    transaction.State       = (int)TransactionStatus.STATE_DONE;
                    transaction.PerformedAt = DateTime.UtcNow;

                    _dbContext.SaveChanges();

                    return new PerformTransactionResult
                    {
                        Transaction = transaction.Id.ToString(),
                        PerformTime = transaction.PerformedAt.ToUnixTime(),
                        State       = transaction.State
                    };
                }
            }
            else if (transaction.State == (int)TransactionStatus.STATE_DONE)
            {
                return new PerformTransactionResult
                {
                    Transaction = transaction.Id.ToString(),
                    PerformTime = transaction.PerformedAt.ToUnixTime(),
                    State       = transaction.State
                };
            }

            throw new UnableCompleteException();
        }

        public async Task<CancelTransactionResult> CancelTransaction(string id, int reason)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x =>
                x.PaycomId == id);

            if (transaction == null)
            {
                throw new TransactionNotFoundException();
            }

            if (transaction.State == (int)TransactionStatus.STATE_IN_PROGRESS)
            {
                transaction.State = (int)TransactionStatus.STATE_CANCELED;
            }
            else if (transaction.State == (int)TransactionStatus.STATE_DONE)
            {
                var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                    x.Id == transaction.OrderId);

                if (!order.CanRefund || order.IsRefunded)
                {
                    throw new UnableCancelTransactionException();
                }
                else
                {
                    order.IsRefunded  = true;
                    transaction.State = (int)TransactionStatus.STATE_POST_CANCELED;
                }
            }
            else
            {
                transaction.State = (int)TransactionStatus.STATE_CANCELED;
            }

            transaction.CanceledAt = DateTime.UtcNow;
            transaction.Reason     = reason;

            _dbContext.SaveChanges();

            return new CancelTransactionResult
            {
                Transaction = transaction.Id.ToString(),
                CancelTime  = transaction.CanceledAt.ToUnixTime(),
                State       = transaction.State
            };
        }

        public async Task<CheckTransactionResult> CheckTransaction(string id)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(x =>
                x.PaycomId == id);

            if (transaction == null)
            {
                throw new TransactionNotFoundException();
            }

            return new CheckTransactionResult
            {
                CancelTime  = transaction.CanceledAt.ToUnixTime(),
                CreateTime  = transaction.CreatedAt.ToUnixTime(),
                PerformTime = transaction.PerformedAt.ToUnixTime(),
                Transaction = transaction.Id.ToString(),
                Reason      = transaction.Reason,
                State       = transaction.State
            };
        }

        public async Task<GetStatementResult> GetStatement(ulong from, ulong to)
        {
            var transactions = await _dbContext.Transactions.Where(x =>
                from < x.PaycomTime && x.PaycomTime < to).ToListAsync();

            return new GetStatementResult
            {
                Transactions = transactions.Select(x => new GetStatementResultLookupDto
                {
                    Id      = x.PaycomId,
                    Time    = x.PaycomTime,
                    Amount  = x.PaycomAmount,
                    Account = new AccountDto
                    {
                        Order = x.OrderId.ToString()
                    },
                    CreateTime  = x.CreatedAt.ToUnixTime(),
                    PerformTime = x.PerformedAt.ToUnixTime(),
                    CancelTime  = x.CanceledAt.ToUnixTime(),
                    Transaction = x.Id.ToString(),
                    State       = x.State,
                    Reason      = x.Reason,
                    Receivers   = null
                }).ToList()
            };
        }
    }
}

