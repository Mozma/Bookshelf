using Bookshelf.Services;
using Moq;
using System;

namespace Bookshelf.Tests
{
    public class TestBase
    {
        protected Lazy<Mock<IGoogleBooks>> GoogleBooksMockLazy {get;set;}
        protected Mock<IGoogleBooks> GoogleBooksMock => GoogleBooksMockLazy.Value;

        public TestBase()
        {
            GoogleBooksMockLazy = new Lazy<Mock<IGoogleBooks>>();
        }
    }
}
