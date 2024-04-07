using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AccountStudentService.Data;
namespace AccountStudentService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AccountStudentServiceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AccountStudentServiceContext") ?? throw new InvalidOperationException("Connection string 'AccountStudentServiceContext' not found.")));

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", corsPolicyBuilder =>
                    corsPolicyBuilder
                        .WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

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