using System.Web;
using System.Web.Mvc;

namespace API.CMV.CUENTAS.HABERES
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
