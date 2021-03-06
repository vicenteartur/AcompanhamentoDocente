using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AcompanhamentoDocente
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
            services.AddScoped<IEscolaViewModel, EscolaViewModelService>();
            services.AddScoped<IColaboradorViewModel, ColaboradorViewModelService>();
            services.AddScoped<IAno, AnoService>();
            services.AddScoped<IEstado, EstadoService>();
            services.AddScoped<ICidade, CidadeService>();
            services.AddScoped<ICargo, CargoService>();
            services.AddScoped<ICCurricular, CCurricularService>();
            services.AddScoped<IAtribCCColEscViewModel, AtribcCColEscViewModelService>();
            services.AddScoped<ICriterioAvaliacao, CriterioAvaliacaoService>();
            services.AddScoped<IClassificacaoCriterio, ClassificacaoCriterioService>();
            services.AddScoped<ICriterioViewModel, CriterioViewService>();
            services.AddScoped<IAvaliacaoViewModel, AvaliacaoViewModelService>();
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                       .RequireAuthenticatedUser()
                       .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireNonAlphanumeric = true;
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<dbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("AcompanhamentoDocente")));
            services.AddIdentity<IdentityUser, IdentityRole>()
                         .AddEntityFrameworkStores<dbContext>();



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

            app.UseAuthentication();

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
