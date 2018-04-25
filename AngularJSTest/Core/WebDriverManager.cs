using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace AngularJSTest.Core
{
    public class WebDriverManager
    {
        public void SetupDriver(string browserName)
        {
            //Default value
            IDriverConfig config = new ChromeConfig();
            switch (browserName.ToLower())
            {
                case "firefox":
                    config = new FirefoxConfig();
                    break;
                case "microsoftedge":
                    config = new EdgeConfig();
                    break;


            }
            SetupConfig(config);
        }


        private void SetupConfig(IDriverConfig config)
        {
            new DriverManager().SetUpDriver(config, "Latest", Architecture.X32);
        } 

    }
}
