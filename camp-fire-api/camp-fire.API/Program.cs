using System.Text.Json.Serialization;
// using camp_fire.API.Hubs;
using camp_fire.Application.IServices;
using camp_fire.Application.Services;
using camp_fire.Domain;
using camp_fire.Domain.SeedWork;
using camp_fire.Infrastructure.Email;
using camp_fire.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Asp.Versioning;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using NpgsqlTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ICampFireDBContext, CampFireDBContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<IPageService, PageService>();
// builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddScoped<IUserConfirmationService, UserConfirmationService>();
// builder.Services.AddScoped<IQuestionService, QuestionService>();
// builder.Services.AddScoped<IScoreboardService, ScoreboardService>();
// builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IExperienceService, ExperienceService>();
builder.Services.AddScoped<IBoxService, BoxService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IGoogleCalendarEventService, GoogleCalendarEventService>();
builder.Services.AddScoped<IAgendaService, AgendaService>();
builder.Services.Configure<GoogleCalendarApiSettings>(builder.Configuration.GetSection("GoogleCalendarApi"));

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
// builder.Services.AddTransient<EventHub>();

var connectionString = builder.Configuration.GetConnectionString(nameof(CampFireDBContext));

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<CampFireDBContext>((optionBuilder) =>
    optionBuilder.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSignalR(e => { e.MaximumReceiveMessageSize = 102400000; });
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true)));

builder.Services.AddAutoMapper(typeof(MappingProfile));

IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
{
    {"Message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
    {"Level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
    {"CreatedDate", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
    {"Exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
    {"Properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
    {"Model", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) }
};

var logger = new LoggerConfiguration().WriteTo.PostgreSQL(connectionString, "Logs", columnWriters, Serilog.Events.LogEventLevel.Debug, needAutoCreateTable: true).CreateLogger();

Log.Logger = logger;
builder.Logging.AddSerilog(logger);
builder.Host.UseSerilog();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExperienceHub.API", Version = "v1" });

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
            {
                new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme } },
                new string[] { }
            }
    });
});

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;

    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:JwtKey"])),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration["Authentication:Url"],
        ValidAudience = builder.Configuration["Authentication:Url"],
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true
    };
});

var app = builder.Build();

var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "experiences", "images");

if (!Directory.Exists(uploadsFolder))
{
    Directory.CreateDirectory(uploadsFolder);
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "experiences", "images")),
    RequestPath = "/experiences/images"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandler("/error");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
});

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// app.MapHub<EventHub>("/eventHub");

app.Run();