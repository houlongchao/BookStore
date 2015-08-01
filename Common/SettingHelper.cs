using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class SettingHelper
    {
        public static int PageSize
        {
            get { return int.Parse(System.Configuration.ConfigurationSettings.AppSettings["PageSize"]??"8"); } 
        }
    }
}
