using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestTransactionsTask.Models;

namespace TestTransactionsTask.Features.TransactionFeatures.Commands
{
    public class UpdateTransactionStatusByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Status { get; set; }
 
        public class UpdateTransactionStatusByIdCommandHandler : IRequestHandler<UpdateTransactionStatusByIdCommand, int>
        {
            private readonly ITestDBContext _context;
            public UpdateTransactionStatusByIdCommandHandler(ITestDBContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateTransactionStatusByIdCommand command, CancellationToken cancellationToken)
            {
                var transaction = _context.Transactions.Where(a => a.TransactionId == command.Id).FirstOrDefault();
                if (transaction == null)
                {
                    return default;
                }
                else
                {
                    transaction.Status = command.Status;
                    await _context.SaveChanges();
                    return transaction.TransactionId;
                }
            }
        }
    }
}
