using Bookshelf.Dialogs;
using Bookshelf.ViewModels;
using Ninject;

namespace Bookshelf
{
    public static class IoC
    {

        public static IKernel Kernel { get; private set; } = new StandardKernel();

        public static IUIManager UI => IoC.Get<IUIManager>();

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
            Kernel.Bind<IUIManager>().ToConstant(new UIManager());
        }

        internal static T Get<T>()
        {
            return IoC.Kernel.Get<T>();
        }
    }
}
