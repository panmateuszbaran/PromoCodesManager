using PromoCodesManager.Domain.Entities;

namespace PromoCodesManager.Domain.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        //Async methods upon actual db implementation
        T Add(T entity);
        T Update(T entity);
        bool Delete(T entity);
        T[] GetAll();
        Task<int> SaveChangesAsync();
    }
}
