using SQLIteSample.Core;
using SQLIteSample.Data;
using System;

namespace SQLIteSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var installation = new Installation();
            installation.Execute();

            var repository = new UserRepository();

            Console.WriteLine("Current users");

            var users = repository.Get();
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            var newuser = new User();
            newuser.Name = "name-" + System.Guid.NewGuid().ToString();
            newuser.Email = "email@domain.com";
            newuser.Password = "abcdef";

            repository.Insert(newuser);
        }
    }
}
