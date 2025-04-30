using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using User.Service.Application.Abstractions;
using User.Service.Application.Mappings;
using User.Service.Application.Services;
using User.Service.Application.Validators;
using User.Service.Domain.Interfaces;
using User.Service.Infrastructure;
using User.Service.Infrastructure.Messaging;
using User.Service.Infrastructure.Repositories;
using User.Service.Presentation.Extensions;
using User.Service.Presentation.Middleware;
using User.Service.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

var redis = ConnectionMultiplexer.Connect("redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddApiAuthentication(builder.Configuration);

builder.Services.AddHangfire(config =>
{
    config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(RegisterUserRequestValidator).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddUseCases();

builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>(sp =>
    new ElasticsearchService("http://elasticsearch:9200"));

builder.Services.AddScoped<ILoggingService, LoggingService>();

builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();

builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IFileStorageService, FileStorageService>();
builder.Services.AddScoped<IDeleteExpiredTokensJob, DeleteExpiredTokensJob>();
builder.Services.AddScoped<IUnblockExpiredUsersJob, UnblockExpiredUsersJob>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddSingleton<IBlacklistedTokenRepository, BlacklistedTokenRepository>();

builder.Services.AddHangfireServer();    

builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (dbContext.Database.IsNpgsql())
    {
        dbContext.Database.Migrate();
    }

}

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<IDeleteExpiredTokensJob>(
    "CleanupExpiredTokens",
    service => service.DeleteExpiredTokensAsync(default),
    Cron.Daily);

RecurringJob.AddOrUpdate<IUnblockExpiredUsersJob>(
    "UnblockExpiredUsers",
    service => service.UnblockExpiredUsersAsync(default),
    Cron.Hourly);

app.UseMiddleware<TokenValidationMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();