using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.ActionFilters;

namespace SerwisAudiometryczny
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilterAttribute());
        }
    }
}
