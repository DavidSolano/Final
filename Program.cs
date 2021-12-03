using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TestTest.Context;
using TestTest.DataModels;

namespace TestTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("what would you like to do?\n1. Search for Movie?\n" + 
                                  "2. Add Movie\n3. Update Movie\n4. Delete Movie\n5. Add New User?\n" + 
                                  "6. Enter Rating On Movie?\n7. List Top Rated Movies?\n8. Undecided\n9. Exit");
                choice = Convert.ToInt32(Console.ReadLine());
                
                if (choice == 1)
                {
                    try
                    {
                        using var db = new MovieContext();
                        foreach (var movies in db.Movies)
                        {
                            Console.WriteLine($"ID: {movies.Id} Movie: {movies.Title}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to display movies {e}");
                        throw;
                    }
                }else if (choice == 2)
                {
                    try
                    {
                        Console.Write("enter movie title> ");
                        var movieName = Console.ReadLine();
                        
                        // create new movie
                        var mov = new Movie();
                        mov.Title = movieName;
                    
                        using (var db = new MovieContext())
                        {
                            db.Add(mov);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to add movie {e}");
                        throw;
                    }
                }else if (choice == 3)
                {
                    try
                    {
                        Console.Write("enter movie title you want to update: ");
                        var selectedMovie = Console.ReadLine();

                        Console.Write("enter updated movie: ");
                        var movieUpdate = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                            var updatedMovie = db.Movies.FirstOrDefault(x => x.Title == selectedMovie);
                            Console.WriteLine($"movie that was updated. ID: ({updatedMovie.Id}) Title: {updatedMovie.Title}");

                            updatedMovie.Title = movieUpdate;

                            db.Movies.Update(updatedMovie);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to update movie {e}");
                        throw;
                    }
                }else if (choice == 4)
                {
                    try
                    {
                        Console.Write("enter movie to delete: ");
                        var occ4 = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                            var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == occ4);
                            Console.WriteLine($"deleted movie. ID: {deleteMovie.Id} Title: {deleteMovie.Title}");

                            db.Movies.Remove(deleteMovie);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to delete movie {e}");
                        throw;
                    }
                }else if (choice == 5)
                {
                    try
                    {
                        // add new user
                        Console.Write("enter your age> ");
                        long nAge = Convert.ToInt32(Console.ReadLine());

                        Console.Write("enter your gender (M/F)> ");
                        var nGender = Console.ReadLine();

                        Console.Write("enter your zipcode> ");
                        string nZipCode = Console.ReadLine();

                        using (var db = new MovieContext())
                        {
                            foreach (var occupation in db.Occupations)
                            {
                                Console.WriteLine($"Occupation: {occupation.Name}");
                            }
                        }
                        
                        Console.Write("enter your occupation> ");
                        var nOccupation = Console.ReadLine();
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
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }else if (choice == 6)
                {
                    try
                    {
                        // enter rating
                        Console.Write("enter the name of the movie you want to rate> ");
                        var selectedMovie = Console.ReadLine()?.ToLower();

                        Console.Write("enter the rating (1-5)> ");
                        long rating = Convert.ToInt64(Console.ReadLine());

                        using (var db = new MovieContext())
                        {
                            var user = db.Users.FirstOrDefault();
                            var movie = db.Movies.FirstOrDefault(x => selectedMovie != null && x.Title.Contains(selectedMovie));

                            var userMovie = new UserMovie()
                            {
                                Rating = rating,
                                RatedAt = DateTime.Now
                            };

                            if (user != null) userMovie.User = user;
                            if (movie != null) userMovie.Movie = movie;

                            db.UserMovies.Add(userMovie);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to enter rating {e}");
                        throw;
                    }
                }else if (choice == 7)
                {
                    try
                    {
                        // list top movies by age bracket or occupation
                        Console.WriteLine("here are the top movies by occupation:");
                        
                        List<DataModels.Movie> movies = new List<DataModels.Movie>();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"failed to list top rated movies {e}");
                        throw;
                    }
                }else if (choice == 8)
                {
                    try
                    {
                        // undecided
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            } while (choice != 5);
        }
    }
}