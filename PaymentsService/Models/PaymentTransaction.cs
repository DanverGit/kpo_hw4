namespace PaymentsService.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } // "Pending", "Completed", "Failed"
        public string TransactionId { get; set; } // Уникальный идентификатор транзакции
    }
}
