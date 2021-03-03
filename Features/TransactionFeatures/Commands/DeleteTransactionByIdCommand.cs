using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestTransactionsTask.Models;

namespace TestTransactionsTask.Features.TransactionFeatures.Commands
{
    public class DeleteTransactionByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteTransactionByIdCommandHandler : IRequestHandler<DeleteTransactionByIdCommand, int>
        {
            private readonly ITestDBContext _context;
            public DeleteTransactionByIdCommandHandler(ITestDBContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteTransactionByIdCommand command, CancellationToken cancellationToken)
            {
                var transaction = await _context.Transactions.Where(a => a.TransactionId == command.Id).FirstOrDefaultAsync();
                if (transaction == null) return default;
                _context.Transactions.Remove(transaction);
                await _context.SaveChanges();
                return transaction.TransactionId;
            }
        }
    }
}
