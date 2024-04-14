using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccountStudentService.Data;
using AccountStudentService.Models;
using AccountStudentService.Helper;
using iTextSharp.text.pdf;
using iTextSharp.text;


namespace AccountStudentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AccountStudentServiceContext _context;

        public StudentsController(AccountStudentServiceContext context)
        {
            _context = context;
        }

       

        // GET: api/Students/5
        [HttpGet("{username}")]
        public async Task<ActionResult<Student>> GetStudent(string username)
        {
          if (_context.Student == null)
          {
              return NotFound();
          }
            var student = await _context.Student.Include(u => u.HistoryTransactions).FirstOrDefaultAsync(i => i.Username == username);

            if (student == null)
            {
                return NotFound();
            }

            
            return student;
        }

        private void CreateInvoice(string filePath, string userID, string name, long amount, string beniID, string beniName)
        {
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

          

            // Fonts
            Font titleFont = FontFactory.GetFont("Arial Unicode MS", 20, Font.BOLD, BaseColor.RED);
            Font headingFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);
            Font bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLUE);

          

            // Title
            Paragraph title = new Paragraph("Bill of sale", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);

            // Add a line separator
            //LineSeparator lineSeparator = new LineSeparator();
            //lineSeparator.LineColor = new BaseColor(0, 0, 0, 68);
            //document.Add(new Chunk(lineSeparator));
            document.Add(new Paragraph(" ")); // Adding a blank line

            // Invoice Info
            document.Add(new Paragraph($"Release date: {DateTime.Now.ToString("dd/MM/yyyy")}", bodyFont));
            document.Add(new Paragraph(" ")); // Blank line

            // Billing Info
            document.Add(new Paragraph("Billing Information", headingFont));
            document.Add(new Paragraph("Student ID: " + userID, bodyFont));
            document.Add(new Paragraph("Name: " + name, bodyFont));
            document.Add(new Paragraph(" "));

            // Add another line separator
            //document.Add(new Chunk(lineSeparator));
            document.Add(new Paragraph(" "));

            // Adding a table
            PdfPTable table = new PdfPTable(3); // 3 columns.
            table.WidthPercentage = 100;
            PdfPCell cell1 = new PdfPCell(new Phrase("Beneficiary ID", headingFont));
            PdfPCell cell2 = new PdfPCell(new Phrase("Name", headingFont));
            PdfPCell cell3 = new PdfPCell(new Phrase("Amount", headingFont));

            cell1.Border = PdfPCell.ALIGN_BASELINE;
            cell2.Border = PdfPCell.ALIGN_BASELINE;
            cell3.Border = PdfPCell.ALIGN_BASELINE;

            table.AddCell(cell1);
            table.AddCell(cell2);
            table.AddCell(cell3);

            // Add product rows here
            // Example:
            table.AddCell(new PdfPCell(new Phrase(beniID, bodyFont)));
            table.AddCell(new PdfPCell(new Phrase(beniName, bodyFont)));
            table.AddCell(new PdfPCell(new Phrase(amount +" VND", bodyFont)));

            document.Add(table);

            // Closing the document
            document.Close();
            writer.Close();

            Console.WriteLine($"Invoice created at: {filePath}");
        }


        [HttpPost("Debt")]
        public IActionResult update([FromBody] DebtModel debt)
        {
            var payer = _context.Student.FirstOrDefault(o => o.Username.Equals(debt.payerid));
            var benefi = _context.Student.FirstOrDefault(i => i.Username.Equals(debt.benefiid));
            if (payer != null && benefi != null)
            {
                payer.AvailableBalance = payer.AvailableBalance - debt.amount;
                var history = new HistoryTransaction()
                {
                    Amount = debt.amount,
                    Date = DateTime.UtcNow,
                    Description = "",
                    StudentId = payer.StudentId,
                    beneId = debt.benefiid,
                    beneName = benefi.FullName

                };
                _context.HistoryTransactions.Add(history);
                _context.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            if (benefi != null)
            {
                benefi.TuitionFee = benefi.TuitionFee - debt.amount;
                _context.SaveChanges();
            }


            string invoicePath = "Public/" + DateTime.Now.Ticks + ".pdf";
            CreateInvoice(invoicePath, debt.payerid, payer.FullName, debt.amount, debt.benefiid, benefi.FullName);

            string confirmationCode = "Hóa đơn điện tử";

            string emailBody = "Xác nhận giao dịch thành công";

            bool sending = SendMail.SendEMail(payer.Email, emailBody, confirmationCode, invoicePath);
            if(sending)
            {
                return Ok("Success");
            }
            else
            {
                return BadRequest("Fail");
            }
        }






        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
