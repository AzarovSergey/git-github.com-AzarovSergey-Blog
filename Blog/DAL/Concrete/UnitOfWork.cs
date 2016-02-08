using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Save all changes to database.
        /// </summary>
        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }
    }
}
