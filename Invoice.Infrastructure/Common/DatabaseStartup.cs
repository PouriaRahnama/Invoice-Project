namespace Invoice.Infrastructure.Common
{
    public static class DatabaseStartup
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Interceptors.SaveChangesInterceptor>();
            services.AddDbContextPool<IApplicationDbContext, SqlServerApplicationDbContext>((sp, options) =>
            {
                var connectionString = configuration.GetConnectionString("DefultConnection") 
                    ?? throw new Exception("connection database invalid");
                options.UseSqlServer(connectionString).AddInterceptors(sp.GetRequiredService<Interceptors.SaveChangesInterceptor>());
            }, poolSize: 16);
        }
    }
}