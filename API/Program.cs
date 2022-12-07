using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddSingleton<StoreContext>();

//Adding auto Mapper (Nuget Package for concatenating URLs received from API requests)
builder.Services.AddAutoMapper(typeof(Mapper));  // Could slow your application

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

builder.Services.Configure<ConfigureConnection>
(builder.Configuration.GetSection(ConfigureConnection.db_connections));

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

//Using Swagger for both development and production
app.UseSwaggerDocumentation();
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// Redirect to Error controller when endpoint doesn't exist
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
