global using Microsoft.EntityFrameworkCore;
global using WebApi.DataAccess;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using WebApi;

var server = Environment.GetEnvironmentVariable("MYSQL_SERVER") ?? "localhost";
var port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
var user = Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root";
var password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "password";
var database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "marko";

var connectionString = $"server={server};port={port};database={database};uid={user};pwd={password}";
Console.WriteLine($"using connection string: {connectionString}");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MarkoContext>(opts => opts.UseMySQL(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(c => c.Run(async ctx =>
{
    var exception = ctx.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    if (exception is null)
        return;
    ctx.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
    await ctx.Response.WriteAsync(exception.ToString());
}));


app.MapCRUDEndpoints<Person, MarkoContext>();
app.MapCRUDEndpoints<Address, MarkoContext>();
app.MapCRUDEndpoints<Ability, MarkoContext>();

app.Run();
