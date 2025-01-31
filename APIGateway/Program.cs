using APIGateway;
using Couchbase.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
configurate(builder);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void configurate(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IRepository, Repository>();
    builder.Services.AddCouchbase(builder.Configuration.GetSection("Couchbase"));

}
