using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Models
{
  public class BackTestResult
  {
    #region Props
    //public decimal StartCapital { get; set; }
    public int TtlNoDays { get; set; }
    public List<Transaction> Transactions { get; set; }
    #endregion

    #region Readonly Props
    public bool HasTrans => (Transactions != null);

    public decimal TtlChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Sum(t => t.ChangeInCapital)) : 0;

    //public decimal EndCapital => (HasTrans) ?
    //  StartCapital + TtlChangeInCapital : StartCapital;

    //public decimal PctChangeInCapital => (StartCapital > 0) ?
    //  NumberUtils.Round((EndCapital - StartCapital) / StartCapital) : 0;

    public decimal AvgPctChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Average(t => t.PctChangeInCapital)) : 0;

    public decimal MaxPctChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Max(t => t.PctChangeInCapital)) : 0;

    public decimal MinPctChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Min(t => t.PctChangeInCapital)) : 0;

    public decimal AvgChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Average(t => t.ChangeInCapital)) : 0;

    public decimal MaxChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Max(t => t.ChangeInCapital)) : 0;

    public decimal MinChangeInCapital => (HasTrans) ?
      NumberUtils.Round(Transactions.Min(t => t.ChangeInCapital)) : 0;

    public int TtlNoTrans => (HasTrans) ? Transactions.Count : 0;

    public decimal PctTransAvailable => (HasTrans) ?
      NumberUtils.Pct(TtlNoTrans, TtlNoDays) : 0;

    public int TtlNoProfitTrans => (HasTrans) ?
      Transactions.Where(s => s.HasProfit).Count() : 0;

    public decimal PctProfitTrans => (TtlNoTrans > 0) ?
      NumberUtils.Pct(TtlNoProfitTrans, TtlNoTrans) : 0;

    public decimal AvgHoldLength => (HasTrans) ?
      NumberUtils.Round(Transactions.Average(t => (decimal)t.HoldLength)) : 0;

    public decimal MaxHoldLength => (HasTrans) ?
      NumberUtils.Round(Transactions.Max(t => t.HoldLength)) : 0;

    public decimal MinHoldLength => (HasTrans) ?
      NumberUtils.Round(Transactions.Min(t => t.HoldLength)) : 0;
    #endregion
  }

  public class Transaction
  {
    #region Props
    public string Symbol { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Action { get; set; }
    public decimal StartPrice { get; set; }
    public decimal EndPrice { get; set; }
    public int Volume { get; set; }
    #endregion

    #region Readonly Props
    public int HoldLength => (EndDate - StartDate).Days;

    public decimal PctChangeInCapital =>
      (Action == Consts.ACTION_BUY && StartPrice > 0) ?
        NumberUtils.PctChange(StartPrice * Volume, EndPrice * Volume) :
      (Action == Consts.ACTION_SHORT && EndPrice > 0) ?
        NumberUtils.PctChange(EndPrice * Volume, StartPrice * Volume) :
      0;

    public decimal ChangeInCapital => (Action == Consts.ACTION_BUY) ?
      EndPrice * Volume - StartPrice * Volume :
      StartPrice * Volume - EndPrice * Volume;

    public bool HasProfit => (ChangeInCapital > 0);
    #endregion
  }
}
