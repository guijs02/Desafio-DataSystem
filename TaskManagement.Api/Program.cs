using TaskManagement.Api;
using TaskManagement.Api.Common;
using TaskManagement.Api.Middlewares;
using TaskManagement.Application.DIP;
using TaskManagement.Infraestructure.Build;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddContext(builder.Configuration);
builder.Services.AddInfraDependencies();
builder.Services.AddApplicationDependencies();

// Register controllers and enable FluentValidation auto-validation
builder.Services.AddControllers();

// Register validators from application assembly

//add swagger documentation
builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// use global exception handler as early as possible
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapControllers();

app.Run();
