
using catalog.api.Data;
using catalog.api.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
                                c.SwaggerDoc("v1", new OpenApiInfo { Title = "catalog.api", Version = "v1" })
                                );

builder.Services.AddScoped<ICatalogContext, CatalogContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


//Fpr loggin 
var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
    config.AddConfiguration(builder.Configuration.GetSection("Logging"));
}).CreateLogger("Program");
builder.Services.AddSingleton(typeof(ILogger), logger );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "catalog.api v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();


