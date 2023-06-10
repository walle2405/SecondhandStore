using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecondhandStore.Infrastructure;
using SecondhandStore.Repository;
using SecondhandStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SecondhandStoreContext>(options =>
    options.EnableSensitiveDataLogging());

builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
