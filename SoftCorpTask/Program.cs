using System.Security.Claims;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftCorpTask.Contexts;
using SoftCorpTask.Enums;
using SoftCorpTask.ExceptionHandlers;
using SoftCorpTask.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Default connection string is not supplied.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(defaultConnectionString));

builder.Services.AddScoped<IPasswordService, SimplePasswordService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IWorkGroupService, WorkGroupService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JsonWebTokenStuff:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JsonWebTokenStuff:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenStuff:Secret"]!)),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Administrator", policy => policy.RequireClaim(ClaimTypes.Role, UserRole.Administrator.ToString()));

builder.Services.AddExceptionHandler<UserNotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<InvalidPasswordExceptionHandler>();
builder.Services.AddExceptionHandler<InvalidRefreshTokenExceptionHandler>();
builder.Services.AddExceptionHandler<CandidateNotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHealthChecks()
    .AddNpgSql(defaultConnectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse});

app.Run();