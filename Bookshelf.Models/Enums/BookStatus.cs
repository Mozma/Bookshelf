using System.ComponentModel;

namespace Bookshelf.Models
{
    public enum BookStatus
    {
        [Description("Без статуса")]
        WithoutStatus,
        [Description("Хочу прочитать")]
        WantToRead,
        [Description("Сейчас читаю")]
        NowReading,
        [Description("Прочёл")]
        Finished,
        [Description("Бросил читать")]
        QuitReading

    }
    public static class Extensions
    {
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(string.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        public static string GetName(this BookStatus status)
        {
            switch (status)
            {
                case BookStatus.WithoutStatus:
                    return "Без статуса";
                case BookStatus.NowReading:
                    return "Сейчас читаю";
                case BookStatus.Finished:
                    return "Прочёл";
                case BookStatus.WantToRead:
                    return "Хочу прочитать";
                case BookStatus.QuitReading:
                    return "Бросил читать";
                default:
                    return "Без статуса";
            }
        }
    }
}
