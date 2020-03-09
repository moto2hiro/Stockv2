﻿using NUnit.Framework;
using Stock.Services;
using Stock.Services.Models.CsvHelper;
using Stock.Services.Services;
using Stock.Services.Utils;
using System;
using System.IO;

namespace Stock.Tests.JobRuns
{
  /// <summary>
  /// To manually run jobs
  /// </summary>
  [TestFixture()]
  public class WorldJobRuns : BaseTest
  {
    [Test()]
    public void Run_AddWorldPriceFromCsvYahoo()
    {
      var fileNames = Directory.GetFiles($"{DOWNLOAD_PATH}Yahoo");
      foreach (var fileName in fileNames)
      {
        var symbol = Path.GetFileNameWithoutExtension(fileName);
        LogUtils.Debug(symbol);
        new WorldService().AddWorldPriceFromCsv<MapYahooWorldPrice>(symbol, fileName);
      }
    }

    [Test()]
    public void Run_AddWorldPriceFromCsvInvesting()
    {
      var fileNames = Directory.GetFiles($"{DOWNLOAD_PATH}Investing");
      foreach (var fileName in fileNames)
      {
        var symbol = Path.GetFileNameWithoutExtension(fileName);
        LogUtils.Debug(symbol.Replace("_", ""));
        new WorldService().AddWorldPriceFromCsv<MapInvestingWorldPrice>(symbol.Replace("_", ""), fileName);
      }
    }

    [Test()]
    public void Run_TransformWorldPrice()
    {
      new WorldService().TransformWorldPrice();
    }

    [Test()]
    public void Run_CreateCsvForPrediction()
    {
      var symbol = Consts.SYMBOL_GSPC;
      var fileName = $"{DOWNLOAD_PATH}world_{symbol}_a.csv";
      var dateFrom = new DateTime(1800, 1, 1);
      var dateTo = new DateTime(2022, 1, 1);
      var isBetween = true;
      var isExcludeNullYActual = true;
      new WorldService().CreateCsvForPrediction(
        symbol,
        fileName,
        dateFrom,
        dateTo,
        isBetween,
        isExcludeNullYActual);
    }
  }
}