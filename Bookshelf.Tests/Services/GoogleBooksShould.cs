using Bookshelf.Services;
using NUnit.Framework;
namespace Bookshelf.Tests
{
    
    public class GoogleBooksShould : TestBase
    {
        [Ignore ("Actions problem")]
        [Test]
        public void GetBooksByIsbn()
        {
            var googleBooks = new GoogleBooks();

            var result = googleBooks.FindBookByIsbn("9781451648546");
            
            
            Assert.That(result.Title, Is.EqualTo("Steve Jobs - 101 Amazing Facts You Didn't Know"));
        }
    }
}
