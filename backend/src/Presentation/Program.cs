using Application.Extensions;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("All", corsBuilder => corsBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllers(options => { options.Filters.Add<ExceptionActionFilter>(); });

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseCors("All");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();