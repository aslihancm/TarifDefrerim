using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.BusinessLayer
{
    public class Test
    {
        public Test()
        {
            DataAccessLayer.DatabaseContext db = new DataAccessLayer.DatabaseContext();
           

            db.Categories.ToList();
        }
    }
}
