using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TarifDefrerim.Common;
using TarifDefrerim.Entity;

namespace TarifDefrerim.Init
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            if (HttpContext.Current.Session["Login"]!=null )
            {
                TarifUser user = HttpContext.Current.Session["Login"] as TarifUser;
                return user.Username;
            }
            return "system";
        }
    }
}