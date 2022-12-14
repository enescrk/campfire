using System.Text.Json.Serialization;
using camp_fire.API.Hubs;
using camp_fire.Application.IServices;
using camp_fire.Application.Services;
using camp_fire.Domain;
using camp_fire.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ICampFireDBContext, CampFireDBContext>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEventService, EventService>();
// builder.Services.AddTransient<EventHub>();

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<CampFireDBContext>((optionBuilder) =>
optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CampFireDBContext))));

builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR(e =>
{
    e.MaximumReceiveMessageSize = 102400000;
});
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
                            policy.AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials()
                                  .SetIsOriginAllowed(origin => true)));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<EventHub>("/eventHub");

app.Run();
