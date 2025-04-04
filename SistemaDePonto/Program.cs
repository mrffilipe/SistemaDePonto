using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SistemaDePonto.Application.Interfaces;
using SistemaDePonto.Infrastructure.Authentication;

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

// Add infrastructure to DI
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// Add application to DI

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
