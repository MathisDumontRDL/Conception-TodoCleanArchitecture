
// Program.cs
using CleanTodo.Application;
using CleanTodo.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net.Http.Headers;
using System.Text;
public class Program
{


    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appURL = builder.Configuration.GetSection("ApiURL");
        var appInfos = builder.Configuration.GetSection("AppInfos");
        builder.Services.AddHttpClient<Broker>(client =>
        {
            client.BaseAddress = new Uri(appURL["URL"]);
            client.Timeout = TimeSpan.FromSeconds(30);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("x-client-id", appInfos["ID"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", appInfos["Secret"]);
        });
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");

        // Add Authentication services
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"]!,
                ValidAudience = jwtSettings["Audience"]!,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!))
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey("jwt"))
                    {
                        context.Token = context.Request.Cookies["jwt"];
                    }
                    return Task.CompletedTask;
                }
            };
        });

        // Add Application Layer
        builder.Services.AddApplication();

        // Add Infrastructure Layer
        builder.Services.AddInfrastructure(builder.Configuration);

        // Add Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });


        // Add Authorization
        builder.Services.AddAuthorization();
        builder.Services.AddCors(options => {
            options.AddPolicy("AllowAll", policy => {
                policy.AllowAnyOrigin()

            .AllowAnyMethod()

                .AllowAnyHeader();

            });

        });

        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseCors("AllowAll");
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
           //dbContext.Database.Migrate();
            //dbContext.Database.EnsureDeleted();
            //dbContext.Database.EnsureCreated();
       
        }
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

       

        app.UseHttpsRedirection();
        app.UseAuthentication(); // Must come before UseAuthorization
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}