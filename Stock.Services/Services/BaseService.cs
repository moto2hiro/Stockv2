using Microsoft.Data.SqlClient;
using Stock.Services.Models.EF;
using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.Services.Services
{
  interface IBaseService
  {
    int Insert<T>(T model) where T : class;
    int Insert<T>(IEnumerable<T> models) where T : class;
    int Update<T>(T model) where T : class;
    int Delete<T>(IEnumerable<T> models) where T : class;
    int DeleteAll<T>() where T : class;
  }

  // cd /path/to/here
  // dotnet ef dbcontext scaffold "Connectionstring (use one single black slash)" Microsoft.EntityFrameworkCore.SqlServer -f -o Models/EF
  public abstract class BaseService : IBaseService
  {
    private SandboxContext _DB;
    protected SandboxContext DB
    {
      get
      {
        if (_DB == null)
        {
          _DB = new SandboxContext();
          //_DB.ChangeTracker.LazyLoadingEnabled = false;
        }
        return _DB;
      }
      set
      {
        _DB = value;
      }
    }
    public int Insert<T>(T model) where T : class
    {
      DB.Set<T>().Add(model);
      return DB.SaveChanges();
    }
    public int Insert<T>(IEnumerable<T> models) where T : class
    {
      LogUtils.Debug($"START-INSERT");
      using (var connection = new SqlConnection(Configs.SandboxDb))
      {
        var bulkCopy = new SqlBulkCopy(
            connection,
            SqlBulkCopyOptions.TableLock |
            SqlBulkCopyOptions.FireTriggers |
            SqlBulkCopyOptions.UseInternalTransaction,
            null);
        bulkCopy.DestinationTableName = typeof(T).Name;
        connection.Open();
        bulkCopy.WriteToServer(PropUtils.ToDataTable(models));
        connection.Close();
      }
      LogUtils.Debug($"END-INSERT");
      return 1;
    }
    public int Update<T>(T model) where T : class
    {
      return DB.SaveChanges();
    }
    public int Update<T>(IEnumerable<T> models) where T : class
    {
      return DB.SaveChanges();
    }
    public int Delete<T>(IEnumerable<T> models) where T : class
    {
      DB.Set<T>().RemoveRange(models);
      return DB.SaveChanges();
    }
    public int DeleteAll<T>() where T : class
    {
      DB.RemoveRange(DB.Set<T>());
      return DB.SaveChanges();
    }
  }
}
