using Microsoft.EntityFrameworkCore;
using NZWalks.API.Repositories;
using SocietySearch.Server.Data;
using SocietySearch.Server.Mappings;
using SocietySearch.Server.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperProfiles).Assembly);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SocietySearchDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocietySearchConnectionString")));
builder.Services.AddScoped<ISocietyRepository, SQLSocietyRepository>();
builder.Services.AddScoped<IAmenitiesRepository, SQLAmenitiesRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
