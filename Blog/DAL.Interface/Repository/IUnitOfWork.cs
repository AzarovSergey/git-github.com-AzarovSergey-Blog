using System;

namespace DAL.Interface.Repository
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
