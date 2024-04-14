using System.ComponentModel.DataAnnotations;
namespace TransactionService.Models
{
    public class PaymentDetail
    {
        [Key]
        public int paymentId { get; set; }
        public string username { get; set; }
        public long amount { get; set; }
   
        public string OTP { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
