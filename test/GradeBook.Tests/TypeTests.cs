using System;
using Xunit;

namespace GradeBook.Tests

{ 
    public delegate string  WriteLogDelegate(string logMessage);  /* delegate -> describe and buil a new type for.NET; it describe how the mothod will look like, what is the return type of the method you expect to call, what are the parameter types and numbers of parameters that you expect to pass when you invoke this method   */


    public class TypeTests
    {
        [Fact]
        public void StringBehaveLikeValueTypes()
        {
            string name = "Magda";
            var nameUpper = MakeUppercase(name);

            Assert.Equal("Magda", name);
            Assert.Equal("MAGDA", nameUpper);

        }
        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }
        
        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);

        }
        private void SetInt(ref int z) 
        {
            z = 42;
        }
        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
           var book1 = GetBook("Book 1");
           GetBookSetName(ref book1, "New Name");
            
            Assert.Equal("New Name", book1.Name);    
        }

        private void GetBookSetName(ref Book book, string name) 
        {
            book = new Book(name);
        }
         
         [Fact]
        public void CSharpIsPassByValue()
        {
           var book1 = GetBook("Book 1");
           GetBookSetName(book1, "New Name");
            
            Assert.Equal("Book 1", book1.Name);    
        }

        private void GetBookSetName(Book book, string name) 
        {
            book = new Book(name);
        }

         [Fact]
        public void CanSetNameFromReference()
        {
           var book1 = GetBook("Book 1");
           SetName(book1, "New Name");
            
            Assert.Equal("New Name", book1.Name);    
        }

        private void SetName(Book book, string name) 
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDiffrentObjects()
        {
           var book1 = GetBook("Book 1");
           var book2 = GetBook("Book 2");
            
            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

         [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
           var book1 = GetBook("Book 1");
           var book2 = book1;
            
            // Assert.Equal("Book 1", book1.Name);
            // Assert.Equal("Book 1", book2.Name);
            Assert.Same(book1, book1);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        Book GetBook(string name) 
        {
            return new Book(name);
        }
    }
}
