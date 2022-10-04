using DAL.Context;
using MeliTest.Middleware;
using MeliTest.Service.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.Contracts.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MeliTest API",
        Description = "ASP.NET Core Web API for a Meli Test.",
        Contact = new OpenApiContact
        {
            Name = "Gustavo Adolfo Arteaga Sanchez",
            Email = "arteaga89adv@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/gustavo-adolfo-arteaga-sanchez-148a5154/"),
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IDebtService, DebtService>();

builder.Services.AddSqlServer<LoanContext>(builder.Configuration.GetConnectionString("cnLoans"));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
