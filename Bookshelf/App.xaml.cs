using Bookshelf.Resources.Localization;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace Bookshelf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IoC.Setup();

            SetupLocalization();
        }

        private void SetupLocalization()
        {

            CultureInfo info = new CultureInfo("ru-RU");

            LocalizedStrings.Instance.SetCulture(info);
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;

            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)));

        }



    }
}
