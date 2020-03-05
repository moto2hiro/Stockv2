using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Tests
{
    public class BaseTest
    {
        protected IConfigurationRoot Config { get; set; }
        protected const string DOWNLOAD_PATH = @"C:\Users\Motohiro\Downloads\";

        [SetUp()]
        public void Setup()
        {
            //Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
    }
}
