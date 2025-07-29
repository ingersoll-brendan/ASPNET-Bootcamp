using Bootcamp.Data;
using Bootcamp.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



#region User Secrets (Debug Only)

#if DEBUG

// Load user secrets from developer machine. Each launchSetting Profile has own secrets id. Make sure your local machine has the same folder/secrets.json
// for each profile and the folder names match the GUIDs defined in the launchSettings.json -> environmentVariables section.
// Create here file here: %APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json
string? userSecretsId = Environment.GetEnvironmentVariable("UserSecretsId");

if (!string.IsNullOrEmpty(userSecretsId))
{
	builder.Configuration.AddUserSecrets(userSecretsId);
}

#endif

#endregion

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

string dbConnectionStringName = "BootcampDatabase";
var connectionString = builder.Configuration.GetConnectionString(dbConnectionStringName) ?? throw new InvalidOperationException($"Connection string '{dbConnectionStringName}' not found.");

builder.Services.AddDbContext<BootcampContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
/* Enable for all environments - In order to see */
//if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Integration"))
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("BlazorWasmPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
