using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Reflection;
using Catalog.Data;
using Catalog.Handlers;
using Catalog.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Register custom serializers
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
// _id: UUID("123e4567-e89b-12d3-a456-426614174000")
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
// createdDate: ISODate("2024-01-01T00:00:00Z")
// createdDate: "2024-01-01T00:00:00Z"

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add Swagger services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(GetAllBrandsHandler).Assembly
};
builder.Services.AddMediatR(ctf => ctf.RegisterServicesFromAssemblies(assemblies));

// Custom services
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();



var app = builder.Build();

// Seed mongo db on startup
using (var scope = app.Services.CreateScope())
{
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await DatabaseSeeder.SeedAsync(config);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();