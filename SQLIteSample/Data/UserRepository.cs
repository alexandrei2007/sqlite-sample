using SQLIteSample.Core;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SQLIteSample.Data
{
    public class UserRepository : BaseRepository
    {
        public void Insert(User user)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                var sql = @"
INSERT INTO `users` (name, email, password) VALUES(@Name, @Email, @Password);
select last_insert_rowid();
";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add(CreateParameter("@name", System.Data.DbType.String, user.Name));
                    cmd.Parameters.Add(CreateParameter("@email", System.Data.DbType.String, user.Email));
                    cmd.Parameters.Add(CreateParameter("@password", System.Data.DbType.String, user.Password));
                    var r = cmd.ExecuteScalar();

                    user.Id = Convert.ToInt32(r);
                }
            }
        }

        public void Update(User user)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                var sql = @"
UPDATE `users` SET name = @name, email = @email, password = @password
WHERE id = @id;
";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add(CreateParameter("@id", System.Data.DbType.Int16, user.Id));
                    cmd.Parameters.Add(CreateParameter("@name", System.Data.DbType.String, user.Name));
                    cmd.Parameters.Add(CreateParameter("@email", System.Data.DbType.String, user.Email));
                    cmd.Parameters.Add(CreateParameter("@password", System.Data.DbType.String, user.Password));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(User user)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();

                var sql = @"
DELETE FROM `users` WHERE id = @id;
";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add(CreateParameter("@id", System.Data.DbType.Int16, user.Id));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<User> Get()
        {
            IList<User> r = new List<User>();

            using (var conn = CreateConnection())
            {
                conn.Open();

                var sql = "select * from users;";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var user = new User()
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Name = Convert.ToString(reader["name"]),
                                    Email = Convert.ToString(reader["email"]),
                                    Password = Convert.ToString(reader["password"])
                                };

                                r.Add(user);
                            }
                        }
                    }
                }

            }

            return r;
        }
    }
}
