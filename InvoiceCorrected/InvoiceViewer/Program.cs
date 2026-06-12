using BuggyApp.Controllers;
using BuggyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen();


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


if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice API v1");
        c.RoutePrefix = "swagger"; 
    });
}

app.UseDefaultFiles(); 
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthorization();


app.MapControllers(); 

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<InvoiceContext>();
    

    if (context.Database.EnsureCreated()) 
    {
        context.InvoiceItems.Add(new InvoiceController.Item { ItemID = 1, InvoiceID = 1, Name = "Widget A", Price = 19.99 });
        context.SaveChanges();
    }
}
app.Run();