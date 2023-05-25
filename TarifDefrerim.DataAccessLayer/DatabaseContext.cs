using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.Entity;

namespace TarifDefrerim.DataAccessLayer
{
    internal class DatabaseContext:DbContext
    {

        public DatabaseContext():base("NOTESQL")
        {

        }
        public DbSet<TarifUser> Users { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likeds { get; set; }


    }
}
