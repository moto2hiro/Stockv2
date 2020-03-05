using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stock.Services.Models.EF
{
    public partial class SandboxContext : DbContext
    {
        public SandboxContext()
        {
        }

        public SandboxContext(DbContextOptions<SandboxContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ChartImage> ChartImage { get; set; }
        public virtual DbSet<StockPrice> StockPrice { get; set; }
        public virtual DbSet<SymbolMaster> SymbolMaster { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2JHG1EA\\SQLEXPRESS;Initial Catalog=Sandbox;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartImage>(entity =>
            {
                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.XImage)
                    .IsRequired()
                    .HasColumnName("X_Image")
                    .IsUnicode(false);

                entity.Property(e => e.YActual).HasColumnName("Y_Actual");

                entity.Property(e => e.YPredicted).HasColumnName("Y_Predicted");

                entity.Property(e => e.YPredictedProbability)
                    .HasColumnName("Y_PredictedProbability")
                    .HasColumnType("decimal(13, 2)");
            });

            modelBuilder.Entity<StockPrice>(entity =>
            {
                entity.Property(e => e.ClosePrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.HighPrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.LowPrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.OpenPrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SymbolMaster>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
