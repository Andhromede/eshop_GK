using Microsoft.EntityFrameworkCore;
using eshop.Models;
/*using System.Text.Json.Serialization;*/
using Microsoft.Extensions.FileProviders;
/*using System.IO;*/


var policyName = "AllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName, builder =>
    {
        builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<EshopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.CustomSchemaIds(s => s.FullName.Replace("+", ".")); });
var app = builder.Build();
app.UseStaticFiles();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");
app.UseStaticFiles(new StaticFileOptions
{
    /*FileProvider = new PhysicalFileProvider(imagesPath),*/
    /*FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Images")),*/
    FileProvider = new PhysicalFileProvider("D:\\FORMATIONS\\5 - POE\\GK\\Project\\site\\back\\eshop\\Images"),
    RequestPath = "/Images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policyName);
app.UseAuthorization();
/*app.UseAuthentication();*/
app.MapControllers();
app.Run();
