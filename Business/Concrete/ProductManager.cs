using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length < 2)
            {
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //İş Kodları. İş kodları nedir if lerin olduğu kısım. Tüm ürünleri listelemek istiyor ama Yetkisi var mı, şöyle mi, böyle mi
            //kodlarını yazdıktan sonra diyelimki geçti veri tabanına yani veri access e diyorki bana ürünleri verebilirsin çünkü
            //ben kurallardan geçtim.
            //Benim ne yapmam gerekiyor iş kodlarını yazdıktan sonra iş kodlarından geçiyorsa nereyi çağırmam lazım Veri Erişimi
            //Kural neydi bir iş sınıfı başka sınıfları new lemez. Ne yapardık bunun için injection yapardık. Bu yüzden asla aşağıdaki
            //gibi kodu yazmamalıyız.
            //InMemoryProductDal inMemoryProductDal = new InMemoryProductDal();
            //return inMemoryProductDal.GetAll();

            //Yani ne yapacağız soyut nesne ile bağlantı kuracağız. Ne InMemeory ismi geçicek ne EntityFramework ismi geçicek.
            //Yukarıdaki tanımladığımız global değişkenle beraber yapılan constructor injection ne demek ProductManager new lendiğinde 
            //Constructor diyoki bana bir tane IProductDal referansı ver. Şu an bu InMemory de olabilir yarın EntityFramework de olabilir
            //Öbürgün NHibernate de olabilir veya Dapper da olabilir bunlar hepsi veri erişim alternatifleri. Yani Business ın bileceği tek
            //şey IProductDal. Bu herşey olabilir.

            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
