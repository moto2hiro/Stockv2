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
        public virtual DbSet<JsonItem> JsonItem { get; set; }
        public virtual DbSet<StockPrice> StockPrice { get; set; }
        public virtual DbSet<SymbolMaster> SymbolMaster { get; set; }
        public virtual DbSet<TechnicalItem> TechnicalItem { get; set; }
        public virtual DbSet<WorldPrice> WorldPrice { get; set; }
        public virtual DbSet<WorldPriceSet> WorldPriceSet { get; set; }

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

            modelBuilder.Entity<JsonItem>(entity =>
            {
                entity.Property(e => e.JsonStr)
                    .IsRequired()
                    .IsUnicode(false);
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

            modelBuilder.Entity<TechnicalItem>(entity =>
            {
                entity.Property(e => e.BollingerLowerStvDev120)
                    .HasColumnName("BollingerLowerStvDev1_20")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.BollingerLowerStvDev220)
                    .HasColumnName("BollingerLowerStvDev2_20")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.BollingerUpperStvDev120)
                    .HasColumnName("BollingerUpperStvDev1_20")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.BollingerUpperStvDev220)
                    .HasColumnName("BollingerUpperStvDev2_20")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.ClosePrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Ema10)
                    .HasColumnName("EMA10")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Ema12)
                    .HasColumnName("EMA12")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Ema26)
                    .HasColumnName("EMA26")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Ema5)
                    .HasColumnName("EMA5")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Ema9)
                    .HasColumnName("EMA9")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.LogType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rsi10)
                    .HasColumnName("RSI10")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Rsi14)
                    .HasColumnName("RSI14")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Sma200)
                    .HasColumnName("SMA200")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Sma50)
                    .HasColumnName("SMA50")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Yactual).HasColumnName("YActual");
            });

            modelBuilder.Entity<WorldPrice>(entity =>
            {
                entity.Property(e => e.ClosePrice).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.HighPrice).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.LowPrice).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.OpenPrice).HasColumnType("decimal(13, 4)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorldPriceSet>(entity =>
            {
                entity.Property(e => e.XAxjodiffNorm)
                    .HasColumnName("X_AXJODiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XBsesndiffNorm)
                    .HasColumnName("X_BSESNDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XHsidiffNorm)
                    .HasColumnName("X_HSIDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XKs11diffNorm)
                    .HasColumnName("X_KS11DiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XN225diffNorm)
                    .HasColumnName("X_N225DiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XNiftydiffNorm)
                    .HasColumnName("X_NIFTYDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XSsecdiffNorm)
                    .HasColumnName("X_SSECDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.XTwiidiffNorm)
                    .HasColumnName("X_TWIIDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.YDiaactual).HasColumnName("Y_DIAActual");

                entity.Property(e => e.YDiadiffActual)
                    .HasColumnName("Y_DIADiffActual")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YDiadiffNorm)
                    .HasColumnName("Y_DIADiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.YDiapredicted).HasColumnName("Y_DIAPredicted");

                entity.Property(e => e.YDiapredictedProb)
                    .HasColumnName("Y_DIAPredictedProb")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YDjiactual).HasColumnName("Y_DJIActual");

                entity.Property(e => e.YDjidiffActual)
                    .HasColumnName("Y_DJIDiffActual")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YDjidiffNorm)
                    .HasColumnName("Y_DJIDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.YDjipredicted).HasColumnName("Y_DJIPredicted");

                entity.Property(e => e.YDjipredictedProb)
                    .HasColumnName("Y_DJIPredictedProb")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YGspcactual).HasColumnName("Y_GSPCActual");

                entity.Property(e => e.YGspcdiffActual)
                    .HasColumnName("Y_GSPCDiffActual")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YGspcdiffNorm)
                    .HasColumnName("Y_GSPCDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.YGspcpredicted).HasColumnName("Y_GSPCPredicted");

                entity.Property(e => e.YGspcpredictedProb)
                    .HasColumnName("Y_GSPCPredictedProb")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YSpyactual).HasColumnName("Y_SPYActual");

                entity.Property(e => e.YSpydiffActual)
                    .HasColumnName("Y_SPYDiffActual")
                    .HasColumnType("decimal(13, 2)");

                entity.Property(e => e.YSpydiffNorm)
                    .HasColumnName("Y_SPYDiffNorm")
                    .HasColumnType("decimal(13, 4)");

                entity.Property(e => e.YSpypredicted).HasColumnName("Y_SPYPredicted");

                entity.Property(e => e.YSpypredictedProb)
                    .HasColumnName("Y_SPYPredictedProb")
                    .HasColumnType("decimal(13, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
