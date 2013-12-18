using System.Globalization;
using System.Web;

namespace UI.Web.Cultures
{
    public interface ICultureService
    {
        CultureInfo GetCulture();
        void SetCulture(HttpContextBase httpContext);
        void SetCulture(CultureInfo culture, HttpContextBase httpContext);
    }
}