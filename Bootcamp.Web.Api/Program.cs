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
            .WithOrigins("https://localhost:7079", 
                "https://symmsoft-bootcamp-client-int.azurewebsites.net", 
                "https://symmsoft-bootcamp-client-prod.azurewebsites.net/") // Add other deployed front-end url domains.
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

// Apply our DB Migrations (i.e. create DB if not exist, create Tables if not exist, apply changes if exist, seed any required data)
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<BootcampContext>();
	db.Database.Migrate(); // This applies any pending migrations
}

// Configure the HTTP request pipeline.
/* Enable for all environments for testing purposes */
//if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Integration"))
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("BlazorWasmPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
