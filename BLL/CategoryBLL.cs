using DAL.RespositoryPattern;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class CategoryBLL
    {
        IGenericRepository<Category> _categoryDal = new GenericRepository<Category>();

        public string AddCategory(Category category)
        {
            if (_categoryDal.Insert(category) > 0)
            {
                return "Success";
            }
            else
            {
                return "There is some Issue Please try after some time";
            }
        }

        public List<Category> GetCategories()
        {
            return _categoryDal.GetAll();
        }

        public int EditCategory(Category category)
        {
           return _categoryDal.Edit(category);
        }
        public int DeleteCategory(int id)
        {
            if(_categoryDal.GetById(id).Products.Any())
            {
                return -1;
            }
            return _categoryDal.Delete(_categoryDal.GetById(id));
        }
        public Category GetCategoryById(int id)
        {
            return _categoryDal.GetById(id);

        }
    }
}

