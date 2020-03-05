using Stock.Services.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Stock.Services.Custom
{
  public class CustomHttpClient
  {
    private UriBuilder _UriBuilder { get; set; }
    private NameValueCollection _Params { get; set; }
    
    public CustomHttpClient(string url)
    {
      _UriBuilder = new UriBuilder(url);
      _Params = HttpUtility.ParseQueryString(_UriBuilder.Query);
    }

    public void AppendParams<T>(T model, string[] propsToExclude)
    {
      if(model != null)
      {
        foreach (var prop in typeof(T).GetProperties())
        {
          if(propsToExclude == null || !propsToExclude.Contains(prop.Name))
          {
            AppendParam(prop.Name, prop.GetValue(model, null));
          }
        }
      }
    }

    public void AppendParam(string key, object value)
    {
      AppendParam(key, (value != null) ? value.ToString() : null);
    }

    public void AppendParam(string key, string value)
    {
      if(!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
      {
        _Params[key] = value;
      }
    }

    public Task<string> GetAsync()
    {
      _UriBuilder.Query = _Params.ToString();
      var url = _UriBuilder.ToString();
      return new HttpClient().GetStringAsync(url);
    }
  }
}
