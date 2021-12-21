namespace Bookshelf.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; }
        public BaseViewModel CurrentOverlayViewModel { get; set; }
    }
}
