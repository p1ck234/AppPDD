using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AppPDD.ModelBD
{
    public partial class BaseModel : DbContext
    {
        public BaseModel()
            : base("name=BaseModel")
        {
        }

        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
