using BookStorage.Extensions.RepositoryExtensions;
using BookStorage.Repositories.BookRepository;
using BookStorage.Services.BookService;
using BookStorage.Settings;

namespace BookStorage
{
    public class Program
    {
        private static AppSettings _appSettings;

        public static void Main(string[] args)
        {
            _appSettings = new AppSettings();

            var builder = WebApplication.CreateBuilder(args);

            IWebHostEnvironment env = builder.Environment;

            builder.Configuration
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile($"appsettings.{Environment.MachineName}.json", true)
                .AddEnvironmentVariables();

            builder.Configuration.Bind(_appSettings);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton(_appSettings);
            builder.Services.AddUnitOfWork();

            #region Repositories

            builder.Services.AddScoped<IBookRepository, BookRepository>();

            #endregion

            #region Services

            builder.Services.AddScoped<IBookService, BookService>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}