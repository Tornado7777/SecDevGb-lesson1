using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter user name:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password name:");
            string userPassword = Console.ReadLine();
            UserService userService = new UserService();
            string token = userService.Authentificate(userName, userPassword);
            Console.WriteLine(token);
            Console.ReadKey(true);
        }        
    }
}