using Microsoft.EntityFrameworkCore;
using TestingEFCoreApp.Data;
using TestingEFCoreApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EFCoreAppContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("EFCoreAppConnection")));

builder.Services.AddScoped<IMateriaRepository, MateriaRepository>();
builder.Services.AddScoped<ICarreraRepository, CarreraRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
