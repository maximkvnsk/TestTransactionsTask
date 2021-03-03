
using System.ComponentModel.DataAnnotations.Schema;


namespace TestTransactionsTask.Models
{
    public partial class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int TransactionId { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string ClientName { get; set; }
        public string  Amount { get; set; }

    }
}
