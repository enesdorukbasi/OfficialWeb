using DorukSoft.OfficialWeb.Application.Tools;
using DorukSoft.OfficialWeb.Domain.Entities;
using DorukSoft.OfficialWeb.Domain.PageEntities;
using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using DorukSoft.OfficialWeb.Domain.ThemeEntities;
using DorukSoft.OfficialWeb.Persistence.Configuration.PageEntityConfigurations;
using DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations;
using DorukSoft.OfficialWeb.Persistence.Configuration.ThemeEntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace DorukSoft.OfficialWeb.Persistence.Context
{
    public class MSSqlContext : DbContext
    {
        public MSSqlContext(DbContextOptions<MSSqlContext> options) : base(options)
        {
        }

        //ProgramEntities
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CompanyInformation> CompanyInformations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactAfterChatMessage> ContactAfterChatMessages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }

        //ThemeEntities
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<ViewerPageMedia> ViewerPageMedias { get; set; }

        //PageEntities
        public DbSet<AboutPage> AboutPages { get; set; }
        public DbSet<GenericPage> GenericPages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyInformationConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new ContactAfterChatMessagesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SocialMediaConfiguration());

            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new SliderConfiguration());
            modelBuilder.ApplyConfiguration(new ViewerPageMediaConfiguration());

            modelBuilder.ApplyConfiguration(new AboutPageConfiguration());
            modelBuilder.ApplyConfiguration(new GenericPageConfiguration());

            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { AppRoleId = 1, Definition = "SuperAdmin" },
                new AppRole { AppRoleId = 2, Definition = "Admin" }
                );

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser { AppUserId = 1, Email = "enesdorukbasi@outlook.com", Name = "Enes", Surname = "Dorukbaşı", IsActive = true, AppRoleId = 1, Password = PasswordHasher.HashPassword("Enes031178.") },
                new AppUser { AppUserId = 2, Email = "enesdirukbasi@gmail.com", Name = "Enes", Surname = "Dorukbaşı", IsActive = true, AppRoleId = 2, Password = PasswordHasher.HashPassword("123321") }
                );


            modelBuilder.Entity<AboutPage>().HasData(
                new AboutPage { AboutId = 1, Content = "null", Title = "null" }
                );

            modelBuilder.Entity<CompanyInformation>().HasData(
                new CompanyInformation { CompanyInformationId = 1, CompanyTitle = "Şirket Title", CompanyName = "Şirket Adı", Email = "Şirket Mail", PhoneNumber = "Telefon Numarası", WhatsappPhoneNumber = "Whatsapp Numarası", Address = "Adresi" }
                );
        }
    }
}
