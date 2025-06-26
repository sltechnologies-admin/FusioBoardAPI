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
builder.Services.AddCors(options =>
{
    //options.AddPolicy("AllowFrontend",
    //policy =>
    //{
    //    policy.WithOrigins("*")//            "https://http://localhost:3001.com") // or "*"
    //          .AllowAnyHeader()
    //          .AllowAnyMethod();
    //});
    //Replace .AllowAnyOrigin() with .WithOrigins("http://localhost:4200") or your actual frontend domain.
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();


// Register here
builder.Services.AddScoped<IDatabaseService, SqlDatabaseService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
builder.Services.AddScoped<ISqlLogger, SqlLogger>();
builder.Services.AddScoped<ISprintService, SprintService>();
builder.Services.AddScoped<ISprintRepository, SprintRepository>();
builder.Services.AddScoped< ILogRepository,LogRepository > ();
builder.Services.AddScoped<ILogService, LogService>();




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//for DAL 
builder.Services.AddSingleton<SqlDatabaseService>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});




var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseCors("AllowAll"); // Must come before UseAuthorization()

app.UseAuthorization();

app.MapControllers();

//Place it early in the middleware pipeline (right after exception handling).
app.UseMiddleware<CorrelationIdMiddleware>();

app.Run();
