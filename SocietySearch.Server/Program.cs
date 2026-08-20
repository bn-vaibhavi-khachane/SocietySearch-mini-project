using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NZWalks.API.Repositories;
using SocietySearch.Server.Data;
using SocietySearch.Server.Mappings;
using SocietySearch.Server.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        Description = "Enter a JWT Bearer token."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference(JwtBearerDefaults.AuthenticationScheme, document)] = []
    });
});

builder.Services.AddDbContext<SocietySearchDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocietySearchConnectionString")));

builder.Services.AddDbContext<SocietySearchAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SocietySearchAuthConnectionString")));

builder.Services.AddScoped<ISocietyRepository, SQLSocietyRepository>();
builder.Services.AddScoped<IAmenitiesRepository, SQLAmenitiesRepository>();
builder.Services.AddScoped<IUnitsRepository, SQLUnitsRepository>();
builder.Services.AddScoped<TokenRepository>();

//Identity
builder.Services.AddIdentity<Manager, IdentityRole>()
    .AddEntityFrameworkStores<SocietySearchAuthDbContext>()
    .AddDefaultTokenProviders();

//Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    });
builder.Services.AddAuthorization();



var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.MapSwagger();
    app.MapSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
