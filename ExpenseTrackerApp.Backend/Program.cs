using ExpenseTrackerApp.Backend.Expense.Contracts.Transactions;
using ExpenseTrackerApp.Backend.Expense.Domain.User;
using ExpenseTrackerApp.Backend.Expense.EFCore;
using ExpenseTrackerApp.Backend.Expense.Services;
using ExpenseTrackerApp.Backend.Expense.Services.Transaction;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ------------------------
// Configure Database
// ------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
// register TransactionAppService and AutoMapper
builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


// ------------------------
// Configure Identity
// ------------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    // Password settings (example)
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddApiEndpoints(); // ? Built-in endpoints added here


// ------------------------
// Configure JWT Authentication
// ------------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});


// ------------------------
// Add controllers and Swagger
// ------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable CORS for Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // your Angular app URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();

// ------------------------
// Configure Middleware
// ------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AngularCors");

app.UseAuthentication(); // <-- Added

app.UseAuthorization();

app.MapControllers();
// ------------------------
// ? Built-in Identity Endpoints
// ------------------------
app.MapIdentityApi<ApplicationUser>(); // ?? exposes /register, /login, /logout, etc.

app.Run();
