
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Stream.Service.BusinessLogic.Handlers;
using Stream.Service.BusinessLogic.Mappings;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.DataAccess.Repositories;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(TestProfile));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(sp.GetRequiredService<IOptions<DatabaseSettings>>().Value.ConnectionString));

builder.Services.AddScoped<IStreamRepository, StreamRepository>();
builder.Services.AddScoped<IStreamCategoryRepository, StreamCategoryRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateStreamHandler).Assembly));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

app.Run();
