using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductTest();
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            //ProductManager ı new lemeye çalıştığımızda bize diyorki beni new leye bilmen için bana hangi veri yöntemi ile çalıştığımı
            //söylemen lazım. Bizden IProductDal tipinde birşeyler istiyor. IProductDal interface i tüm veri yönetim somut classların referansını
            //tutabildiği için biz şimdilik InMemoryProductDal class ını new leyebiliriz. Şu an bu şu demek ben InMemory çalışacam demek.
            ProductManager productManager = new ProductManager(new EfProductDal());
            //Business a diyorki bana tüm ürünleri ver diyo.
            foreach (var product in productManager.GetByUnitPrice(40, 100))
            {
                Console.WriteLine(product.ProductName);
            }
        }
    }
}