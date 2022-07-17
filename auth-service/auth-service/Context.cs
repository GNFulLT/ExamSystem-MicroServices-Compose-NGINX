using auth_service.Models;
using Microsoft.EntityFrameworkCore; // place this line at the beginning of file.

namespace auth_service.Services
{
    public class Context : DbContext
    {
        public static string GetConnectionString()
        {
            var providerName = "Npgsql";
            var databaseName = Environment.GetEnvironmentVariable("DB_NAME");
            var userName = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            var host = Environment.GetEnvironmentVariable("DB_HOST");
            var port = 5432;

            return $"Server={host};Port={port};User Id={userName};Password={password};Database={databaseName};";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        
        public Context() : base()
        {

        }

        public static void UpdateDatabase(IServiceProvider service)
        {
            Log.InfoW("Updatable database aranıyor");
            var dbContext = service.GetRequiredService<Context>();
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                Log.InfoW("Database de migrate edilmemiş migrationlar bulundu migrate ediliyor");
                try 
                { 
                dbContext.Database.Migrate();
                    Log.InfoW("Başarıyla Migrate Edildi.");
                }
                catch (Exception ex)
                {
                    Log.ExceptionW(ex.Message,"Migration Gerçekleştirilemedi",nameof(UpdateDatabase),nameof(Context));
                    Log.InfoW("Program Kapatılıyor");
                    throw;
                }
            }
            else
            {
                Log.InfoW("Migration Bulunamadı");
            }
            Log.InfoW("Default User Varmı Yokmu Kontrol Ediliyor");
            try
            {
                var user = dbContext.Users.Where(user => user.Id == 1).FirstOrDefault();
                if(user is null)
                {
                    Log.InfoW("User Bulunamadı Ekleme işlemi yapılıyor");
                    dbContext.Users.Add(new User());
                    dbContext.SaveChanges();
                    Log.InfoW("Default Eleman Eklendi");
                }
                else
                {
                    Log.InfoW("Default User zaten var");

                }
            }
            catch(Exception ex)
            {
                Log.ExceptionW(ex.Message, "Default User eklenirken hata", nameof(UpdateDatabase), nameof(Context));
            }
        }

        public static Context GetDatabase(IServiceProvider service)
        {
            return service.GetRequiredService<Context>();
        }
        public DbSet<Session> Sessions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
