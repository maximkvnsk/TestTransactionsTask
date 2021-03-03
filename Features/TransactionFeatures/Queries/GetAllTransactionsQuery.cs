using Aspose.Cells;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestTransactionsTask.ExcelCreating;
using TestTransactionsTask.Models;

namespace TestTransactionsTask.Features.TransactionFeatures.Queries
{
    public class GetAllTransactionsQuery : IRequest<FileResult>
    {
        public class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, FileResult>
        {
            private readonly ITestDBContext _context;
            public GetAllTransactionsQueryHandler(ITestDBContext context)
            {
                _context = context;
            }
            public async Task<FileResult> Handle(GetAllTransactionsQuery query, CancellationToken cancellationToken)
            {
                var transactionList = await _context.Transactions.ToListAsync();
                if (transactionList == null)
                {
                    return null;
                }
                ExcelFileCreator.CreateExcelFile(transactionList);
                return new VirtualFileResult("Excel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "export.xlsx" };
            }
        }
    }
}
