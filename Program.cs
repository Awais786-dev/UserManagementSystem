// It sets up a configuration environment for your web application,
// creates an instance of the WebApplicationBuilder class.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//configuring services and preparing data for documentation
builder.Services.AddEndpointsApiExplorer();     // gather details about the API endpoints you’ve defined.   //useful for tools like Swagger that visualize your API
builder.Services.AddSwaggerGen();   //generating a JSON file that describes your API and providing a UI for it.

// creates an instance of the WebApplication class.
// It turns your configurations into a runnable application that is ready to handle requests. 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // In form Of JSON, sets up the middleware to generate the Swagger documentation for your API.
    app.UseSwaggerUI();  // creates a user-friendly interface.
}

app.UseAuthorization();

app.MapControllers();

app.Run();
