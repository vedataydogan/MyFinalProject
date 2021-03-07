using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        //Global değişken (Bu class için) -- InMemory çalıştığımız için veri kaynağı görevi görür.
        List<Product> _products;
        public InMemoryProductDal()
        {
            //Oracle, Sql Server, Postgres, MongoDb den geliyormuş gibi simüle ediyoruz.
            _products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
                new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
                new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
                new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1}
            };
        }
        // Bu ürünü veri kaynağımıza ekleriz. Business dan parametre olarak bize gönderilen ürünü alıyoruz veritabanına kaydediyoruz.
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //productToDelete demek silinecek ürün demek.
            //Local değişken olduğu için null değerini atadık. Asla yapılan işlemin amacı gereği new deyip bellekte gereksiz bir yer işgal ettirtme.
            //Çünkü bizim buradaki amacımız listedeki silmek istediğimiz ürünün referans değerini yakalayıp bu değişkene atamak.
            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}

            //LINQ - Language Integrated Query -- Dile gömülü sorgulama
            //LINQ ile bu liste bazlı yapıları aynen SQL gibi filtreleyebiliriz.
            //=> işaretine Lamda işareti denir. Şimdi biz burda yukarıdaki foreach döngüsünü linq ile yazalım.
            //Şimdi linq olmasaydı olay neydi listeyi dolaşmak tek tek dolaşmak. sonra bir şart koymak. SingleOrDefault() metodu tek bir eleman bulmaya yarar.
            //Bu metot bizim yerimize ürünleri (Ürün listesine uyguladığımız için -- _products) tek tek dolaşmaya yarar. p => için; p burda tek tek dolaşacak ya
            //tek tek dolaşırken verdiğimiz takma isim tıpkı foreach döngüsünde verdiğimiz takma isim gibi. Yani '_products.SingleOrDefault(p =>)' bu kod yukarıdaki foreach i
            //yapıyor. (İçi boş olan şart bloğu olmayan tab tab dediğimizde oluşan döngü). p için yani her bir p için kuralım ne diyorsun, p nin ProductId si eşit eşittir
            //benim gönderdiğim Product ın productId si. productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); Olayımız bu.
            //Yani biz listeyi klasik döngü ile dolaşmak yerine LINQ ile yazarak kodu basitleştirdik.
            //Özetlemek gerekirse her p için git bak bakayım p nin ProductId si benim gönderdiğim Product ın Id sine eşit mi. eğer eşit ise onu productToDelete değişkinine ata.
            //veya her p için p nin Id si yani o an dolaştığım ürünün Id si benim parametre ile gönderdiğim ürünün Id sine eşit ise onun referansını productToDelete değişkenine eşitle.            
            //her p için p nin Id si yani o an dolaştığım ürünün Id si benim parametre ile gönderdiğim ürünün Id sine eşit olan ürünün referansını productToDelete değişkenine eşitle.
            //SingleOrDefault() metodu bir tane arar. Genelde Id olan aramalarda bu metot kullanılır. Bunun yerine FirstOrDefault(), First() metotlarını kullansak da olur.
            //Yani özetlen çok basit bu; _products.SingleOrDefault(p => foreach yap her birine takma isim ver demek. Buda; p.ProductId == product.ProductId) kuralımız.
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll() // Veritabanındaki datayı Business a vermemiz lazım.
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        //Ne yapacaz bizim bir select yazmamız gerekli. Where de bütün elemanları döner aynı mantıkla her p için diye başlar
        //Where koşulu şunu yapar. İçerideki şarta uyan bütün elemanları yeni bir liste haline getirir ve onu döndürür. Yani SQL deki where koşulu ne yapıyordu
        //senin Where olarak verdiğin şarta uyanları yeni bir tablo yapmıyor muydu buda onu yapıyor işte.
        public List<Product> GetAllByCategory(int categoryId)
        {
            //Klasik
            //List<Product> products = null;
            //foreach (var product in _products)
            //{
            //    if (product.CategoryId == categoryId)
            //    {
            //        products.Add(product);
            //    }
            //}
            //return products;

            //LINQ ile
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        //Güncellemeye gelelim. Güncellemede de aynı. Sonuç olarak hepsi referans tipi iyi bilmekle alakalı. Benim yine güncellenecek referansı bulmam lazım.
        public void Update(Product product)
        {
            //productToUpdate demek güncellenecek ürün demek.
            //Gönderdiğim Ürün Id sine sahip olan listedeki Ürünü bul. Ki biz onu güncelleyebilelim. 
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            //Şimdi mantık kuralım, productToUpdate artık bizim listedeki elemanımız mı yani onun referansı mı. Sen artık bu productToUpdate in tüm öğeleri veya özelliklerini
            //aşağıdaki gibi birebir atama işlemi yaparak güncelleyebilirsin.
            //Tabi bunlar işin mantığı. EntityFramework veya Dapper veya NHibernate vb gibi yapılar kullandığımızda aslında bunları bizim yerimize yapacak.
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}