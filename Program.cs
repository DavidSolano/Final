using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using TestTest.Context;
using TestTest.DataModels;

namespace TestTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int choice;
            var logger = NLog.LogManager.GetCurrentClassLogger();

            do
            {
                //menu
                Console.WriteLine("what would you like to do?\n1. Search for Movie?\n" + 
                                  "2. Add Movie\n3. Update Movie\n4. Delete Movie\n5. Add New User?\n" + 
                                  "6. Enter Rating On Movie?\n7. List Top Rated Movies?\n8. Random movie based on occupation\n9. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                
                if (choice == 1)
                {
                    try
                    {
                        int option;
                        Console.Write("1. for all movies\n2. Enter a specific amount\n3. Search for a specific movie\n> ");
                        option = Convert.ToInt32(Console.ReadLine());

                        // keeps asking for option
                        while (option < 1 || option > 3)
                        {
                            Console.Write("Please enter a valid option\n> ");
                            option = Convert.ToInt32(Console.ReadLine());
                        }

                        // switch for options 1-3
                        switch (option)
                        {
                            case 1:
                            {
                                // displays all movies 
                                using var db = new MovieContext();
                                foreach (var movies in db.Movies)
                                {
                                    Console.WriteLine($"ID: {movies.Id} Movie: {movies.Title}");
                                }

                                break;
                            }
                            case 2:
                                //displays a specific amount of movies
                                int amount;
                                Console.Write("how many movies would you like to see> ");
                                amount = Convert.ToInt32(Console.ReadLine());

                                using (var db = new MovieContext())
                                {
                                    var movs = db.Movies.Take(amount).ToList();

                                    foreach (var movie in movs)
                                    {
                                        Console.WriteLine($"ID: {movie.Id} Movie: {movie.Title}");
                                    }
                                }
                                
                                break;
                            case 3:
                                // lets user search for a movie
                                Console.Write("Enter the movie you would like to search for> ");
                                var search = Console.ReadLine();
                                
                                // keeps asking for valid input
                                while (search == "")
                                {
                                    Console.Write("Please enter a valid movie> ");
                                    search = Console.ReadLine();
                                }
                                
                                // search functionality
                                using (var db = new MovieContext())
                                {
                                    var movs = db.Movies.Where(m => search != null && m.Title.ToLower().Contains(search.ToLower())).ToList();

                                    foreach (var movie in movs)
                                    {
                                        Console.WriteLine($"ID: {movie.Id} Movie: {movie.Title}");
                                    }
                                }

                                break;
                        }
                        logger.Info("displayed movies");
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to display movies");
                        throw;
                    }
                }else if (choice == 2)
                {
                    try
                    {
                        // adds a movie
                        Console.Write("enter movie title> ");
                        var movieName = Console.ReadLine();

                        // keeps asking for valid input
                        while (movieName == "")
                        {
                            Console.Write("Please enter a valid movie name\n> ");
                            movieName = Console.ReadLine();
                        }
                        
                        // creates a new movie
                        var mov = new Movie();
                        mov.Title = movieName;
                    
                        using (var db = new MovieContext())
                        {
                            db.Add(mov);
                            db.SaveChanges();
                        }
                        logger.Info("added new movies");
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to add movies");
                        throw;
                    }
                }else if (choice == 3)
                {
                    try
                    {
                        // updates movies
                        Console.Write("enter movie title you want to update: ");
                        var selectedMovie = Console.ReadLine();

                        // keeps asking for valid input
                        while (selectedMovie == "")
                        {
                            Console.Write("Please enter a valid movie name\n> ");
                            selectedMovie = Console.ReadLine();
                        }
                        
                        Console.Write("enter updated movie: ");
                        var movieUpdate = Console.ReadLine();
                        
                        // keeps asking for valid input
                        while (movieUpdate == "")
                        {
                            Console.Write("Please enter a valid movie name\n> ");
                            movieUpdate = Console.ReadLine();
                        }

                        using (var db = new MovieContext())
                        {
                            var updatedMovie = db.Movies.FirstOrDefault(x => x.Title == selectedMovie);
                            Console.WriteLine($"movie that was updated. ID: ({updatedMovie.Id}) Title: {updatedMovie.Title}");

                            updatedMovie.Title = movieUpdate;

                            db.Movies.Update(updatedMovie);
                            db.SaveChanges();
                            logger.Info("updated specified movie");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to update movies");
                        throw;
                    }
                }else if (choice == 4)
                {
                    try
                    {
                        // deletes movies
                        Console.Write("enter movie to delete: ");
                        var occ4 = Console.ReadLine();

                        // keeps asking for valid input
                        while (occ4 == "")
                        {
                            Console.Write("Please enter a valid movie name\n> ");
                            occ4 = Console.ReadLine();
                        }

                        using (var db = new MovieContext())
                        {
                            var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == occ4);
                            Console.WriteLine($"deleted movie. ID: {deleteMovie.Id} Title: {deleteMovie.Title}");

                            db.Movies.Remove(deleteMovie);
                            db.SaveChanges();
                            logger.Info("deleted specified movie");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to delete movies");
                        throw;
                    }
                }else if (choice == 5)
                {
                    try
                    {
                        // adds new user
                        Console.Write("enter your age> ");
                        long nAge = Convert.ToInt32(Console.ReadLine());

                        // keeps asking for valid input
                        while (nAge < 0)
                        {
                            Console.Write("Please enter a valid age\n> ");
                            nAge = Convert.ToInt32(Console.ReadLine());
                        }
                        
                        Console.Write("enter your gender (M/F)> ");
                        var nGender = Console.ReadLine().ToUpper();

                        // keeps asking for valid input
                        while (nGender == "")
                        {
                            System.Console.WriteLine("Please enter a valid character\n>");
                            nGender = Console.ReadLine().ToUpper();
                        }
                        
                        Console.Write("enter your zipcode> ");
                        string nZipCode = Console.ReadLine();

                        // keeps asking for valid input
                        while (nZipCode == "")
                        {
                            Console.Write("Please enter a valid zipcode\n> ");
                            nZipCode = Console.ReadLine();
                        }
                        
                        using (var db = new MovieContext())
                        {
                            foreach (var occupation in db.Occupations)
                            {
                                Console.WriteLine($"Occupation: {occupation.Name}");
                            }
                        }
                        
                        Console.Write("enter your occupation> ");
                        var nOccupation = Console.ReadLine();

                        // keeps asking for valid input
                        while (nOccupation == "")
                        {
                            Console.Write("Please enter a valid occupation\n> ");
                            nOccupation = Console.ReadLine();
                        }

                        Occupation temp = null;

                        using (var db = new MovieContext())
                        {
                            temp = db.Occupations.FirstOrDefault(x => x.Name.ToLower().Contains(nOccupation.ToLower()));
                        }
                        var user = new User()
                        {
                            Age = nAge,
                            Gender = nGender,
                            ZipCode = nZipCode,
                            Occupation = temp
                        };

                        using (var db = new MovieContext())
                        {
                            db.Users.Update(user);
                            db.SaveChanges();
                        }

                        Console.WriteLine($"Age: {nAge} Gender: {nGender} Zipcode: {nZipCode} Occupation: {nOccupation}\nhas been added");
                        logger.Info("added new user");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        logger.Error(e, "failed to add new user");
                        throw;
                    }
                }else if (choice == 6)
                {
                    try
                    {
                        // enter ratings on new movies
                        Console.Write("enter the name of the movie you want to rate> ");
                        var selectedMovie = Console.ReadLine()?.ToLower();

                        // keeps asking for valid input
                        while (selectedMovie == "")
                        {
                            Console.Write("Please enter a valid movie name\n> ");
                            selectedMovie = Console.ReadLine()?.ToLower();
                        }

                        Console.Write("enter the rating (1-5)> ");
                        long rating = Convert.ToInt64(Console.ReadLine());

                        // keeps asking for valid input
                        while (rating < 1 || rating > 5)
                        {
                            Console.Write("Please enter a valid rating\n> ");
                            rating = Convert.ToInt64(Console.ReadLine());
                        }
                        
                        using (var db = new MovieContext())
                        {
                            var user = db.Users.FirstOrDefault();
                            var movie = db.Movies.FirstOrDefault(x => selectedMovie != null && x.Title.ToLower().Contains(selectedMovie));

                            var userMovie = new UserMovie()
                            {
                                Rating = rating,
                                RatedAt = DateTime.Now
                            };

                            if (user != null) userMovie.User = user;
                            if (movie != null) userMovie.Movie = movie;

                            // displays the movie that was rated
                            Console.WriteLine($"User: {user.Id} updated movie: {movie} rating given: {rating}");

                            db.UserMovies.Add(userMovie);
                            db.SaveChanges();
                            logger.Info("entered rating");
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to enter rating");
                        throw;
                    }
                }else if (choice == 7)
                {
                    try
                    {
                        // list top movies by occupation
                        Console.WriteLine("here are the top movies by occupation:");

                        List<DataModels.Occupation> occupations;
                        using (var db = new MovieContext())
                        {
                            // I got stuck on this part and I asked justin for a bit of help. He walked me how to go through it. 
                            var upw = db.UserMovies.Include(r => r.Movie).Include(r=> r.User).Where(c=> c.Rating.Equals(5));
                            occupations = db.Occupations.ToList();

                            foreach (var jls in occupations)
                            {
                                var temp = upw.FirstOrDefault(x=> x.User.Occupation == jls);
                                Console.WriteLine($"{temp?.Movie.Title} {temp?.Rating} {jls?.Name}");
                            }
                        }
                        logger.Info("listed sorted movies by occupation");
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to display top rated movies");
                        throw;
                    }
                }else if (choice == 8)
                {
                    try
                    {
                        //show movies based on random genre picked from the total genre count
                        Console.WriteLine("here are the top movies by occupation:");

                        List<DataModels.Occupation> occupations;

                        using (var db = new MovieContext())
                        {
                            occupations = db.Occupations.ToList();
                        }

                        // random number generator for genre selection
                        var random = new Random();
                        var randomOccupation = occupations[random.Next(0, occupations.Count)];

                        using (var db = new MovieContext())
                        {
                            var upw = db.UserMovies.Include(r => r.Movie).Include(r => r.User).Where(c => c.Rating.Equals(5));
                            var temp = upw.FirstOrDefault(x => x.User.Occupation == randomOccupation);
                            Console.WriteLine($"{temp?.Movie.Title} {temp?.Rating} {randomOccupation?.Name}");
                        }
                        
                        logger.Info("listed sorted movies by genre");
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "failed to");
                        throw;
                    }
                }
            } while (choice != 9);
            NLog.LogManager.Shutdown();
            
        }
    }
}