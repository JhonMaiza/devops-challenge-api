using DevOpsApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DevOpsApi.Services.JwtService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
public partial class Program { }