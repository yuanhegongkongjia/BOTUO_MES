using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DynamicForm
{
    public partial class DFSite : System.Web.UI.MasterPage
    {
        public static string AttachVersionInfo(string url)
        {
            if (url.IndexOf('?') >= 0)
            {
                return string.Format("{0}&v={1}", url, ConfigurationManager.AppSettings["JSVersion"]);
            }
            else
            {
                return string.Format("{0}?v={1}", url, ConfigurationManager.AppSettings["JSVersion"]);
            }
        }
    }
}