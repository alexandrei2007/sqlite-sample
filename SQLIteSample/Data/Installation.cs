using SQLIteSample.Core;
using System.Data.SQLite;

namespace SQLIteSample.Data
{
    public class Installation : BaseRepository
    {
        public void Execute()
        {
            if (!IsInstalled())
            {
                this.Install();
                this.Feed();
            }
        }

        protected string GetDatabasePath()
        {
            return System.IO.Path.Combine(Settings.DATABASE_PATH, Settings.DATABASE_NAME);
        }

        protected bool IsInstalled()
        {
            var path = GetDatabasePath();
            return System.IO.File.Exists(path);
        }

        protected void Feed()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                var transaction = conn.BeginTransaction();

                var sql = @"
INSERT INTO `users` (name, email, password) VALUES ('Alexandre', 'alexandre@email.com', '123456');
INSERT INTO `users` (name, email, password) VALUES ('Maria', 'maria@email.com', '654321');
";
                using (var cmd = new SQLiteCommand(sql, conn, transaction))
                {
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }

        protected void Install()
        {
            var path = GetDatabasePath();

            SQLiteConnection.CreateFile(path);

            using (var conn = CreateConnection())
            {
                conn.Open();

                var sql = @"
CREATE TABLE `users` (
`id`	INTEGER PRIMARY KEY AUTOINCREMENT,
`name`	varchar(50) NOT NULL,
`email`	varchar(50) NOT NULL,
`password`	varchar(50) NOT NULL
);
";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
