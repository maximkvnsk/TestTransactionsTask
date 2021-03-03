using Aspose.Cells;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestTransactionsTask.ExcelCreating;
using TestTransactionsTask.Models;
namespace TestTransactionsTask.Features.TransactionFeatures.Queries
{
    public class GetFilteredTransactionsQuery : IRequest<FileResult>
    {
        public string ClientName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public class GetFilteredTransactionsQueryHandler : IRequestHandler<GetFilteredTransactionsQuery, FileResult>
        {
            private readonly ITestDBContext _context;
            public GetFilteredTransactionsQueryHandler(ITestDBContext context)
            {
                _context = context;
            }
            public async Task<FileResult> Handle(GetFilteredTransactionsQuery query, CancellationToken cancellationToken)
            {
                var transactionList = await _context.Transactions.ToListAsync();
                if (!String.IsNullOrEmpty(query.ClientName))
                {
                     transactionList = transactionList.Where(a => a.ClientName == query.ClientName).ToList();
                }
                if (!String.IsNullOrEmpty(query.Type))
                {
                    transactionList = transactionList.Where(a => a.Type == query.Type).ToList();
                }
                if (!String.IsNullOrEmpty(query.Status))
                {
                    transactionList = transactionList.Where(a => a.Status == query.Status).ToList();
                }

                if (transactionList == null)
                {
                    return null;
                }
                ExcelFileCreator.CreateExcelFile(transactionList);
                return new PhysicalFileResult("Excel.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") { FileDownloadName = "export.xlsx" };
            }
        }
    }
}
