using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            webApplicationBuilder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //webApplicationBuilder.Services.AddScoped<IgenericRepository<Product>, GenericRepository<Product>>();
            //webApplicationBuilder.Services.AddScoped<IgenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //webApplicationBuilder.Services.AddScoped<IgenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            webApplicationBuilder.Services.AddScoped(typeof(IgenericRepository<>),typeof(GenericRepository<>));
            #endregion

            using var app = webApplicationBuilder.Build();

            var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbcontext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync(); //Update Database
                await StoreContextSeed.SeedAsync(_dbcontext); //Apply Seeding

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "ann error has been occured during apply the migration");
            }
            #region Configure Kestrel Middelewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapControllerRoute(
            //    //    name: "default",
            //    //    pattern: "{controller}/{action}/{id?}"
            //    //    );

            //    endpoints.MapControllers();
            //});

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
