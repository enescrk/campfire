// using System.Reflection;



// public static class DependencyResolver
// {
//     public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
//     {
//         services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
//         services.AddTransient<IUnitOfWork, UnitOfWork>();

//         services.AddMediatR(typeof(ICommand).GetTypeInfo().Assembly);


//         /*-----Services-----*/
//         services.AddTransient<IEmailService, EmailService>();
//         services.AddScoped<ICacheService, InMemoryCacheService>();
//         services.AddTransient<IStorageService, StorageService>();

//         return services;
//     }

//     public static void AddConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
//     {
//         serviceCollection.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
//         serviceCollection.Configure<CacheSettings>(configuration.GetSection("Cache"));
//     }
// }
