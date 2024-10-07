using AccountLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountLibrary.Services
{
    public class Serialization
    {
        static string FilePath = @"E:\DotNet\AccountLibrary\AccountsData.json";
        public static List<Account> Deserialze()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Account>();
            }
            var jsonData = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Account>>(jsonData);
        }
        public static string Serialize(List<Account> accounts)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine(JsonSerializer.Serialize(accounts));
                return "Movies saved Successfully!";
            }
        }
    }
}
