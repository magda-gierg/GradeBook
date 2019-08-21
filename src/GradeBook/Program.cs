using System;
using System.Collections.Generic;



namespace GradeBook
{
    class Program 
    {
        static void Main(string[] args)
        {
            IBook book = new DiskBook("Magda's Grade Book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("**");
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }

    }
}

// encapsulation -> hiding complexities and hiding details that are unimportant at a certain level; ex. reading througt the Main methd some details aren't importatnt (access modifires like public and private give us explicit control over encapsulation and who seen the members of the class)

// polymorphism -> allows us to have object of the same type that can behave diffrently

// DRY - don't repeat yourself

//this -> implicit variable; it is always avaiable inside of the methods and of the constuctors; you use it when you want to refer to the object that is currently being operated on

//static -> are not associated with an object instatce; static members are associated with the type that they are define inside of

// var grades = new List<double>() {12.7, 10.3, 6.11, 4.1}; /*we have to describe what types of things we are going to put into that list; we have to initialize grades -> 'new' keyword; List is dynamic, we can add new things into it   */