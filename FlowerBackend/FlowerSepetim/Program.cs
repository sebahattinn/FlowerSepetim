    using CicekSepeti.Domain.Interfaces;
    using CicekSepeti.Infrastructure.Repositories;
    using CicekSepeti.Application.Validators;
    using CicekSepeti.API.Middlewares;
    using FluentValidation;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Serilog;
    using System.Text;
    // 🔥 REDIS İÇİN GEREKLİ USINGLER
    using CicekSepeti.Application.Interfaces;
    using CicekSepeti.Infrastructure.Services;

    // =======================
    // PROGRAM.CS BAŞI – PRODUCTION READY
    // =======================

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.Console()
        .CreateLogger();

    try
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Serilog Entegrasyonu
        builder.Host.UseSerilog();

        // 2. JWT Authentication Ayarları
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey!)
                    )
                };
            });

        // 3. Controllers + FluentValidation
        builder.Services.AddControllers();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<CreateFlowerValidator>();

        // 4. CORS (Vercel Bağlantısı İçin Kritik)
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        builder.Services.AddEndpointsApiExplorer();

        // 5. Swagger Setup
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CicekSepeti API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

        // 6. Dependency Injection (Repository'ler)
        builder.Services.AddScoped<IFlowerRepository, FlowerRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        // 🔥🔥 7. REDIS ENTEGRASYONU (BURAYI EKLEDİM) 🔥🔥
        // Connection string'i appsettings.json'dan okur
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "CicekSepeti_";
        });

        // Bizim yazdığımız ICacheService'i sisteme tanıtıyoruz
        builder.Services.AddScoped<ICacheService, RedisCacheService>();


        var app = builder.Build();

        // =======================
        // 8. MIDDLEWARE PIPELINE
        // =======================

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "CicekSepeti API V1");
            c.RoutePrefix = "swagger";
        });

        app.UseSerilogRequestLogging();

        app.UseHttpsRedirection();

        app.UseMiddleware<MaintenanceMiddleware>();

        app.UseCors("AllowAll");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Uygulama başlatılırken kritik hata oluştu");
    }
    finally
    {
        Log.CloseAndFlush();
    }