using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using web_movie.Data;
using web_movie.Data.Cart;
using web_movie.Data.Services;
using web_movie.Models;

namespace web_movie
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
            //DbContext configurations
            services.AddDbContext<AppDbcontext>(option => option.UseSqlServer(Configuration
                .GetConnectionString("DefaulConnectionString")));

            //service confiugrations
            services.AddScoped<IActorServices, ActorsService>();
            services.AddScoped<IProducerServices, ProducerServices>();
            services.AddScoped<ICinemaServices, CinemaServices>();
            services.AddScoped<IMoviesServices, MoviesServices>();
            services.AddScoped<IOrderServices, OrderServices>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            //Identity
            services.AddIdentity<ApplicationUser , ApplicationRole>()
                .AddEntityFrameworkStores<AppDbcontext>();

            services.AddAuthentication();

            services.AddSession(options => { options.Cookie.Name = "web_movie.Session"; });
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
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movies}/{action=Index}/{id?}");
            });
            //AppDb_Data.Seed_User_Role(app).Wait();
        }
    }
}
//services.AddMemoryCache();

//services.AddAuthentication(op =>
//{
//    op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//});
