using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.DataAccessLayer;
using TarifDefrerim.DataAccessLayer.EntityFramework;
using TarifDefrerim.Entity;

namespace TarifDefrerim.BusinessLayer
{
    public class Test
    {
        public Test()
        {
            Repository<Category> repo = new Repository<Category>();
            var liste = repo.List();
        }
    }
}
