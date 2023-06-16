using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.Repository;
using SecondhandStore.Services;
using System.Web.Http.Cors;


var builder = WebApplication.CreateBuilder(args);

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

// Add services to the container.

var config = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SecondhandStoreContext>(options =>
    options.UseSqlServer(config["ConnectionStrings:SecondhandStoreDB"])
        .EnableSensitiveDataLogging());
//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RoleService>();

builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<PostRepository>();
builder.Services.AddScoped<PostService>();

builder.Services.AddScoped<TopUpRepository>();
builder.Services.AddScoped<TopUpService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
