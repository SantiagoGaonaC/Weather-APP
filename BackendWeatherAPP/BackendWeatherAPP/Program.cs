using BackendWeatherAPP.Data;
using BackendWeatherAPP.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext => Context inject whit service in this aplication
builder.Services.AddSqlServer<WeatherDbContext>(builder.Configuration.GetConnectionString("WeatherConnection"));

//SERVICE LAYER - Servicio de WeatherService
builder.Services.AddScoped<WeatherService>();
//SERVICE LAYER - Servicio de AccountService
builder.Services.AddScoped<RegisterService>();
//SERVICE LAYER - Servicio de LoginService
builder.Services.AddScoped<LoginService>();
//SERVICE LAYER - Servicio para obtener datos del clima de la API
builder.Services.AddScoped<GetWeatherCityService>();
//SERVICE LAYER - Servicio para obtener UserId
builder.Services.AddScoped<UserIdService>();
//SERVICE DE AUTENTICACIÓN
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// Añadir configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:8080")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline. => Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Use CORS antes de UseAuthentication y UseAuthorization
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();