using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;
using Tabtaba.Domain.Contracts;
using Tabtaba.Domain.Entities;
using Tabtaba.Domain.Entities.Settings;
using Tabtaba.Domain.Entities.UserEntity;
using Tabtaba.Persistence.Repositories;
using Tabtaba.Presentation.Controllers;
using Tabtaba.Presentation.Middlewares;
using Tabtaba.Services.Features.EducationServices;
using Tabtaba.Services.Services;
using Tabtaba.ServicesAbstraction;
using Tabtaba.ServicesAbstraction.Interfaces;
using Tabtaba.ServicesAbstraction.Validators;
using Tabtba.Persistence.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Serilog
builder.Host.UseSerilog((ctx, config) =>
{
    config
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File("Logs/tabtaba-.log",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30);
});

// DeepSeek Ai Model Chatbot
builder.Services.Configure<DeepSeekSettings>(builder.Configuration.GetSection("DeepSeekSettings"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

// PostgreSQL (Npgsql)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHttpClient<IChatbotService, ChatbotService>();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("LoginPolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromMinutes(1);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 0;
    });
    options.RejectionStatusCode = 429;
});

// JWT + Google
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
    };
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
});

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(SaveEducationHandler).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(
    typeof(EducationValidator).Assembly);

// 🔥 تعديل الـ CORS: سمحنا لأي موقع خارجي (بما فيهم Vercel) يكلم الـ API عشان نخلص من خنقة الدومينات الخارحية
builder.Services.AddCors(options =>
{
    options.AddPolicy("TabtabaPolicy", policy =>
    {
        policy.SetIsOriginAllowed(origin => true) // دي بتخلي أي فرونت إند (Vercel أو غيره) يعدي حلاوة
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSingleton<EncryptionService>();

var app = builder.Build();

// 1. الـ Global Exception أول حاجة في البايبلاين
app.UseMiddleware<GlobalExceptionMiddleware>();

// 🔥 2. الـ CORS لازم يكون هنا فوق قبل أي حاجة عشان يوافق على الـ OPTIONS preflight فوراً
app.UseCors("TabtabaPolicy");

// تم إزالة app.UseAntiforgery() الملعون اللي كان بيخرب الـ Requests

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Security Headers متظبطة ومفتوحة للـ CORS المريح
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY");
    context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Append("Referrer-Policy", "no-referrer");
    context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRateLimiter();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<SanitizationMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();