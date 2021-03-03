using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace TestTransactionsTask.Models
{
    public interface ITestDBContext
    {
        DbSet<Transaction> Transactions { get; set; }

        Task<int> SaveChanges();
    }
}