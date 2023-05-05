using MicroservicePOC1.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WeatherForecastApi", Version = "v1" });
});
builder.Services.AddSingleton<IConnectionMultiplexer, ConnectionMultiplexer>(x => 
{
    return ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING"));
});
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddOpenTelemetry()
    .WithMetrics(provider =>
    {
        provider
            .AddRuntimeInstrumentation()
            .AddProcessInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddPrometheusExporter();
    })
    .WithTracing(provider =>
    {
        provider
            .AddSource("MicrosevicePOC1")
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MicrosevicePOC1"))
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRedisInstrumentation()
            .AddZipkinExporter(o =>
            {
                o.Endpoint = new Uri(Environment.GetEnvironmentVariable("ZIPKIN_EXPORTER_ENDPOINT"));
            });
    });
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
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.MapControllers();
app.Run();