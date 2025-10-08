using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

//connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//db context injection stuff
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlServer(connectionString));

//new
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();

//AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Swagger stuff
builder.Services.AddSwaggerGen( options =>
{
    //reading xml from here(?
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
