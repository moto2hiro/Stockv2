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
        public virtual DbSet<Condition> Condition { get; set; }
        public virtual DbSet<Financial> Financial { get; set; }
        public virtual DbSet<StockPrice> StockPrice { get; set; }
        public virtual DbSet<SymbolMaster> SymbolMaster { get; set; }
        public virtual DbSet<Technical> Technical { get; set; }
        public virtual DbSet<WorldPrice> WorldPrice { get; set; }
        public virtual DbSet<WorldPriceSet> WorldPriceSet { get; set; }
        public virtual DbSet<Yactual> Yactual { get; set; }

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

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.Property(e => e.Condition1)
                    .IsRequired()
                    .HasColumnName("Condition")
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Financial>(entity =>
            {
                entity.Property(e => e.CurrentAssets).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentLiabilities).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DebtToEquityRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DivPayoutRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FreeCashFlow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrossProfit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MarketCap).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetIncomeGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetIncomeQqGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NopatGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NopatQqGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OperatingIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PbRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PeRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RevenueGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RevenueQqGrowth).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Roa).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Roe).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TtlAssets).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TtlEquity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TtlLiabilities).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<StockPrice>(entity =>
            {
                entity.Property(e => e.ClosePrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.HighPrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.LowPrice).HasColumnType("decimal(13, 2)");

                entity.Property(e => e.OpenPrice).HasColumnType("decimal(13, 2)");
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

            modelBuilder.Entity<Technical>(entity =>
            {
                entity.Property(e => e.CalcType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CalcValue)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
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

            modelBuilder.Entity<Yactual>(entity =>
            {
                entity.ToTable("YActual");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
