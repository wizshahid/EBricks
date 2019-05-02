using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RespositoryPattern
{
    public interface IGenericRepository<T> where T : class
    {
        bool IsExists(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        List<T> GetAll();

        T GetById(long id);

        T GetMax();
        IQueryable<T> GetAllQueryable();
        List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        int Insert(T obj);
        int DeleteRange(List<T> obj);
        int Edit(T obj);
        int EditRange(List<T> obj);
        int Delete(T obj);

        int InsertList(List<T> obj);

        List<T> GetByPage(int PageNumber, int PageSize);

        int GetCount();

        T GetFirstRecord();
    }
}
