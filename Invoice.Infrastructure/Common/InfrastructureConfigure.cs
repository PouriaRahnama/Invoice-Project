using Invoice.Infrastructure.Repository.EntitiesRepository;
using Invoice.Infrastructure.Repository.InterfacesRepository;
using Invoice.Infrastructure.UnitOfWork;
namespace Invoice.Infrastructure.Common
{
    public static class InfrastructureConfigure
    {
        public static void ApplicationConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region DI ( Registeration Services )
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion


        }
    }
}
