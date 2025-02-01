using code_review_analysis_platform.Data;
using code_review_analysis_platform.Repositories.Auth;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var corsSettings = builder.Configuration.GetSection("CorsSettings");
var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>();
var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>();
var allowCredentials = corsSettings.GetValue<bool>("AllowCredentials");

builder.Services.AddCors(options =>
{
    options.AddPolicy("crap-ui", policy =>
    {
        policy.WithOrigins(allowedOrigins!)
              .WithHeaders(allowedHeaders!)
              .WithMethods(allowedMethods!);

        if (allowCredentials)
        {
            policy.AllowCredentials();
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("crap-ui");
app.UseAuthorization();

app.MapControllers();

app.Run();
