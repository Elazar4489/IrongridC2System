using ConsumerIrongridC2System.Data;
using IrongridC2SystemAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString =  builder.Configuration.GetConnectionString("IrongridC2ConnectionString");
builder.Services.AddDbContext<ApiDbContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<AssetsRepository>();
builder.Services.AddScoped<AssetsStatusRepository>();
builder.Services.AddScoped<OperationReportsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
