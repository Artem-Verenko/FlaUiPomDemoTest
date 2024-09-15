using System.Collections.Specialized;
using System.Configuration;

namespace FlaUiPomDemoTest.Config
{
    public class AppConfig
    {
        public static string GetAppPath(string appKey) => ConfigurationManager.AppSettings[appKey];
        public static int Timeout => int.Parse(ConfigurationManager.AppSettings["Timeout"]);

        public static string Username => ((NameValueCollection)ConfigurationManager.GetSection("UserCredentials"))["Username"];
        public static string Password => ((NameValueCollection)ConfigurationManager.GetSection("UserCredentials"))["Password"];
    }
}
