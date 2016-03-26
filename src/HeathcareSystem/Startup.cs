using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HeathcareSystem.Services;
using Healthcare.Models;
using Newtonsoft.Json.Serialization;
using HeathcareSystem.DataStuff;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace HeathcareSystem
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<HealthCareContext>(options =>
                    options.UseInMemoryDatabase());
            services.AddIdentity<User, IdentityRole<long>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonLetterOrDigit = false; ;
                options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<HealthCareContext, long>()
            .AddDefaultTokenProviders();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<IHealthcareContext>(provider => provider.GetService<HealthCareContext>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();
            app.UseDatabaseErrorPage();

            // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859

            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                 .CreateScope();
            var context = serviceScope.ServiceProvider.GetService<HealthCareContext>();

            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<long>>>();
            SeedData(context, userManager, roleManager);



            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                   name: "api",
                   template: "api/{controller}/{action}/{id?}");
                routes.MapRoute(
                   name: "api2",
                   template: "api/{controller}/{id?}");
                routes.MapRoute("DeepLink", "{*pathInfo}", defaults: new { controller = "Home", action = "Index" });
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        private void SeedData(HealthCareContext context, UserManager<User> userManager, RoleManager<IdentityRole<long>> roleManager)
        {

            context.SeedDepartment();
            roleManager.SeedRole();
            context.SeedDisease();
            var patientProfiles = context.SeedProfile(5);
            userManager.SeedPatient(patientProfiles);
            var managerProfiles = context.SeedProfile(2);
            userManager.SeedManager(managerProfiles);
            //var doctorProfiles = context.SeedProfile(48);
            //userManager.SeedDoctoc(doctorProfiles);
            //user
            var profiles = userManager.SeedDoctor(context, 48);
            var departments = context.Departments.ToList();
            int index = 0;
            foreach (var doctor in profiles)
            {
                context.DoctorInDepartments.Add(new DoctorInDepartment { DepartmentId = departments[index].Id, DoctorId = doctor.Id });
                index++;
                if (index == departments.Count)
                {
                    index = 0;
                }
            }
            
            context.SaveChange();
            context.SeedNotification();
            context.SeedAppointment();
            context.SeedMedicalRecord();
            context.SaveChange();

        }
    }
}
