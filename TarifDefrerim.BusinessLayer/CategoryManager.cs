using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.DataAccessLayer.EntityFramework;
using TarifDefrerim.Entity;

namespace TarifDefrerim.BusinessLayer
{
    public class CategoryManager
    {
        Repository<Category> repo = new Repository<Category>();

        public
             List<Category> GetCategories()
            {
            return repo.List();
            }

        public Category GetCategoryById(int id)
        {
            return repo.Find(i =>i.Id==id);
        }
    }
}
