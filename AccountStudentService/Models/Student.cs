

namespace AccountStudentService.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public long AvailableBalance { get; set; }
        public long TuitionFee { get; set; }
   
        public ICollection<HistoryTransaction> HistoryTransactions { get; set; }
        
    }
}
