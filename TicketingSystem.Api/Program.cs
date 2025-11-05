using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TicketingSystem.Application.Auth.Commands;

using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Application.Ticketing.Commands;
using TicketingSystem.Application.Ticketing.Dtos;
using TicketingSystem.Application.Ticketing.Queries;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Infrastructure.Auth;
using TicketingSystem.Infrastructure.Persistence;
using TicketingSystem.Shared.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// اضافه کردن سرویس‌ها به DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<RegisterCommandHandler>();
builder.Services.AddScoped<CreateTicketCommandHandler>();

builder.Services.AddScoped<ICommandBus, SimpleMediator>();

builder.Services.AddSingleton<SimpleMediator>();

builder.Services.AddScoped<ICommandHandler<LoginCommand, string>, LoginCommandHandler>();
builder.Services.AddScoped<ICommandHandler<RegisterCommand, Guid>, RegisterCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateTicketCommand, Guid>, CreateTicketCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateTicketCommand, bool>, UpdateTicketCommandHandler>();
builder.Services.AddScoped<ICommandHandler<GetAllTicketsQuery, List<TicketDto>>, GetAllTicketsQueryHandler>();
builder.Services.AddScoped<ICommandHandler<GetMyTicketsQuery, List<TicketDto>>, GetMyTicketsQueryHandler>();
builder.Services.AddScoped<ICommandHandler<GetTicketStatsQuery, Dictionary<TicketStatus,int>>, GetTicketStatsQueryHandler>();
builder.Services.AddHttpContextAccessor();

// Authentication & Authorization
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TicketingSystem API", Version = "v1" });

    // اضافه کردن تعریف امنیتی برای JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbInitializer.SeedAsync(context);
}


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TicketingSystem API v1");
    c.RoutePrefix = string.Empty; // Swagger UI در روت اصلی
});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();