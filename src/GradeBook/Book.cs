using System;
using System.Collections.Generic;
using System.IO;



namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject 
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;
        public abstract void AddGrade(double grade);
        public abstract Statistics GetStatistics();
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {                
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while(line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {            
            grades = new List<double>();
            Name = name;
        }
         
        public override void AddGrade(double grade)
        {            
            if(grade <= 100 && grade >= 0)
            {                
                grades.Add(grade);  
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
               throw new ArgumentException($"Invalid {nameof(grade)}");
            }            
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            
            for(var index = 0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);                            
            }
                
            return result;
        }

        private List<double> grades;
        
        public const string CATEGORY = "Science";
    }
}

                // //   property -> name of the book 
                // public string Name
                // {
                //     get { return name; }  /*returning the field */
                //     set
                //     {
                //         if (!String.IsNullOrEmpty(value))
                //         {
                //             name = value;  /*it will be the incoming value someone is trying to write into property   */
                //         }
                //     }
                // }

                // public string Name   -> auto property
                // { get; private set }

                // readonly string category = "Science";  -> keyword readonly - I can only initialize or change or write to the filed into constructor 
                // const string CATEGORY = "Science";  -> const is constant, that's never going to change; const filed are treated like they are static members of the class (you can't access static member via an object reference, only by type name)

                //field
                //private string name;



/* constructor -> constructs objects of type Book; it is another method on the class, but it has to have the same name as the class and can't have the return type; explicit constructor - explicitly initialize grades */
/* we can overloaded the constructors; we can have multiple constructors defined as long as the moethod signature for each of those constructor is unique, so diffrent types or diffrent number of parameters */
/* public -> access modifiers; they control access this particular member of the class; we can add pubic to methods, fields */
// this -> refers to the object that I'm intereacting with
// base() -> I can reference my base class; we are accessing a constructor on my base class  
// methid overloading -> it allowes us to have multiple methods using the same name inside of the type; C# complier when is looking a the method members, it is looking at more than just the method name, it is looking at method signature. The methid signature consist of the metod name, parameters types and the number of parameters to that method.           
// inheritance -> allows you to define a base class; class cpntains members, and members represent the state and behavoir of a particular class. Any members that I write in a base class, I can inherit into a derived class and allowe those base-class members to effectively be members of my derived class
//  we can reuse code; code placed inside of a method in a base class, any class that I derive from that base class will contain that base class method
// public delegate void GradeAddedDelegate(object sender, EventArgs args);   /* a delegate to define an event as part of some classes; all events in .NET typically have 2 parameters, first is of type object(you can pass anything to this parameter) -> it is sending, who is sending this event; second is event argument */
     /* class -> blueprint; it describes how I'm going to build objects. We instantiate a class that creates the objects  */