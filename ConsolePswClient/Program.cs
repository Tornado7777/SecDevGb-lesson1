using CardStorageService.Utils;

namespace ConsolePswClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var result = PasswordUtils.CreatePasswordHash("12345");
            Console.WriteLine(result);
        }
    }
}