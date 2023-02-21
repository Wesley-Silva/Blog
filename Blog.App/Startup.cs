using AutoMapper;
using Blog.App.Areas.Identity.Data;
using Blog.Business.Interfaces;
using Blog.Business.Notificacoes;
using Blog.Business.Services;
using Blog.Data.Context;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blog.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // aqui fa�o as injecao de dependencias e referencio as classes p/ utilizar em todo o projeto
            services.AddControllersWithViews();

            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            //add context do identity
            services.AddDbContext<AspNetCoreIdentityContext>(options =>
                    options.UseSqlServer(
                        Configuration.GetConnectionString("AspNetCoreIdentityContextConnection")));

            //setando o identity
            services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<AspNetCoreIdentityContext>();

            services.AddScoped<BlogDbContext>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            services.AddScoped<IComentarioRepository, ComentarioRepository>();
            services.AddScoped<IComentarioService, ComentarioService>();

            services.AddScoped<INotificador, Notificador>();
            services.AddAutoMapper(typeof(Startup));
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
                //Middleware de tratamento de erro                
                app.UseExceptionHandler("/Error/500");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Middleware
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //para funcionar o identity na app
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Posts}/{action=Index}/{id?}");

                endpoints.MapRazorPages();
            });
        }
    }
}
