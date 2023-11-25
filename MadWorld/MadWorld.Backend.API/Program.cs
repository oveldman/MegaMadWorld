using MadWorld.Backend.API.Endpoints;
using MadWorld.Backend.Application.CommonLogic.Extensions;
using MadWorld.Backend.Infrastructure.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddApplication();
builder.AddDatabase();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.AddCurriculumVitaeEndpoints();
app.MigrateDatabases();

app.Run();