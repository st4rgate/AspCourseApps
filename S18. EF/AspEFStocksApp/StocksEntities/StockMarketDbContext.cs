using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System;

namespace StocksEntities
{
    // Rappresenta il database (DbContext)
    // Necessari i pacchetti NuGet:
    // "Microsoft.EntityFrameworkCore.SqlServer" (Progetto MyEntities)
    // "Microsoft.EntityFrameworkCore.Tools" (Progetto MyEntities)
    // "Microsoft.EntityFrameworkCore.Design" (Progetto MyCRUD)
    // Nella console NuGet:
    // "Add-Migration Initial" (Generazione prima Migrazione verso il DB)
    // "Remove-Migration" (Rimozione Migrazione)
    // "Update-Database -Verbose" (Esecuzione Migrazione)
    public class StockMarketDbContext : DbContext
    {
        public StockMarketDbContext(DbContextOptions options) : base(options) { }
        // Definizione dataset
        public DbSet<BuyOrder> BuyOrders { get; set; }
        public DbSet<SellOrder> SellOrders { get; set; }

        // Con questo override del metodo "OnModelCreating", si effettua il mapping delle entities con le tabelle del database
        // E' anche possibile intervenire su altri parametri del database, tipo le relazioni
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mappatura entities -> tabelle
            modelBuilder.Entity<BuyOrder>().ToTable("BuyOrders");
            modelBuilder.Entity<SellOrder>().ToTable("SellOrders");
        }
    }
}
