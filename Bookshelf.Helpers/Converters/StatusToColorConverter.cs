using Bookshelf.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Bookshelf.Helpers
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is BookStatus && value != null)
            {
                BookStatus status = (BookStatus)value;
                var color = new SolidColorBrush(Colors.White);

                switch (status)
                {
                    case BookStatus.WithoutStatus:
                        color = ResourceFinder.Get<SolidColorBrush>("StatusWithoutStatusBrush");
                        break;
                    case BookStatus.NowReading:
                        color = ResourceFinder.Get<SolidColorBrush>("StatusNowReadingBrush");
                        break;
                    case BookStatus.Finished:
                        color = ResourceFinder.Get<SolidColorBrush>("StatusFinishedBrush");
                        break;
                    case BookStatus.WantToRead:
                        color = ResourceFinder.Get<SolidColorBrush>("StatusWantToReadBrush");
                        break;
                    case BookStatus.QuitReading:
                        color = ResourceFinder.Get<SolidColorBrush>("StatusQuitReadingBrush");
                        break;
                    default:
                        color = ResourceFinder.Get<SolidColorBrush>("BaseLightBlack");
                        break;

                }

                return color;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
