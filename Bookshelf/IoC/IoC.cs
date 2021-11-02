using Bookshelf.ViewModels;
using Ninject;

namespace Bookshelf
{
    public static class IoC
    {

        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// Bind all required ViewModels
        /// </summary>
        public static void Setup()
        {
            BindViewModel();
        }

        private static void BindViewModel()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel { CurrentViewModel = new HomeViewModel() });
        }

        internal static T Get<T>()
        {
            return IoC.Kernel.Get<T>();
        }
    }
}
