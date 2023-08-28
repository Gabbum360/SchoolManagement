using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.ApplicationServices.Extensions;
using SchoolProject.ApplicationServices.Features.Library;
using SchoolProject.Core.Domain.Core.Dto;
using SchoolProject.Infrastructure.Common;
using SchoolProject_Submit.ServiceCollections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddControllers();
builder.Services.Configure<MailOptions>(builder.Configuration.GetSection("MailOptions"));
builder.ConfigureSerilog();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management System API"));
}
app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management System API"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region RequestBook
app.MapPost("/requestbook", async ([FromBody] RequestBookDto model, IMediator mediator) =>
{
    var result = await mediator.Send(model);
    //if (result != null) return result;
});
#endregion
app.Run();
