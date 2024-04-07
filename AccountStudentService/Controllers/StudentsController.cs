using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AccountStudentService.Data;
using AccountStudentService.Models;

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

     

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
