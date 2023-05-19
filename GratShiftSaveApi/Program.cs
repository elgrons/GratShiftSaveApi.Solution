using GratShiftSaveApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Google.Cloud.Firestore;
using System;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<GratShiftSaveApiContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(
                      builder.Configuration["ConnectionStrings:AZURE_MYSQL_CONNECTIONSTRING"],
                            ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:AZURE_MYSQL_CONNECTIONSTRING"]
                    )
                  )
                );
// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<GratShiftSaveApiContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer to Identity
.AddJwtBearer(options =>
{
  options.SaveToken = true;
  options.RequireHttpsMetadata = false;
  options.TokenValidationParameters = new TokenValidationParameters()
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidAudience = configuration["JWT:ValidAudience"],
    ValidIssuer = configuration["JWT:ValidIssuer"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
  };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("lowRisk", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(claim => 
                claim.Type == "risk" && Int32.Parse(claim.Value) < 50
            )
        )
    );
    options.AddPolicy("IsAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("IsUser", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
  build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Path to the service account key JSON file
string path = "/path/to/your/serviceAccountKey.json";

// Set the GOOGLE_APPLICATION_CREDENTIALS environment variable
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);

// Initialize Firestore
FirestoreDb db = FirestoreDb.Create(builder.Configuration["Firestore:ProjectId"]);

// Add Firestore to the services collection
builder.Services.AddSingleton(db);

// var devCorsPolicy = "devCorsPolicy";
// builder.Services.AddCors(options => options.AddPolicy(devCorsPolicy, builder =>
//   {
//     builder
//       .AllowAnyOrigin()
//       .AllowAnyMethod()
//       .AllowAnyHeader();
//   }));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseHttpsRedirection();
}

app.UseCors("corspolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllers()
    .RequireCors("corspolicy");

// app.UseCors(devCorsPolicy);

app.Run();
