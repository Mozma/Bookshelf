using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshelf.Services
{
    public static class Credentials
    {
        public static string ApiKey => Environment.GetEnvironmentVariable("BOOKSHELF_GOOGLEBOOK_API_KEY");
    }
}
