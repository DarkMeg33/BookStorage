using BookStorage.Extensions;
using BookStorage.Extensions.RepositoryExtensions;
using BookStorage.Repositories.AccountRepository;
using BookStorage.Repositories.BookRepository;
using BookStorage.Repositories.ChapterRepository;
using BookStorage.Repositories.CommentRepository;
using BookStorage.Repositories.UserRepository;
using BookStorage.Services.AccountService;
using BookStorage.Services.AdminService;
using BookStorage.Services.BookService;
using BookStorage.Services.ChapterService;
using BookStorage.Services.ClaimService;
using BookStorage.Services.CommentService;
using BookStorage.Services.FictionBookReaderService;
using BookStorage.Services.FileValidationService;
using BookStorage.Services.UserContextService;
using BookStorage.Services.UserService;
using BookStorage.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            builder.Services
                .AddControllersWithViews()
                .AddNewtonsoftJson(o =>
                {
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            builder.Services.SetupCookie(_appSettings);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton(_appSettings);
            builder.Services.AddUnitOfWork();
            builder.Services.AddSwaggerDocs();

            builder.Services.AddAzureBlobStorage(_appSettings);

            #region Repositories

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IChapterRepository, ChapterRepository>();

            #endregion

            #region Services

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IClaimService, ClaimService>();
            builder.Services.AddScoped<IUserContextService, UserContextService>();
            builder.Services.AddScoped<IFileValidationService, FileValidationService>();
            builder.Services.AddScoped<IFictionBookReaderService, FictionBookReaderService>();
            builder.Services.AddScoped<IChapterService, ChapterService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            #endregion

            var app = builder.Build();

            app.InitializeRepoDb();

            app.UseSwaggerDocs();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}