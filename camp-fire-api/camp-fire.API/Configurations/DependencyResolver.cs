// using System.Reflection;
// using HilfeOhneGrenzen.Domain.SeedWork;
// using HilfeOhneGrenzen.Infrastructure.Mail;
// using HilfeOhneGrenzen.Application.Configurations.CommandQueryHandler;
// using HilfeOhneGrenzen.Domain;
// using HilfeOhneGrenzen.Infrastructure.Cache;
// using HilfeOhneGrenzen.Infrastructure.Payment;
// using HilfeOhneGrenzen.Infrastructure.Storage;
// using MediatR;
// using HilfeOhneGrenzen.Infrastructure.Cache.InMemory;

// namespace HilfeOhneGrenzen.API.Configurations;

// public static class DependencyResolver
// {
//     public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
//         services.AddTransient<IHilfeOhneGrenzenContext, HilfeOhneGrenzenContext>();
//         services.AddTransient<IUnitOfWork, UnitOfWork>();

//         services.AddMediatR(typeof(ICommand).GetTypeInfo().Assembly);


//         /*-----Services-----*/
//         services.AddTransient<IEmailService, EmailService>();
//         services.AddScoped<ICacheService, InMemoryCacheService>();
//         services.AddTransient<IPaymentService, StripePaymentService>();
//         services.AddTransient<IStorageService, StorageService>();

//         return services;
//     }

//     public static void AddConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
//     {
//         serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
//         serviceCollection.Configure<PaymentSettings>(configuration.GetSection("PaymentSettings"));
//         serviceCollection.Configure<CacheSettings>(configuration.GetSection("Cache"));
//     }
// }
