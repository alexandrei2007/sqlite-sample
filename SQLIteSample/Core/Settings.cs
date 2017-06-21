using System.Configuration;

namespace SQLIteSample.Core
{
    public static class Settings
    {
        public static string DATABASE_NAME
        {
            get
            {
                return ConfigurationManager.AppSettings["DATABASE_NAME"];
            }
        }

        public static string DATABASE_PATH
        {
            get
            {
                return ConfigurationManager.AppSettings["DATABASE_PATH"];
            }
        }
    }
}
