using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AccountStudentService.Models
{
    public class HistoryTransaction
    {
        [Key]
        public int TransactionId { get; set; }
        
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [JsonIgnore]
        public Student Student { get; set; }
    }
}
