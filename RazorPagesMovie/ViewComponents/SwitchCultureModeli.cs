using System.Collections.Generic;
using System.Globalization;

namespace RazorPagesMovie.ViewComponents
{
    internal class SwitchCultureModeli
    {
        public List<CultureInfo> SupportedCultures { get; set; }
        public object CurrentUlCulture { get; set; }
    }
}