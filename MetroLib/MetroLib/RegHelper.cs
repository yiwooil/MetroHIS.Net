using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32;

namespace MetroLib
{
    public class RegHelper
    {
        static public string GetValue(string section, string key, string value)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.NET").CreateSubKey(section);
            return reg.GetValue(key, value).ToString();
        }

        static public void SaveValue(string section, string key, string value)
        {
            RegistryKey reg = Registry.CurrentUser.CreateSubKey("Software").CreateSubKey("MetroHIS.Net").CreateSubKey(section);
            reg.SetValue(key, value);
        }
    }
}
