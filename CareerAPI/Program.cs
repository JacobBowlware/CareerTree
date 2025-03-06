using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CareerAPI.Models;
using AutoMapper;
using CareerAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Configure the database context (using SQL Server here as an example)
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register AutoMapper and the UserProfile - for mapping User to UserDTO and vice versa for SQL operations
builder.Services.AddAutoMapper(typeof(UserProfile));

// Add Identity services and configure options
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Configure password requirements, lockout, etc.
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<UserDbContext>()
.AddDefaultTokenProviders();

// Add Swagger service
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.EnableAnnotations();
        c.SwaggerDoc("v1", new() { Title = "CareerAPI", Version = "v1" });
    });
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // Required for API Explorer to work

// Add CORS policy to allow requests from Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular default port
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAngular");

// Use Swagger and Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CareerAPI V1");
        c.RoutePrefix = string.Empty; // Sets Swagger UI at the root
    });
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Add Authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();
