using MadWorld.Backend.Identity.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(
    options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UserDbContext))));

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<UserDbContext>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseDeveloperExceptionPage();

app.MapIdentityApi<IdentityUser>();
app.MigrateDatabases();

app.UseHttpsRedirection();

app.Run();
