using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson4Framework4_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.Title = Properties.Settings.Default.ApplicationNameDebug;
            Console.WriteLine(Properties.Settings.Default.ApplicationNameDebug);
#else
            Console.Title = Properties.Settings.Default.ApplicationName;
            Console.WriteLine(Properties.Settings.Default.ApplicationName);
#endif

            if (string.IsNullOrEmpty(Properties.Settings.Default.FIO))
            {
                Console.WriteLine("Введите ваше ФИО:");
                Properties.Settings.Default.FIO = Console.ReadLine();
            }

            if (Properties.Settings.Default.Age <= 0)
            {
                Console.WriteLine("Введите ваш возраст:");
                if (!Int32.TryParse(Console.ReadLine(), out int age))
                    Console.WriteLine("Ввозраст введен неверно.");
                else
                    Properties.Settings.Default.Age = age;
            }
            Properties.Settings.Default.Save();


            // %USERPROFILE%\AppData\Local\
            Console.WriteLine($"ФИО: {Properties.Settings.Default.FIO}");
            Console.WriteLine($"Возраст: {Properties.Settings.Default.Age} \n");

            //CryptographyExample();

            UnCryptographyExample();

            Console.ReadKey(true);
        }
        private static void CryptographyExample()
        {
            ConnectionString connectionString1 = new ConnectionString
            {
                DatabaseName = "Database1",
                Host = "localhost",
                Password = "password1",
                UserName = "User1"
            };
            ConnectionString connectionString2 = new ConnectionString
            {
                DatabaseName = "Database2",
                Host = "localhost",
                Password = "password2",
                UserName = "User2"
            };

            List<ConnectionString> connectionStrings = new List<ConnectionString>();
            connectionStrings.Add(connectionString1);
            connectionStrings.Add(connectionString2);

            CacheProvider cacheProvider = new CacheProvider();
            cacheProvider.CacheConnection(connectionStrings);
        }

        private static void UnCryptographyExample()
        {
            CacheProvider cacheProvider = new CacheProvider();
            List<ConnectionString> connectionStrings = cacheProvider.GetConnectionsFromCache();

            foreach (ConnectionString connectionString in connectionStrings)
            {
                Console.WriteLine($" Host: {connectionString.Host},\n Databasename: {connectionString.DatabaseName},\n Password: {connectionString.Password}, \n Username: {connectionString.UserName}. \n\n");
            }
        }
    }
    
}
