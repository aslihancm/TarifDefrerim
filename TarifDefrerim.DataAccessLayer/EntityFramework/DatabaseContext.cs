using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.Entity;

namespace TarifDefrerim.DataAccessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {

        public DatabaseContext():base("NOTESQL")
        {
            Database.SetInitializer(new MyInitializer());
        }
        public DbSet<TarifUser> Users { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likeds { get; set; }


    }
}
