using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
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
        public List<Product> GetAll()
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
            return _productDal.GetAll();

        }

        public List<Product> GetAllByCategoryId(int id)
        {
            return _productDal.GetAll(p => p.CategoryId == id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }
    }
}
