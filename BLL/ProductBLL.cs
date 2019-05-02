using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.RespositoryPattern;
using System.IO;
using System.Web;

namespace BLL
{
   public class ProductBLL
    {
        IGenericRepository<Product> _productDal = new GenericRepository<Product>();
        IGenericRepository<ProductImage> _productImageDal = new GenericRepository<ProductImage>();

        public string AddProduct(Product product)
        {
            

            if(_productDal.Insert(product)>0)
            {
                if(product.Files!=null)
                {
                    List<ProductImage> productImages = new List<ProductImage>();
                    foreach (var item in product.Files)
                    {
                        ProductImage productImage = new ProductImage();
                        productImage.ImagePath = CommonBLL.UploadImage(item, "ProductImages");
                        productImage.ProductId = product.Id;
                        productImages.Add(productImage);

                    }
                    _productImageDal.InsertList(productImages);
                }
               
                return "Success";
            }
            else
            {
                return "There is some Issue Please try after some time";
            }
        }

        public List<Product> GetProducts()
        {
            return _productDal.FindBy(x=>true);
        }

        public int EditProduct(Product product)
        {
            if (product.Files != null)
            {
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in product.Files)
                {
                    ProductImage productImage = new ProductImage();
                    productImage.ImagePath = CommonBLL.UploadImage(item, "ProductImages");
                    productImage.ProductId = product.Id;
                    productImages.Add(productImage);

                }
                _productImageDal.InsertList(productImages);
            }

            return _productDal.Edit(product);
        }
        public int DeleteProduct(long id)
        {
            if (_productDal.GetById(id).Bookings.Any())
            {
                return -1;
            }
           foreach(var item in _productImageDal.FindBy(x => x.ProductId == id))
            {
                File.Delete(HttpContext.Current.Server.MapPath(item.ImagePath));
                _productImageDal.Delete(_productImageDal.GetById(item.Id));
            }
            return _productDal.Delete(_productDal.GetById(id));
        }
        public Product GetProductById(long id)
        {
            return _productDal.FindBy(x=>x.Id==id).SingleOrDefault();

        }

        public int DeleteImage(long id,string imagePath)
        {
            File.Delete(HttpContext.Current.Server.MapPath(imagePath));
          return  _productImageDal.Delete(_productImageDal.GetById(id));
        }
    }
}
