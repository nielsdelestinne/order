using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oder_infrastructure.Exceptions;
using Oder_infrastructure.Logging;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Order_domain.Customers;
using Order_service.Customers;
using Order_service.Items;
using Order_service.Orders;

namespace Order_api
{
    public class Startup
    {
        public Startup(ILoggerFactory logFactory, IConfiguration configuration)
        {
            ApplicationLogging.LoggerFactory = logFactory;

            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<AddressMapper>();
            services.AddTransient<EmailMapper>();
            services.AddTransient<PhoneNumberMapper>();
            services.AddTransient<CustomerMapper>();
            services.AddTransient<CustomerValidator>();
            services.AddTransient<ItemValidator>();
            services.AddTransient<OrderValidator>();

            services.AddSingleton<CustomerService>();
            services.AddSingleton<CustomerRepository>();
            services.AddSingleton<CustomerDatabase>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
