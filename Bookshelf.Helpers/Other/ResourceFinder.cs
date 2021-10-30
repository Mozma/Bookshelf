using System.Windows;

namespace Bookshelf.Helpers
{
    public static class ResourceFinder
    {
        public static T Get<T>(string resourceName) where T : class
        {
            return Application.Current.TryFindResource(resourceName) as T;
        }
    }
}
