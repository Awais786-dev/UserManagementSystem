using Microsoft.EntityFrameworkCore;
using User_Management_System.Entities;
using User_Management_System.MappingProfile;
using User_Management_System.Repositories;
using User_Management_System.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Register your services here
builder.Services.AddScoped<UserRepository>(); // Shared instance for user data
builder.Services.AddScoped<IUserService, UserService>(); // Register the service // New instance for each request

builder.Services.AddAutoMapper(typeof(UserProfile)); // Registers your profile

builder.Services.AddDbContext<EFCoreDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
