using MediatR;
using System;
using CsvHelper;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestTransactionsTask.Models;
using Microsoft.AspNetCore.Http;

namespace TestTransactionsTask.Features.TransactionFeatures.Commands
{
    public class ImportTransactionsFromCsvCommand : IRequest<int>
    {
        public IFormFile uploadedFile { get; set; }

        public class ImportTransactionsFromCsvCommandHandler : IRequestHandler<ImportTransactionsFromCsvCommand, int>
        {
            private readonly ITestDBContext _context;
            public ImportTransactionsFromCsvCommandHandler(ITestDBContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(ImportTransactionsFromCsvCommand command, CancellationToken cancellationToken)
            {
                //   string pathCsvFile = "C:\\Users\\maxim\\Desktop\\data.csv";
                using (StreamReader streamReader = new StreamReader(command.uploadedFile.OpenReadStream()))
                {
                    //  using (CsvReader csvReader = new CsvReader(streamReader, new System.Globalization.CultureInfo("es-ES", false)))
                    using (CsvReader csvReader = new CsvReader(streamReader, new CsvHelper.Configuration.CsvConfiguration(new System.Globalization.CultureInfo("es-ES", false)) {Delimiter = "," }))
                    {                     
                        IEnumerable csvTransactions =
                            csvReader.GetRecords<Transaction>();

                        foreach (Transaction t in csvTransactions)
                        {
                            var transactionToUpdate = _context.Transactions.Where(a => a.TransactionId == t.TransactionId).FirstOrDefault();
                            if (transactionToUpdate !=null && transactionToUpdate !=t){
                                transactionToUpdate.Status = t.Status;
                                transactionToUpdate.Type = t.Type;
                                transactionToUpdate.ClientName = t.ClientName;
                                transactionToUpdate.Amount = t.Amount;
                            }
                            else _context.Transactions.Add(t);
                        }
                        await _context.SaveChanges();
                        return 1;
                    }
                }

                 
            }
        }
    }
}
