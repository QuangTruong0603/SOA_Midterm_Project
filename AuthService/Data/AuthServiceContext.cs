using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AuthService.Models;

namespace AuthService.Data
{
    public class AuthServiceContext : DbContext
    {
        public AuthServiceContext (DbContextOptions<AuthServiceContext> options)
            : base(options)
        {
        }

        public DbSet<AuthService.Models.Student> Student { get; set; } = default!;
        
    }
}
