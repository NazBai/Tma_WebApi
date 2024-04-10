using Microsoft.EntityFrameworkCore;
using TMA_Warehouse.Models;

namespace TMA_Warehouse.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Item> Item { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<RequestRow> RequestRow { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(i =>
            {
                i.HasKey(e => e.Item_ID);
                i.Property(e => e.Item_ID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Request>(r =>
            {
                r.HasKey(e => e.Request_ID);
                r.Property(e => e.Request_ID).ValueGeneratedOnAdd();
            });
                
            

            modelBuilder.Entity<RequestRow>(e =>
            {
                e.HasKey(e => e.Request_Row_ID);

                e.Property(r => r.Request_Row_ID).ValueGeneratedOnAdd();

                e.HasOne(d => d.Request)
                      .WithMany(p => p.RequestRows)
                      .HasForeignKey(d => d.Request_ID)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("Request_Row_Request");
            });
        }
    }



}

