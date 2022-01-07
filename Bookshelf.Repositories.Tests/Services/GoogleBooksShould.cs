using Bookshelf.Services;
using NUnit.Framework;

namespace Bookshelf.Tests
{
    public class GoogleBooksShould
    {
        [Test]
        public void GetBooksByIsbn()
        {
            var googleBooks = new GoogleBooks();

            var result = googleBooks.FindBookByIsbnAsync("9781451648546").Result;

            Assert.That(result.Title, Is.EqualTo("Steve Jobs - 101 Amazing Facts You Didn't Know"));
        }
    }
}
