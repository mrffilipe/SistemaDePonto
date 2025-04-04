using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Application.Services;
using SistemaDePonto.Application.UseCases;
using SistemaDePonto.Domain.Interfaces;
using SistemaDePonto.Infrastructure.Authentication;
using SistemaDePonto.Infrastructure.Persistence;
using SistemaDePonto.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        string projectId = Environment.GetEnvironmentVariable("SistemaDePonto__FirebaseProjectId")
        ?? throw new ArgumentNullException("Variável de ambiente 'SistemaDePonto__FirebaseProjectId' não encontrada.");

        options.Authority = $"https://securetoken.google.com/{projectId}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"https://securetoken.google.com/{projectId}",

            ValidateAudience = true,
            ValidAudience = projectId,

            ValidateLifetime = true,
        };
    });
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add infrastructure to DI
builder.Services.AddScoped<ICurrentUser, CurrentUser>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();

// Add application to DI
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IListTimeEntriesByDateUseCase, ListTimeEntriesByDateUseCase>();
builder.Services.AddScoped<IRegisterTimeEntryUseCase, RegisterTimeEntryUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
