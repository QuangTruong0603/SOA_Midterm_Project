using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", corsPolicyBuilder => corsPolicyBuilder
        .WithOrigins("http://127.0.0.1:5500") 
        .AllowAnyMethod() 
        .AllowAnyHeader() 
        .AllowCredentials()); 
});

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();
app.UseCors("MyCorsPolicy");

app.MapGet("/", () => "Hello World!");
app.MapControllers();
await app.UseOcelot();

app.Run();
