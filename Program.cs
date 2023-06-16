using Microsoft.EntityFrameworkCore;
using SecondhandStore.Infrastructure;
using SecondhandStore.ServiceExtension;
using SecondhandStore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services to the container.
builder.Services.AddSwaggerService();

// Add services to the container.
builder.Services.AddScopedService();

// Add services to the container.
builder.Services.AddJwtAuthenticationService(config);

// Add services to the container.
builder.Services.AddApplicationService(config);

// Add services to the container.
builder.Services.AddAuthorizationService();

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 2048;
    options.UseCaseSensitivePaths = true;
});

builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAnyOrigin", corsPolicyBuilder =>
    {
        corsPolicyBuilder
            .SetIsOriginAllowed(x => _ = true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

// auto migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<SecondhandStoreContext>();

    // Here is the migration executed
    dbContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();