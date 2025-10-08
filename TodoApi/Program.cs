using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApi.Data;
using TodoApi.Repositories;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

//CORS
const string DEV_CORS_POLICY = "AllowDevOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: DEV_CORS_POLICY,
      policy =>
      {
          // This allows your React app's origin
          policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
      });
});


// --- Register Services for Dependency Injection ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TodoDbContext>(opt => opt.UseSqlServer(connectionString));

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// --- Configure the HTTP request pipeline ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Apply the CORS policy ---
// This MUST go before UseAuthorization and MapControllers
app.UseCors(DEV_CORS_POLICY);

// app.UseAuthorization(); // We will add this later

app.MapControllers();

app.Run();
