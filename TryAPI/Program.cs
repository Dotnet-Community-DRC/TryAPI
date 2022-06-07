using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TryAPI.Data;
using TryAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder.AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithOrigins("http://localhost:4200");
        });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:DefaultConnection"]));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "TryAPI", Version = "v1" });

});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(swaggerUIOptions =>
    {
        swaggerUIOptions.DocumentTitle = "TryAPI Web API";
        swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "TryAPI.");
        swaggerUIOptions.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.MapPostsEndpoints();

app.Run();