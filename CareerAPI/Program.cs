var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger service
builder.Services.AddEndpointsApiExplorer();  // Required for API Explorer to work
builder.Services.AddSwaggerGen();  // Required to generate Swagger docs

var app = builder.Build();

// Use Swagger and Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Generates Swagger Docs
    app.UseSwaggerUI();  // Displays Swagger UI
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
