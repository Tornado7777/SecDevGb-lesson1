using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Lesson4Framework4_8
{
    public class CacheProvider
    {
        private static byte[] _additionalEntropy = { 5, 55, 32, 11, 22, 65 };
        public void CacheConnection(List<ConnectionString> connections)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>)); //для того, чтобы сериализовать данные в некий источник
                MemoryStream memoryStream = new MemoryStream(); //виртуальный поток(источник), что бы не работать с файловым потокм
                XmlWriter xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); // связываем виртуальный поток с xml
                xmlSerializer.Serialize(xmlWriter, connections);    //

                byte[] protectedData = Protect(memoryStream.ToArray());

                File.WriteAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected", protectedData);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Serialize data error.\n {e.Message}");

            }

        }

        public List<ConnectionString> GetConnectionsFromCache()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<ConnectionString>));
                byte[] protectedData = File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}data.protected");
                byte[] data = Unprotect(protectedData);
                return (List<ConnectionString>)xmlSerializer.Deserialize(new MemoryStream(data));
            }
            catch(Exception e)
            {
                Console.WriteLine($"Deserialize data error.\n {e.Message}");
                return null;
            }
        }

        private byte[] Protect(byte[] data)
        {
            try
            {
                return ProtectedData.Protect(data, _additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch(CryptographicException e)
            {
                Console.WriteLine($"Protected error.\n {e.Message}");
                return null;
            }
        }

        private byte[] Unprotect(byte[] data)
        {
            try
            {
                return ProtectedData.Unprotect(data, _additionalEntropy, DataProtectionScope.LocalMachine);
            }
            catch(CryptographicException e)
            {
                Console.WriteLine($"Unprotected error.\n {e.Message}");
                return null;
            }
}
    }
}
