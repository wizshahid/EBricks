using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RespositoryPattern
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EBricksDBEntities db = new EBricksDBEntities();

        public bool IsExists(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Any(predicate);
        }
            public List<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            return db.Set<T>().Where(predicate).ToList();
        }
        //public List<T> GetAllWithInclude(T obj)
        //{

        //    return db.Set<T>().Include(obj.ToString()).ToList();
        //}
        public virtual IQueryable<T> GetAllQueryable()
        {

            IQueryable<T> query = db.Set<T>();
            return query;
        }

        public List<T> GetAll()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Set<T>().ToList();

        }

        public int GetCount()
        {

            return db.Set<T>().Count();
        }
        public List<T> GetByPage(int PageNumber, int PageSize)
        {
            return GetAll().Skip((PageNumber - 1) * PageSize).Take(PageNumber * PageSize).ToList();
        }


        public T GetFirstRecord()
        {
            return db.Set<T>().FirstOrDefault();
        }

        public T GetById(long id)
        {
            return db.Set<T>().Find(id);
        }




        public int Insert(T obj)
        {
            db.Set<T>().Add(obj);
            return db.SaveChanges();

        }


        public int InsertList(List<T> obj)
        {

            db.Set<T>().AddRange(obj);
            return db.SaveChanges();

        }



        public int Edit(T obj)
        {


            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();



        }

        public int EditRange(List<T> obj)
        {

            foreach (var object1 in obj)
            {
                db.Entry(object1).State = System.Data.Entity.EntityState.Modified;
            }
            return db.SaveChanges();

        }

        public int Delete(T obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();

        }

        public int DeleteRange(List<T> obj)
        {

            db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
            return db.SaveChanges();

        }
        public T GetMax()
        {
            return db.Set<T>().Max();
        }


        
    }
}
