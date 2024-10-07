using MovieLibrary.Exceptions;
using MovieLibrary.Models;
using MovieLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Controller
{
    public class MovieManager
    {

        static int MovieStorage = 5;
        static List<Movie> movies = new List<Movie>();

        public MovieManager()
        {
            movies = Serialization.Deserialization();
        }

        public Movie GetMovieByID(int id)
        {
            if (movies.Count == 0)
            {
                throw new ZeroMoviesException("Zero Movies exist in the store.");
            }

            var movie = movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                throw new MovieNotFoundByIdException($"No movie with ID {id} exists.");
            }
            return movie;
        }

        public Movie GetMovieByName(string name)
        {
            if (movies.Count == 0)
            {
                throw new ZeroMoviesException("Zero Movies exist in the store.");
            }

            var movie = movies.FirstOrDefault(m => m.MovieName == name);
            if (movie == null)
            {
                throw new MovieNotFoundByNameException($"No movie with the name '{name}' exists.");
            }
            return movie;
        }

        public void CreateMovie(int id, string name, string genre, int year)
        {
            if (movies.Count >= MovieStorage)
            {
                throw new MovieStoreFullException("Cannot add more than 5 movies. Movie store is full.");
            }
            if (movies.Any(m => m.MovieId == id))
            {
                throw new MovieExistByIdException($"A movie with ID {id} already exists.");
            }

            if (movies.Any(m => m.MovieName.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new MovieExistByNameException($"A movie with the name '{name}' already exists.");
            }

            movies.Add(new Movie(id, name, genre, year));
        }

        public Movie Print(int id)
        {
            Movie movie = GetMovieByID(id);
            if (movie != null)
                return movie;
            throw new MovieNotFoundByIdException($"No movie with ID {id} exists.");

        }

        public List<Movie> PrintAllMovies()
        {
            if (movies.Count == 0)
            {
                throw new ZeroMoviesException("Zero Movies exist in the store.");
            }

            return movies;
        }

        public void ClearAllMovies()
        {
            if (movies.Count == 0)
            {
                throw new ZeroMoviesException("Zero Movies exist in the store.");
            }

            movies.Clear();
        }

        public void DeleteMovie(int id)
        {
            Movie movie = GetMovieByID(id);
            if (movie != null)
                movies.Remove(movie);
        }

        public void EditMovie(int id, string name, string genre, int year)
        {
            Movie movie = GetMovieByID(id);
            if (movie != null)
            {
                movie.MovieName = name;
                movie.MovieGenre = genre;
                movie.MovieYear = year;
            }
        }

        public string SerializationMovies()
        {
            return Serialization.SerializationMoviesList(movies);
        }
    }
}
