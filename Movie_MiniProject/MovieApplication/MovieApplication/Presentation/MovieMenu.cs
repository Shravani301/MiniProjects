using MovieLibrary.Controller;
using MovieLibrary.Exceptions;
using MovieLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApplication.Presentation
{
   public class MovieMenu
    {
        static MovieManager manager = new MovieManager();
        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("Enter your choice to perform Operation");
                Console.WriteLine("1. Add new Movie\n" +
                                  "2. Edit Movie\n" +
                                  "3. Find Movie by ID\n" +
                                  "4. Find Movie by name\n" +
                                  "5. Display All movies\n" +
                                  "6. Clear Movie\n" +
                                  "7. Clear All Movies\n" +
                                  "8. Exit");

                int choice = Convert.ToInt32(Console.ReadLine());
                DoTask(choice);
            }
        }

        public static void DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    AddMovie();
                    break;
                case 2:
                    EditMovie();
                    break;
                case 3:
                    FindMovieByID();
                    break;
                case 4:
                    FindMovieByName();
                    break;
                case 5:
                    DisplayAllMovies();
                    break;
                case 6:
                    ClearMovie();
                    break;
                case 7:
                    ClearAllMovies();
                    break;
                case 8:
                    manager.SerializationMovies();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input, please enter correct option.");
                    break;
            }
        }

        public static void AddMovie()
        {
            try
            {
                Console.WriteLine("Enter Movie ID:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Movie Name:");
                string name = Console.ReadLine()??string.Empty;
                Console.WriteLine("Enter Genre of Movie:");
                string genre = Console.ReadLine()??string.Empty;
                Console.WriteLine("Enter year of release:");
                int year = Convert.ToInt32(Console.ReadLine());
                manager.CreateMovie(id, name, genre, year);
            }
            catch (MovieStoreFullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (MovieExistByIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (MovieExistByNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void ClearAllMovies()
        {
            try
            {
                manager.ClearAllMovies();
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void ClearMovie()
        {
            try
            {
                Console.WriteLine("Enter Movie ID:");
                int id = Convert.ToInt32(Console.ReadLine());
                manager.DeleteMovie(id);
            }
            catch (MovieNotFoundByIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void DisplayAllMovies()
        {
            try
            {
                List<Movie> movies = manager.PrintAllMovies();
                Console.WriteLine("Available Movies are:");
                foreach (Movie movie in movies)
                {
                    Console.WriteLine(movie);
                }
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void EditMovie()
        {
            try
            {
                Console.WriteLine("Enter Movie ID to edit:");
                int id = Convert.ToInt32(Console.ReadLine());
                Movie movie = manager.GetMovieByID(id);
                Console.WriteLine("Enter new Movie Name:");
                string name = Console.ReadLine()?? string.Empty;
                Console.WriteLine("Enter new Genre:");
                string genre = Console.ReadLine()??string.Empty;
                Console.WriteLine("Enter new Release Year:");
                int year = Convert.ToInt32(Console.ReadLine());
                manager.EditMovie(id, name, genre, year);
            }
            catch (MovieNotFoundByIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void FindMovieByID()
        {
            try
            {
                Console.WriteLine("Enter Movie ID:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(manager.GetMovieByID(id));
            }
            catch (MovieNotFoundByIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void FindMovieByName()
        {
            try
            {
                Console.WriteLine("Enter Movie Name:");
                string name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine(manager.GetMovieByName(name));
            }
            catch (MovieNotFoundByNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ZeroMoviesException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
