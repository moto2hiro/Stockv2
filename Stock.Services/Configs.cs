using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Stock.Services
{
  // TO DO: AppSettings?
  public class Configs
  {
    public static string SandboxDb => @"Data Source=DESKTOP-2JHG1EA\SQLEXPRESS;Initial Catalog=Sandbox;Integrated Security=True";
    public static string TDAmApiKey => "QLKKOSWCWHMNWUWAOL9Z1D1MIIXYGSLF";
    public static string WtdApiKey => "YASOkww60BbNqur3z3bp0HAFrKpdDx0aAxVdvdQZqt0D7M9UveqP1sTxx0Dg";

    private static string GetAppSettings(string key, string dflt)
    {
      var ret = "";
      if (!string.IsNullOrEmpty(key))
      {
        var val = ConfigurationManager.AppSettings[key];
        if (val != null) ret = val.ToString();
      }
      if (string.IsNullOrEmpty(ret) && !string.IsNullOrEmpty(dflt))
      {
        ret = dflt;
      }
      return ret;
    }
  }
}
