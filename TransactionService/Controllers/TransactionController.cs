
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionService.Data;
using TransactionService.Models;
using TransactionService.TranModel;
using TransactionService.Helper;


namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionServiceContext _context;

        public TransactionController(TransactionServiceContext context)
        {
            _context = context;
        }
        private static string GenerateRandomCode()
        {
            // Tạo mã xác nhận ngẫu nhiên gồm 6 chữ số
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }
        private bool IsSpecialCharacter(char c)
        {
            // Define your set of special characters
            string specialCharacters = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";

            return specialCharacters.Contains(c);
        }
        [HttpPost("GenerateOTP")]
        public IActionResult GenerateOTP([FromBody] TranModel.TranModel tran)
        {
            string confirmationCode = GenerateRandomCode();

            string emailBody = "Mã xác nhận";

            bool sending = SendMail.SendEMail(tran.email, emailBody, confirmationCode, "");
           

            if(sending)
            {
                var expiredOTPs = _context.PaymentDetail
                .Where(od => od.username == tran.username)
                .ToList();

                if (expiredOTPs.Count > 0)
                {
                    _context.RemoveRange(expiredOTPs);
                    _context.SaveChanges();
                }


                PaymentDetail paymentDetail = new PaymentDetail()
                {
                    username = tran.username,
                    amount = tran.amount,

                    OTP = confirmationCode,
                    Email = tran.email,
                    CreatedAt = DateTime.Now


                };
                _context.PaymentDetail.Add(paymentDetail);
                _context.SaveChanges();

                return Ok("Success");


            }
            else
            {
                return BadRequest("Failed to send email");
            }



           
        }
        [HttpPost("CheckOTP")]
        public IActionResult checkOTP([FromBody] TranModel.OTP oTP)
        {
            var transaction = _context.PaymentDetail.FirstOrDefault(o => o.OTP.Equals(oTP.OTPcode) && o.username.Equals(oTP.username));
            if (transaction == null || (DateTimeOffset.UtcNow - transaction.CreatedAt).TotalMinutes > 2)
            {
                return BadRequest("Fail");
            }
            else
            {
                return Ok("Success");
            }
        }

    }
}
