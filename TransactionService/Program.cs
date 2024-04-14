using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Data;
namespace TransactionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TransactionServiceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TransactionServiceContext") ?? throw new InvalidOperationException("Connection string 'TransactionServiceContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}