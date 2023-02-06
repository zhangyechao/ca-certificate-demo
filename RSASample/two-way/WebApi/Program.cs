var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllCors",
        builder => builder.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowAllCors");

app.UseAuthorization();

app.MapControllers();

app.Run("http://*:7779");
