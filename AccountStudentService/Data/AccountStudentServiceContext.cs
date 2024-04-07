using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AccountStudentService.Models;

namespace AccountStudentService.Data
{
    public class AccountStudentServiceContext : DbContext
    {
        public AccountStudentServiceContext (DbContextOptions<AccountStudentServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AccountStudentService.Models.Student> Student { get; set; } = default!;
        public DbSet<AccountStudentService.Models.HistoryTransaction> HistoryTransactions { get; set; } = default!;
    }
}
