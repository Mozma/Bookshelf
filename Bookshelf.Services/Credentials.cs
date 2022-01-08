namespace Bookshelf.Services
{
    public static class Credentials
    {
        public static string ApiKey => Environment.GetEnvironmentVariable("BOOKSHELF_GOOGLEBOOK_API_KEY");
    }
}
