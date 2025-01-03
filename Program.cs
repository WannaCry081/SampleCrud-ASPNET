using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using SampleCrud_ASPNET.Data;
using SampleCrud_ASPNET.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SampleCrud_ASPNET.Models.Utils;
using Microsoft.Extensions.Options;
using SampleCrud_ASPNET.Services.Users;
using JSerializer = System.Text.Json.JsonSerializer;
using SampleCrud_ASPNET.Services.Notes;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region AutoMapper Configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

builder.Services.AddSwaggerGen();

ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();

#region Automatic Database Update
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataContext>();

    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    #region ModelState Validation Configuration
    services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
    #endregion

    #region Versioning Configuration
    services.AddApiVersioning(options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
    });
    #endregion

    #region Swagger Docs
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Note API",
            Version = "1.0",
            Description = "A simple Note API showcasing CRUD operation in ASP.NET WebAPI Core Framework."
        });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                []
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });
    #endregion

    #region Authentication Configuration
    var jwt = configuration.GetSection("JWT");
    var secret = jwt["Secret"];

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(secret))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = JSerializer.Serialize(
                    new { message = "Authentication token is missing or invalid." });
                return context.Response.WriteAsync(response);
            }
        };
    });
    #endregion

    #region JWT Data Binding
    services.Configure<JWTSettings>(configuration.GetSection("JWT"));
    services.AddSingleton(resolver =>
        resolver.GetRequiredService<IOptions<JWTSettings>>().Value);
    #endregion

    #region SMTP Data Binding
    services.Configure<SMTPSettings>(configuration.GetSection("JWT"));
    services.AddSingleton(resolver =>
        resolver.GetRequiredService<IOptions<SMTPSettings>>().Value);
    #endregion

    #region Setup Database Connection
    services.AddDbContext<DataContext>(
        options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    #endregion

    #region Configure Logging
    services.AddLogging();
    #endregion

    #region Services Configuration
    services.AddScoped<IAuthService, AuthService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<INoteService, NoteService>();
    #endregion
}