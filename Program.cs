var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// CORS setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(origin => true) 
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
