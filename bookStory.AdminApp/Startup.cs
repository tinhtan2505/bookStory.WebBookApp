using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStory.ApiIntegration.Book;
using bookStory.ApiIntegration.Language;
using bookStory.ApiIntegration.Project;
using bookStory.ApiIntegration.Role;
using bookStory.ApiIntegration.User;
using bookStory.ApiIntegration.Comment;
using bookStory.ApiIntegration.Report;
using bookStory.ApiIntegration.Rating;
using bookStory.ApiIntegration.Translation;
using bookStory.ViewModels.System.Users;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using bookStory.ApiIntegration.Paragraph;
using bookStory.AdminApp.Hubs;
using bookStory.ApiIntegration.Chat;

namespace bookStory.AdminApp
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
            services.AddHttpClient();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/Index/";
                    options.AccessDeniedPath = "/User/Forbidden/";
                });

            services.AddControllersWithViews()
                     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<IRoleApiClient, RoleApiClient>();
            services.AddTransient<ILanguageApiClient, LanguageApiClient>();
            services.AddTransient<IBookApiClient, BookApiClient>();
            services.AddTransient<IParagraphApiClient, ParagraphApiClient>();
            services.AddTransient<IProjectApiClient, ProjectApiClient>();
            services.AddTransient<ICommentApiClient, CommentApiClient>();
            services.AddTransient<IChatApiClient, ChatApiClient>();
            services.AddTransient<IRatingApiClient, RatingApiClient>();
            services.AddTransient<IReportApiClient, ReportApiClient>();
            services.AddTransient<ITranslationApiClient, TranslationApiClient>();
            services.AddSignalR();
            //services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});
            IMvcBuilder builder = services.AddRazorPages();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

#if DEBUG
            if (environment == Environments.Development)
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
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

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatSignlR>("/chatsignlr");
            });
        }
    }
}