using KolokwiumAPBD.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDBService, DBService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Optional, for Minimal APIs
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "KolokwiumAPBD", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline (formerly in Startup.Configure)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(ui =>
    {
        ui.SwaggerEndpoint("/swagger/v1/swagger.json", "KolokwiumAPBD v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();