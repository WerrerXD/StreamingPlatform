
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Handlers;
using Stream.Service.BusinessLogic.Handlers.StreamHandlers;
using Stream.Service.BusinessLogic.Mappings;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.BusinessLogic.Services;
using Stream.Service.BusinessLogic.Validators;
using Stream.Service.DataAccess;
using Stream.Service.DataAccess.Repositories;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Settings;
using Stream.Service.Presentation.Hubs;
using Stream.Service.Presentation.Middleware;
using Stream.Service.Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(CreateStreamCommandValidator).Assembly));

builder.Services.AddSingleton<IElasticsearchService, ElasticsearchService>(sp =>
    new ElasticsearchService("http://elasticsearch:9200"));

builder.Services.AddScoped<ILoggingService, LoggingService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<IOptions<DatabaseSettings>>().Value.ConnectionString));

builder.Services.AddScoped<IChatNotificationService, ChatNotificationService>();
builder.Services.AddScoped<IPaymentService, FakePaymentService>();

builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<IStreamCategoryRepository, StreamCategoryRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();
builder.Services.AddScoped<IDonationGoalRepository, DonationGoalRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateStreamHandler).Assembly));


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
