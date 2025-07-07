using Bootcamp.Data;
using Bootcamp.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorWasmPolicy", policy =>
    {
        policy
            .WithOrigins("https://localhost:7079") // Change to your Blazor WASM origin
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//string dbConnectionStringName = "Bootcamp1Database";
string dbConnectionStringName = "BootcampDatabase";
var connectionString = builder.Configuration.GetConnectionString(dbConnectionStringName) ?? throw new InvalidOperationException($"Connection string '{dbConnectionStringName}' not found.");

builder.Services.AddDbContext<BootcampContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlazorWasmPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
