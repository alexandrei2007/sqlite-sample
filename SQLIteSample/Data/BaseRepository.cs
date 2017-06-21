using SQLIteSample.Core;
using System.Data.Common;
using System.Data.SQLite;

namespace SQLIteSample.Data
{
    public abstract class BaseRepository
    {
        public string ConnectionString
        {
            get
            {
                return string.Format("Data Source={0}", System.IO.Path.Combine(Settings.DATABASE_PATH, Settings.DATABASE_NAME));
            }
        }

        public SQLiteParameter CreateParameter(string parameterName, System.Data.DbType dbType, object value)
        {
            var parameter = new SQLiteParameter(parameterName, dbType);
            parameter.Value = value;

            return parameter;
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
