using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserRepository,UserRepository>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"));
}, ServiceLifetime.Scoped);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/SelectAllColumnsOfUser", async (IUserRepository userRepository) =>
{
    var result = await userRepository.SelectAllColumnsOfUserAsync();

    if(result is null)
    {
        return Results.NotFound("No data!");
    }

    return Results.Ok(result);
});

app.MapPost("/SelectCustomColumnsOfUserAsync", async (IUserRepository userRepository, string[] columnNames) =>
{
    var result = await userRepository.SelectCustomColumnsOfUserAsync(columnNames);

    if(result is null)
    {
        return Results.NotFound("No data!");
    }

    return Results.Ok(result);
});

app.ExecuteSeedData();
app.Run();
