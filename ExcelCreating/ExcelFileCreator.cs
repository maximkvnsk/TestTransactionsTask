using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTransactionsTask.Models;

namespace TestTransactionsTask.ExcelCreating
{
    public class ExcelFileCreator
    {
        public static void CreateExcelFile(IEnumerable<Transaction> transactionList)
        {
            Workbook wb = new Workbook();
            Worksheet worksheet = wb.Worksheets[0];
            int counter = 2;
            worksheet.Cells["A1"].PutValue("ClientName");
            worksheet.Cells["B1"].PutValue("Type");
            worksheet.Cells["C1"].PutValue("Status");
            worksheet.Cells["D1"].PutValue("Amount");
            foreach (var t in transactionList)
            {
                worksheet.Cells["A" + counter].PutValue(t.ClientName);
                worksheet.Cells["B" + counter].PutValue(t.Type);
                worksheet.Cells["C" + counter].PutValue(t.Status);
                worksheet.Cells["D" + counter].PutValue(t.Amount);
                counter++;
            }
            wb.Save("wwwroot\\Excel.xlsx", SaveFormat.Xlsx);
        }
    }
}
