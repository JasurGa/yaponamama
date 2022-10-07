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
    public class MerchantService : IMerchantService
    {
        private long TIME_EXPIRED = 43200000;

        private readonly IAtlasDbContext _dbContext;

        public MerchantService(IAtlasDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<CheckPerformTransactionResult> CheckPerformTransaction(int amount, AccountDto account)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x =>
                x.Id == account.Order);

            if (order == null)
            {
                throw new OrderNotFoundException();
            }

            if (order.SellingPrice * 100 != amount)
            {
                throw new IncorrectAmountException();
            }

            return new CheckPerformTransactionResult
            {
                Allow = true
            };
        }

        public async Task<CreateTransactionResult> CreateTransaction(string id, long time, int amount, AccountDto account)
        {
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
                        OrderId      = account.Order.Value,
                        PaycomId     = id,
                        PaycomTime   = time,
                        PaycomAmount = amount,
                        CreatedAt    = DateTime.UtcNow,
                        State        = (int)TransactionStatus.STATE_IN_PROGRESS
                    };

                    await _dbContext.Transactions.AddAsync(newTransaction);
                    _dbContext.SaveChanges();

                    return new CreateTransactionResult
                    {
                        CreateTime  = newTransaction.CreatedAt.Ticks,
                        Transaction = newTransaction.Id.ToString(),
                        State       = newTransaction.State,
                    };
                }
            }
            else
            {
                if (transaction.State == (int)TransactionStatus.STATE_IN_PROGRESS)
                {
                    if (DateTime.UtcNow.Ticks - transaction.PaycomTime > TIME_EXPIRED)
                    {
                        throw new UnableCompleteException();
                    }
                    else
                    {
                        return new CreateTransactionResult
                        {
                            CreateTime  = transaction.CreatedAt.Ticks,
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
                if (DateTime.UtcNow.Ticks - transaction.PaycomTime > TIME_EXPIRED)
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
                        Timestamp   = transaction.PerformedAt.Ticks,
                        State       = transaction.State
                    };
                }
            }
            else if (transaction.State == (int)TransactionStatus.STATE_DONE)
            {
                return new PerformTransactionResult
                {
                    Transaction = transaction.Id.ToString(),
                    Timestamp   = transaction.PerformedAt.Ticks,
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

                if (!order.CanRefund)
                {
                    throw new UnableCancelTransactionException();
                }
                else
                {
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
                CanceledAt  = transaction.CanceledAt.Ticks,
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
                CancelTime  = transaction.CanceledAt.Ticks,
                CreateTime  = transaction.CreatedAt.Ticks,
                PerformTime = transaction.PerformedAt.Ticks,
                Transaction = transaction.Id.ToString(),
                Reason      = transaction.Reason,
                State       = transaction.State
            };
        }

        public async Task<GetStatementResult> GetStatement(long from, long to)
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
                        Order = x.OrderId
                    },
                    CreateTime  = x.CreatedAt.Ticks,
                    PerformTime = x.PerformedAt.Ticks,
                    CancelTime  = x.CanceledAt.Ticks,
                    Transaction = x.Id.ToString(),
                    State       = x.State,
                    Reason      = x.Reason,
                    Receivers   = null
                }).ToList()
            };
        }
    }
}

