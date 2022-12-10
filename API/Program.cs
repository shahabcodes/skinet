using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

builder.Services.Configure<ConfigureConnection>
(builder.Configuration.GetSection(ConfigureConnection.db_connections));

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

//Using Swagger for both development and production
app.UseSwaggerDocumentation();
// // Configure the HTTP request pipeline.
// //if (app.Environment.IsDevelopment())
//// {
////     app.UseSwagger();
////     app.UseSwaggerUI();
//// }

// Redirect to Error controller when endpoint doesn't exist
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
