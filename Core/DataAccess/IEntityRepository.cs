using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //buradaki T nedir, kısacası bana çalışacağım tipi söyle demek. Yani Product dersem Product olacak Category dersem Category olacak.
    //Generic repository nesnelerinde genellikle aşağıdaki operasyonlar olur sadece.
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
