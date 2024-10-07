using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieLibrary.Services
{
    public class Serialization
    {
        static string FilePath = @"E:\DotNet\MovieLibrary\MovieLibrary\MoviesData.json";
        public static List<Movie> Deserialization()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Movie>();
            }
            var jsonData = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Movie>>(jsonData);
        }
        public static string SerializationMoviesList(List<Movie> movies)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                sw.WriteLine(JsonSerializer.Serialize(movies));
                return "Movies saved Successfully!";
            }
        }
    }
}
