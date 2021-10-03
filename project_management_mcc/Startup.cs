using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project_management_mcc.Context;
using project_management_mcc.Repositories.Data;
using project_management_mcc.Repository.Data;

namespace project_management_mcc
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
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddScoped<AccountRepository>();
            services.AddScoped<ActivityRepository>();
            services.AddScoped<ActivityHistoryRepository>();
            services.AddScoped<EmployeeRepository>();
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<JobRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<RoleRepository>();

            services.AddDbContext<MyContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("PMContext"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
