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
                                  "2. Add Movie\n3. Update Movie\n4. Delete Movie\n5. exit");
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
                }
            } while (choice != 5);
        }
    }
}