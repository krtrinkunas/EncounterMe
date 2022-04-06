using System;
using System.Linq;
using System.Collections.Generic;
using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ModeratorUI
{
    class Program
    {
        ServiceCollection serviceCollection;
        ServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            
            Console.ReadLine();
            Program pro = new Program();
            pro.startProgram();
            
        }
        Program()
        {
            serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IConsoleCommentService, InternetConsoleCommentService>();
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
        void startProgram()
        {
            Console.WriteLine("Printing...");
            printComments();
            Console.WriteLine("Printing complete.");
            if (wantToCheck("Check comment? y/n"))
            {
                readAndDeleteComment(readID());
            }
            if (wantToCheck("Check another one? y/n"))
            {
                startProgram();
            }
        }
        void printComments()
        {
            //InternetConsoleCommentService serv = new InternetConsoleCommentService();
            var comments = serviceProvider.GetService<IConsoleCommentService>().GetComments();
            comments.Wait();
            var res = comments.Result.OrderBy(com => com.Rating);
            
            foreach (var comm in res)
            {
                Console.WriteLine(comm.CommentId + " " + comm.Rating);
            }
        }
        void readAndDeleteComment(int id)
        {
            var com = serviceProvider.GetService<IConsoleCommentService>().GetComment(id);
            com.Wait();
            var comment = com.Result;
            if (comment is null)
            {
                Console.WriteLine("Comment with ID " + id + " does not exist!");
            }
            else
            {
                Console.WriteLine("Comment ID " + comment.CommentId + " with rating of " + comment.Rating + " in location ID " + comment.LocationId + " on " + comment.TimePosted + " by ID " + comment.UserId + " wrote:");
                Console.WriteLine(comment.CommentText);
                if (wantToCheck("Delete comment? y/n"))
                {
                    serviceProvider.GetService<IConsoleCommentService>().DeleteComment(com.Result);
                }
            }
        }
        bool wantToCheck(string question)
        {
            string answer;
            do
            {
                Console.WriteLine(question);
                answer = Console.ReadLine();
                if (answer != "y" && answer != "n") Console.WriteLine("Inaccurate answer, try again!");

            } while (answer != "y" && answer != "n");
            if (answer == "y") return true;
            else return false;
        }
        int readID()
        {
            int id = 0;
            try
            {
                Console.WriteLine("Write ID:");
                var idToRead = Console.ReadLine();
                id = int.Parse(idToRead);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid ID!");
                id = readID();
            }
            return id;
        }
    }
}
