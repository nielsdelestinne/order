using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Order_domain.Customers;
using Order_service.Customers;

namespace Order_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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
            services.AddTransient<CustomerService>();
            services.AddTransient<CustomerRepository>();
            services.AddTransient<CustomerDatabase>();
            services.AddTransient<CustomerValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
