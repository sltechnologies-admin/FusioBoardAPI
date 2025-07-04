using API.Common.Logging;
using API.Data;
using API.Data.Interfaces;
using API.Middleware;
using API.Repositories;
using API.Repositories.Interfaces;
using API.Services;
using API.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ------------------ CORS ------------------
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFrontend", policy =>
//    {
//        policy.WithOrigins(
//            "http://localhost:3000",           // Local React dev (Change if using different port)
//            "https://fusioboard-ui.azurewebsites.net" // Deployed frontend
//        )
//        .AllowAnyHeader()
//        .AllowAnyMethod();
//        // .AllowCredentials(); // Uncomment if using cookies/auth headers
//    });
//});

// Register CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            // Local dev: allow everything
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            // Production: restrict to specific frontend
            policy.WithOrigins("https://fusioboard-ui.azurewebsites.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            // .AllowCredentials(); // Only if you're using cookies/auth headers
        }
    });
});


// ------------------ Services :Add services to the container.------------------
builder.Services.AddControllers();

// Dependency Injection - Services and Repositories
builder.Services.AddScoped<IDatabaseService, SqlDatabaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ISprintService, SprintService>();
builder.Services.AddScoped<ISprintRepository, SprintRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
builder.Services.AddScoped<ISqlLogger, SqlLogger>();

// Singleton for shared base service
//builder.Services.AddSingleton<SqlDatabaseService>();

// ------------------ Swagger ------------------
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// ------------------ App Build ------------------
var app = builder.Build();

// ------------------ Middleware Pipeline ------------------

// Swagger (enabled always, remove for production if needed)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// CORS must come before authentication/authorization
app.UseCors("AllowFrontend");

app.UseAuthorization();

// Add correlation ID middleware (for structured logging)
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapControllers();

//Logger: To confirm the current environment at runtime
app.Logger.LogInformation("Running in {env} environment", builder.Environment.EnvironmentName);

app.Run();
