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

        public virtual DbSet<Analysis> Analysis { get; set; }
        public virtual DbSet<ChartImage> ChartImage { get; set; }
        public virtual DbSet<Financial> Financial { get; set; }
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
            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.Property(e => e.Adpv20)
                    .HasColumnName("ADPV_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Adpv30)
                    .HasColumnName("ADPV_30")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Adpv50)
                    .HasColumnName("ADPV_50")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Adtv20)
                    .HasColumnName("ADTV_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Adtv30)
                    .HasColumnName("ADTV_30")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Adtv50)
                    .HasColumnName("ADTV_50")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgGain10)
                    .HasColumnName("AvgGain_10")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgGain14)
                    .HasColumnName("AvgGain_14")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgGain6)
                    .HasColumnName("AvgGain_6")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgLoss10)
                    .HasColumnName("AvgLoss_10")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgLoss14)
                    .HasColumnName("AvgLoss_14")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AvgLoss6)
                    .HasColumnName("AvgLoss_6")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerLowerStvDev120)
                    .HasColumnName("BollingerLowerStvDev1_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerLowerStvDev220)
                    .HasColumnName("BollingerLowerStvDev2_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerLowerStvDev2520)
                    .HasColumnName("BollingerLowerStvDev25_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerUpperStvDev120)
                    .HasColumnName("BollingerUpperStvDev1_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerUpperStvDev220)
                    .HasColumnName("BollingerUpperStvDev2_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BollingerUpperStvDev2520)
                    .HasColumnName("BollingerUpperStvDev25_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClosePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentAssets).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentLiabilities).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DebtToEquityRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DivPayoutRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema10)
                    .HasColumnName("EMA_10")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema12)
                    .HasColumnName("EMA_12")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema13)
                    .HasColumnName("EMA_13")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema26)
                    .HasColumnName("EMA_26")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema48)
                    .HasColumnName("EMA_48")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema5)
                    .HasColumnName("EMA_5")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ema9)
                    .HasColumnName("EMA_9")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FreeCashFlow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrossProfit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HasCrossAboveBollingerUpperStvDev220).HasColumnName("HasCrossAboveBollingerUpperStvDev2_20");

                entity.Property(e => e.HasCrossAboveBollingerUpperStvDev2520).HasColumnName("HasCrossAboveBollingerUpperStvDev25_20");

                entity.Property(e => e.HasCrossBelowBollingerLowerStvDev220).HasColumnName("HasCrossBelowBollingerLowerStvDev2_20");

                entity.Property(e => e.HasCrossBelowBollingerLowerStvDev2520).HasColumnName("HasCrossBelowBollingerLowerStvDev25_20");

                entity.Property(e => e.HasEma10crossAbove12).HasColumnName("HasEMA10CrossAbove12");

                entity.Property(e => e.HasEma10crossAbove13).HasColumnName("HasEMA10CrossAbove13");

                entity.Property(e => e.HasEma10crossAbove26).HasColumnName("HasEMA10CrossAbove26");

                entity.Property(e => e.HasEma10crossAbove48).HasColumnName("HasEMA10CrossAbove48");

                entity.Property(e => e.HasEma10crossBelow12).HasColumnName("HasEMA10CrossBelow12");

                entity.Property(e => e.HasEma10crossBelow13).HasColumnName("HasEMA10CrossBelow13");

                entity.Property(e => e.HasEma10crossBelow26).HasColumnName("HasEMA10CrossBelow26");

                entity.Property(e => e.HasEma10crossBelow48).HasColumnName("HasEMA10CrossBelow48");

                entity.Property(e => e.HasEma12crossAbove13).HasColumnName("HasEMA12CrossAbove13");

                entity.Property(e => e.HasEma12crossAbove26).HasColumnName("HasEMA12CrossAbove26");

                entity.Property(e => e.HasEma12crossAbove48).HasColumnName("HasEMA12CrossAbove48");

                entity.Property(e => e.HasEma12crossBelow13).HasColumnName("HasEMA12CrossBelow13");

                entity.Property(e => e.HasEma12crossBelow26).HasColumnName("HasEMA12CrossBelow26");

                entity.Property(e => e.HasEma12crossBelow48).HasColumnName("HasEMA12CrossBelow48");

                entity.Property(e => e.HasEma13crossAbove26).HasColumnName("HasEMA13CrossAbove26");

                entity.Property(e => e.HasEma13crossAbove48).HasColumnName("HasEMA13CrossAbove48");

                entity.Property(e => e.HasEma13crossBelow26).HasColumnName("HasEMA13CrossBelow26");

                entity.Property(e => e.HasEma13crossBelow48).HasColumnName("HasEMA13CrossBelow48");

                entity.Property(e => e.HasEma26crossAbove48).HasColumnName("HasEMA26CrossAbove48");

                entity.Property(e => e.HasEma26crossBelow48).HasColumnName("HasEMA26CrossBelow48");

                entity.Property(e => e.HasEma5crossAbove10).HasColumnName("HasEMA5CrossAbove10");

                entity.Property(e => e.HasEma5crossAbove12).HasColumnName("HasEMA5CrossAbove12");

                entity.Property(e => e.HasEma5crossAbove13).HasColumnName("HasEMA5CrossAbove13");

                entity.Property(e => e.HasEma5crossAbove26).HasColumnName("HasEMA5CrossAbove26");

                entity.Property(e => e.HasEma5crossAbove48).HasColumnName("HasEMA5CrossAbove48");

                entity.Property(e => e.HasEma5crossAbove9).HasColumnName("HasEMA5CrossAbove9");

                entity.Property(e => e.HasEma5crossBelow10).HasColumnName("HasEMA5CrossBelow10");

                entity.Property(e => e.HasEma5crossBelow12).HasColumnName("HasEMA5CrossBelow12");

                entity.Property(e => e.HasEma5crossBelow13).HasColumnName("HasEMA5CrossBelow13");

                entity.Property(e => e.HasEma5crossBelow26).HasColumnName("HasEMA5CrossBelow26");

                entity.Property(e => e.HasEma5crossBelow48).HasColumnName("HasEMA5CrossBelow48");

                entity.Property(e => e.HasEma5crossBelow9).HasColumnName("HasEMA5CrossBelow9");

                entity.Property(e => e.HasEma9crossAbove10).HasColumnName("HasEMA9CrossAbove10");

                entity.Property(e => e.HasEma9crossAbove12).HasColumnName("HasEMA9CrossAbove12");

                entity.Property(e => e.HasEma9crossAbove13).HasColumnName("HasEMA9CrossAbove13");

                entity.Property(e => e.HasEma9crossAbove26).HasColumnName("HasEMA9CrossAbove26");

                entity.Property(e => e.HasEma9crossAbove48).HasColumnName("HasEMA9CrossAbove48");

                entity.Property(e => e.HasEma9crossBelow10).HasColumnName("HasEMA9CrossBelow10");

                entity.Property(e => e.HasEma9crossBelow12).HasColumnName("HasEMA9CrossBelow12");

                entity.Property(e => e.HasEma9crossBelow13).HasColumnName("HasEMA9CrossBelow13");

                entity.Property(e => e.HasEma9crossBelow26).HasColumnName("HasEMA9CrossBelow26");

                entity.Property(e => e.HasEma9crossBelow48).HasColumnName("HasEMA9CrossBelow48");

                entity.Property(e => e.HasRsi10crossAbove70).HasColumnName("HasRSI10CrossAbove70");

                entity.Property(e => e.HasRsi10crossAbove75).HasColumnName("HasRSI10CrossAbove75");

                entity.Property(e => e.HasRsi10crossAbove80).HasColumnName("HasRSI10CrossAbove80");

                entity.Property(e => e.HasRsi10crossAbove85).HasColumnName("HasRSI10CrossAbove85");

                entity.Property(e => e.HasRsi10crossBelow15).HasColumnName("HasRSI10CrossBelow15");

                entity.Property(e => e.HasRsi10crossBelow20).HasColumnName("HasRSI10CrossBelow20");

                entity.Property(e => e.HasRsi10crossBelow25).HasColumnName("HasRSI10CrossBelow25");

                entity.Property(e => e.HasRsi10crossBelow30).HasColumnName("HasRSI10CrossBelow30");

                entity.Property(e => e.HasRsi14crossAbove70).HasColumnName("HasRSI14CrossAbove70");

                entity.Property(e => e.HasRsi14crossAbove75).HasColumnName("HasRSI14CrossAbove75");

                entity.Property(e => e.HasRsi14crossAbove80).HasColumnName("HasRSI14CrossAbove80");

                entity.Property(e => e.HasRsi14crossAbove85).HasColumnName("HasRSI14CrossAbove85");

                entity.Property(e => e.HasRsi14crossBelow15).HasColumnName("HasRSI14CrossBelow15");

                entity.Property(e => e.HasRsi14crossBelow20).HasColumnName("HasRSI14CrossBelow20");

                entity.Property(e => e.HasRsi14crossBelow25).HasColumnName("HasRSI14CrossBelow25");

                entity.Property(e => e.HasRsi14crossBelow30).HasColumnName("HasRSI14CrossBelow30");

                entity.Property(e => e.HasRsi6crossAbove70).HasColumnName("HasRSI6CrossAbove70");

                entity.Property(e => e.HasRsi6crossAbove75).HasColumnName("HasRSI6CrossAbove75");

                entity.Property(e => e.HasRsi6crossAbove80).HasColumnName("HasRSI6CrossAbove80");

                entity.Property(e => e.HasRsi6crossAbove85).HasColumnName("HasRSI6CrossAbove85");

                entity.Property(e => e.HasRsi6crossBelow15).HasColumnName("HasRSI6CrossBelow15");

                entity.Property(e => e.HasRsi6crossBelow20).HasColumnName("HasRSI6CrossBelow20");

                entity.Property(e => e.HasRsi6crossBelow25).HasColumnName("HasRSI6CrossBelow25");

                entity.Property(e => e.HasRsi6crossBelow30).HasColumnName("HasRSI6CrossBelow30");

                entity.Property(e => e.HasSma100crossAbove200).HasColumnName("HasSMA100CrossAbove200");

                entity.Property(e => e.HasSma100crossBelow200).HasColumnName("HasSMA100CrossBelow200");

                entity.Property(e => e.HasSma10crossAbove100).HasColumnName("HasSMA10CrossAbove100");

                entity.Property(e => e.HasSma10crossAbove20).HasColumnName("HasSMA10CrossAbove20");

                entity.Property(e => e.HasSma10crossAbove200).HasColumnName("HasSMA10CrossAbove200");

                entity.Property(e => e.HasSma10crossAbove50).HasColumnName("HasSMA10CrossAbove50");

                entity.Property(e => e.HasSma10crossBelow100).HasColumnName("HasSMA10CrossBelow100");

                entity.Property(e => e.HasSma10crossBelow20).HasColumnName("HasSMA10CrossBelow20");

                entity.Property(e => e.HasSma10crossBelow200).HasColumnName("HasSMA10CrossBelow200");

                entity.Property(e => e.HasSma10crossBelow50).HasColumnName("HasSMA10CrossBelow50");

                entity.Property(e => e.HasSma20crossAbove100).HasColumnName("HasSMA20CrossAbove100");

                entity.Property(e => e.HasSma20crossAbove200).HasColumnName("HasSMA20CrossAbove200");

                entity.Property(e => e.HasSma20crossAbove50).HasColumnName("HasSMA20CrossAbove50");

                entity.Property(e => e.HasSma20crossBelow100).HasColumnName("HasSMA20CrossBelow100");

                entity.Property(e => e.HasSma20crossBelow200).HasColumnName("HasSMA20CrossBelow200");

                entity.Property(e => e.HasSma20crossBelow50).HasColumnName("HasSMA20CrossBelow50");

                entity.Property(e => e.HasSma50crossAbove100).HasColumnName("HasSMA50CrossAbove100");

                entity.Property(e => e.HasSma50crossAbove200).HasColumnName("HasSMA50CrossAbove200");

                entity.Property(e => e.HasSma50crossBelow100).HasColumnName("HasSMA50CrossBelow100");

                entity.Property(e => e.HasSma50crossBelow200).HasColumnName("HasSMA50CrossBelow200");

                entity.Property(e => e.HighPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsLocalMax10).HasColumnName("IsLocalMax_10");

                entity.Property(e => e.IsLocalMax20).HasColumnName("IsLocalMax_20");

                entity.Property(e => e.IsLocalMax200).HasColumnName("IsLocalMax_200");

                entity.Property(e => e.IsLocalMax50).HasColumnName("IsLocalMax_50");

                entity.Property(e => e.IsLocalMin10).HasColumnName("IsLocalMin_10");

                entity.Property(e => e.IsLocalMin20).HasColumnName("IsLocalMin_20");

                entity.Property(e => e.IsLocalMin200).HasColumnName("IsLocalMin_200");

                entity.Property(e => e.IsLocalMin50).HasColumnName("IsLocalMin_50");

                entity.Property(e => e.LongTermDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LowPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpenPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceVolume).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Roe).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rsi10)
                    .HasColumnName("RSI_10")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rsi14)
                    .HasColumnName("RSI_14")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Rsi6)
                    .HasColumnName("RSI_6")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sma10)
                    .HasColumnName("SMA_10")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sma100)
                    .HasColumnName("SMA_100")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sma20)
                    .HasColumnName("SMA_20")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sma200)
                    .HasColumnName("SMA_200")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sma50)
                    .HasColumnName("SMA_50")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TtlAssets).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TtlEquity).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TtlLiabilities).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Yactual).HasColumnName("YActual");

                entity.Property(e => e.Yactual10).HasColumnName("YActual10");

                entity.Property(e => e.Yactual11).HasColumnName("YActual11");

                entity.Property(e => e.Yactual12).HasColumnName("YActual12");

                entity.Property(e => e.Yactual13).HasColumnName("YActual13");

                entity.Property(e => e.Yactual14).HasColumnName("YActual14");

                entity.Property(e => e.Yactual15).HasColumnName("YActual15");

                entity.Property(e => e.Yactual16).HasColumnName("YActual16");

                entity.Property(e => e.Yactual17).HasColumnName("YActual17");

                entity.Property(e => e.Yactual18).HasColumnName("YActual18");

                entity.Property(e => e.Yactual19).HasColumnName("YActual19");

                entity.Property(e => e.Yactual2).HasColumnName("YActual2");

                entity.Property(e => e.Yactual20).HasColumnName("YActual20");

                entity.Property(e => e.Yactual21).HasColumnName("YActual21");

                entity.Property(e => e.Yactual22).HasColumnName("YActual22");

                entity.Property(e => e.Yactual23).HasColumnName("YActual23");

                entity.Property(e => e.Yactual24).HasColumnName("YActual24");

                entity.Property(e => e.Yactual25).HasColumnName("YActual25");

                entity.Property(e => e.Yactual26).HasColumnName("YActual26");

                entity.Property(e => e.Yactual27).HasColumnName("YActual27");

                entity.Property(e => e.Yactual28).HasColumnName("YActual28");

                entity.Property(e => e.Yactual29).HasColumnName("YActual29");

                entity.Property(e => e.Yactual3).HasColumnName("YActual3");

                entity.Property(e => e.Yactual30).HasColumnName("YActual30");

                entity.Property(e => e.Yactual31).HasColumnName("YActual31");

                entity.Property(e => e.Yactual32).HasColumnName("YActual32");

                entity.Property(e => e.Yactual33).HasColumnName("YActual33");

                entity.Property(e => e.Yactual34).HasColumnName("YActual34");

                entity.Property(e => e.Yactual35).HasColumnName("YActual35");

                entity.Property(e => e.Yactual36).HasColumnName("YActual36");

                entity.Property(e => e.Yactual37).HasColumnName("YActual37");

                entity.Property(e => e.Yactual38).HasColumnName("YActual38");

                entity.Property(e => e.Yactual39).HasColumnName("YActual39");

                entity.Property(e => e.Yactual4).HasColumnName("YActual4");

                entity.Property(e => e.Yactual40).HasColumnName("YActual40");

                entity.Property(e => e.Yactual41).HasColumnName("YActual41");

                entity.Property(e => e.Yactual42).HasColumnName("YActual42");

                entity.Property(e => e.Yactual43).HasColumnName("YActual43");

                entity.Property(e => e.Yactual44).HasColumnName("YActual44");

                entity.Property(e => e.Yactual45).HasColumnName("YActual45");

                entity.Property(e => e.Yactual46).HasColumnName("YActual46");

                entity.Property(e => e.Yactual47).HasColumnName("YActual47");

                entity.Property(e => e.Yactual48).HasColumnName("YActual48");

                entity.Property(e => e.Yactual49).HasColumnName("YActual49");

                entity.Property(e => e.Yactual5).HasColumnName("YActual5");

                entity.Property(e => e.Yactual50).HasColumnName("YActual50");

                entity.Property(e => e.Yactual51).HasColumnName("YActual51");

                entity.Property(e => e.Yactual52).HasColumnName("YActual52");

                entity.Property(e => e.Yactual53).HasColumnName("YActual53");

                entity.Property(e => e.Yactual54).HasColumnName("YActual54");

                entity.Property(e => e.Yactual55).HasColumnName("YActual55");

                entity.Property(e => e.Yactual56).HasColumnName("YActual56");

                entity.Property(e => e.Yactual57).HasColumnName("YActual57");

                entity.Property(e => e.Yactual58).HasColumnName("YActual58");

                entity.Property(e => e.Yactual59).HasColumnName("YActual59");

                entity.Property(e => e.Yactual6).HasColumnName("YActual6");

                entity.Property(e => e.Yactual60).HasColumnName("YActual60");

                entity.Property(e => e.Yactual7).HasColumnName("YActual7");

                entity.Property(e => e.Yactual8).HasColumnName("YActual8");

                entity.Property(e => e.Yactual9).HasColumnName("YActual9");
            });

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

            modelBuilder.Entity<Financial>(entity =>
            {
                entity.Property(e => e.CurrentAssets).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrentLiabilities).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DivPayoutRatio).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FreeCashFlow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.GrossProfit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LongTermDebt).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetIncome).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

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
