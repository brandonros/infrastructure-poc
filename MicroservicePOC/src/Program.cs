using MicroservicePOC.Services;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WeatherForecastApi", Version = "v1" });
});
builder.Services.AddSingleton<ConnectionMultiplexer>(x => 
{
    string connectionString = System.Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
    return ConnectionMultiplexer.Connect(connectionString);
});
builder.Services.AddScoped<IRedisService, RedisService>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecastApi v1"));
}
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();