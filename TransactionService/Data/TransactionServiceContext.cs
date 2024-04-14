using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionService.Models;

namespace TransactionService.Data
{
    public class TransactionServiceContext : DbContext
    {
        public TransactionServiceContext (DbContextOptions<TransactionServiceContext> options)
            : base(options)
        {
        }

        public DbSet<TransactionService.Models.PaymentDetail> PaymentDetail { get; set; } = default!;

       
    }
}
