using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFLocalizeExtension.Engine;

namespace Bookshelf.Resources.Localization
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {

        }

        public static LocalizedStrings Instance = new LocalizedStrings();

        public void SetCulture(string cultureCode)
        {
            LocalizeDictionary.Instance.Culture = new CultureInfo(cultureCode);
        }

        public void SetCulture(CultureInfo info)
        {
            LocalizeDictionary.Instance.Culture = info;
        }

        public string this[string key] 
        {
            get
            {
                var result = LocalizeDictionary.Instance.GetLocalizedObject("Bookshelf.Resources", "Strings", key, LocalizeDictionary.Instance.Culture);
                return result as string;    
            }
        }
    }
}
