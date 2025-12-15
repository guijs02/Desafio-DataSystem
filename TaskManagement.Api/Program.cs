using TaskManagement.Api;
using TaskManagement.Api.Common;
using TaskManagement.Api.Middlewares;
using TaskManagement.Application.DIP;
using TaskManagement.Infraestructure.Build;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContext(builder.Configuration);
builder.Services.AddInfraDependencies();
builder.Services.AddApplicationDependencies();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();
builder.Services.AddCorsConfiguration(builder.Configuration);

var app = builder.Build();

await app.Services.EnsureDatabaseCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapControllers();

app.Run();
