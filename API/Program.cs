using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<StoreContext>();

/* AddScoped Creates an instance of the ProductRepository when API receives 
an HTTP request and Create a new instance of the Controllers, disposes it later */
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Adding Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//Adding auto Mapper (Nuget Package)
builder.Services.AddAutoMapper(typeof(Mapper)); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ConfigureConnection>
(builder.Configuration.GetSection(ConfigureConnection.db_connections));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
