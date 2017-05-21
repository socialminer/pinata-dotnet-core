using Microsoft.Extensions.Configuration;
using System.IO;

namespace PinataCore.IntegrationTest
{
    public class ConfigurationManager
    {
        public static string GetConnectionString(string name)
        {
            
            var config = new ConfigurationBuilder()
                // TODO: Usar linha abaixo após correção do bug de diretório (#311)
                //.SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(@"D:\Projects\pinatacore\PinataCore.IntegrationTest")
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString(name);
        }
    }
}
