using CardStorageService.Data;

namespace CardStorageService.Controllers
{
    public static class SecurityUtils
    {
        public static bool CheckString(string value)
        {
            Char[] closedChar = new Char[] { '\\', '/', '\"', ' ', '\'' };
            
            if (string.IsNullOrEmpty(value)) 
                return false;

            string[] a = value.Split(closedChar);
            
            if (a.Length > 1)
                return false;
            
            return true;
        }

       
    }
}
