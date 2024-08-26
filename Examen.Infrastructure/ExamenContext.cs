using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Examen.ApplicationCore.Domain;
using Microsoft.AspNetCore.Identity;

namespace Examen.Infrastructure
{
    public class ExamenContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ExamenContext(DbContextOptions<ExamenContext> options)
            : base(options)
        {
        }

        public DbSet<BizAccount> BizAccounts { get; set; }
        public DbSet<Adress> Adress { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orderss { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        public DbSet<DeliveryType> DeliveryTypess { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Combi> Combis { get; set; }
        public DbSet<MenuPage> MenuPages { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemDetail> ItemDetails { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ItemPrice> ItemPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation BizAccount-Adress (One-to-Many avec suppression en cascade)
            modelBuilder.Entity<BizAccount>()
                .HasMany(b => b.Adresses)
                .WithOne(a => a.BizAccount)
                .HasForeignKey(a => a.BizAccountID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration des autres relations avec suppression restrict
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.BizAccount)
                .WithMany(b => b.Orders)
                .HasForeignKey(o => o.BizAccountID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryStatus)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliveryStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryType)
                .WithMany(d => d.Orders)
                .HasForeignKey(o => o.DeliveryTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Adress)
                .WithMany(a => a.Customers)
                .HasForeignKey(c => c.AdressID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MenuPage>()
                .HasOne(m => m.Menu)
                .WithMany(m => m.MenuPages)
                .HasForeignKey(m => m.MenuID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.MenuPage)
                .WithMany(m => m.Items)
                .HasForeignKey(i => i.PageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Combi)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CombiID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Item)
                .WithMany(i => i.OrderDetails)
                .HasForeignKey(od => od.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemDetail>()
                .HasOne(id => id.Item)
                .WithMany(i => i.ItemDetails)
                .HasForeignKey(id => id.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemDetail>()
                .HasOne(id => id.Language)
                .WithMany(l => l.ItemDetails)
                .HasForeignKey(id => id.LanguageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemPrice>()
                .HasOne(ip => ip.Item)
                .WithMany(i => i.ItemPrices)
                .HasForeignKey(ip => ip.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemPrice>()
                .HasOne(ip => ip.Currency)
                .WithMany(c => c.ItemPrices)
                .HasForeignKey(ip => ip.CurrencyID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerReview>()
                .HasOne(cr => cr.Order)
                .WithOne(o => o.CustomerReview)
                .HasForeignKey<CustomerReview>(cr => cr.OrderID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerReview>()
                .HasOne(cr => cr.Customer)
                .WithMany(c => c.CustomerReviews)
                .HasForeignKey(cr => cr.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combi>()
                .HasOne(c => c.MenuPage)
                .WithMany(mp => mp.Combis)
                .HasForeignKey(c => c.PageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Combi>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Combi)
                .HasForeignKey(i => i.CombiID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Menu>()
        .HasOne(m => m.BizAccount)
        .WithMany(b => b.Menus)
        .HasForeignKey(m => m.BizAccountID)
        .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            // Custom conventions can be added here
        }
    }
}
