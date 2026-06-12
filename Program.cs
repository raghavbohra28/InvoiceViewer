using BuggyApp.Controllers;
using BuggyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

builder.Services.AddOpenApiDocument(config => 
{
    config.PostProcess = document => document.Info.Title = "Invoice API v1";
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<InvoiceContext>(options =>
    options.UseSqlite("Data Source=invoice.db"));

var app = builder.Build();

// Enable Swagger UI across both Development and Production environments
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseOpenApi(); // Serves the raw spec document at /swagger/v1/swagger.json
    app.UseSwaggerUi(config => 
    {
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/v1/swagger.json";
    });
}

// Enable serving your frontend index.html, script.js, and styles.css
app.UseDefaultFiles(); 
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers(); 


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<InvoiceContext>();
        
        if (context.Database.EnsureCreated()) 
        {

            if (!context.InvoiceItems.Any())
            {
                context.InvoiceItems.Add(new InvoiceController.Item { ItemID = 1, InvoiceID = 1, Name = "Widget A", Price = 19.99 });
                context.SaveChanges();
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database seeding check skipped or halted: {ex.Message}");
    }
}

app.Run();