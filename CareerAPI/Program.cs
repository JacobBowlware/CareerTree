using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CareerAPI.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Connect to SQL Server database & configure services
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Password policy
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

builder.Services.Configure<IdentityOptions>(options =>
{
    // Allow letters, digits, and common email characters (even if you’re not using email as username)
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();  // Required for API Explorer to work
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
