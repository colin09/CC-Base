﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C.B.Common.Mapping;
using C.B.MySql.AutoMapperConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace StmWeb {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            //添加认证Cookie信息
            services.AddAuthentication (Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie (options => {
                    options.LoginPath = new PathString ("/Sys/Login/Index");
                    options.AccessDeniedPath = new PathString ("/denied");
                });

            /*
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false; // true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy
                    .RequireAssertion(context => context.User.HasClaim(c => (c.Type == "Manager" || c.Type == "Develop")))
                );
            });*/

            services.AddDistributedMemoryCache ();
            services.AddSession (options => {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes (30);
                options.Cookie.HttpOnly = true;
            });

            //services.AddAutoMapper();
            services.AddAutoMapper (cfg => cfg.AddProfile<EditorModelMapperConfig> ());

            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_1);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseExceptionHandler ("/Home/Error");
                app.UseHsts ();
            }

            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseStaticFiles (new StaticFileOptions () {
                FileProvider = new PhysicalFileProvider (
                        Path.Combine (Directory.GetCurrentDirectory (), @"SourcesFile")),
                    RequestPath = new PathString ("/Sources"),
                    OnPrepareResponse = ctx => {
                        ctx.Context.Response.Headers.Append ("Cache-Control", "public,max-age=600");
                    }
            });
            app.UseCookiePolicy ();
            app.UseAuthentication ();
            //Mappings.RegisterMappings();
            app.UseSession ();

            app.UseMvc (routes => {
                routes.MapRoute (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute (
                    name: "manager",
                    template: "{area=Sys}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}