using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.AutoMapper;
using WebApp.Repositories;
using WebApp.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace WebApp
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
            var mappingConfig = new MapperConfiguration(mapping =>
            {
                mapping.AddProfile(new AutoMapperConfig());
            });
            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
            services.AddDbContext<docLanDbcontext>(opt => opt.UseSqlServer(Configuration["connectionString"]));
            services.AddScoped<docLanDbcontext>();
            services.AddScoped<DirectoryRepository>();
            services.AddScoped<DocumentRepository>();



            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(System.IO.Directory.GetCurrentDirectory(),"wwwroot")));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "documents")),
                RequestPath = "/documents"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "Temp")),
                RequestPath = "/compares"
            });
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<docLanDbcontext>().Database.Migrate();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
