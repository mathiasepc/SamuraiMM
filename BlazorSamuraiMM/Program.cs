using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SamuraiMM.Interfaces;
using SamuraiMM.Repo;


namespace BlazorSamuraiMM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddBlazoredLocalStorage();
            //builder.Services.AddSingleton<WeatherForecastService>();

            // når du møder et interface så map det til et repo...
            builder.Services.AddScoped<IHorse, HorseRepo>();
            builder.Services.AddScoped<ISamurai, SamuraiRepo>();
            builder.Services.AddScoped<IQuotes, QuotesRepo>();
            builder.Services.AddScoped<IClan, ClanRepo>();
            builder.Services.AddScoped<IBattle, BattlesRepo>();
            builder.Services.AddScoped<IBlade, BladeRepo>();
            builder.Services.AddScoped<IBattleSchema, BattleSchemaRepo>();
            builder.Services.AddScoped<ILogin, LoginRepo>();

            //builder.Services.AddScoped<IBattlesSamurai, SamuraiBattlesRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}