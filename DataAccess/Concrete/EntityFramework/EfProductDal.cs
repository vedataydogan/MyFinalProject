using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {

    }

    //class EfProductDalExplaniton : IProductDal //Refactor edilmemiş hali açıklamalar var içinde
    //{
    //    public void Add(Product entity)
    //    {
    //        //Bu using C# a özel çok güzel bir yapı. Siz classı new lediğinizde o bellekten garbage collector belli bir zamanda düzenli
    //        //olarak gelir ve bellekten onu atar. Using içerisine yazdığınız nesneler using bitince anında garbage collectore geliyor
    //        //diyorki beni diyor bellekten at diyor. Neden çünkü context nesnesi biraz pahalıdır. O yüzden yani NorthwindContext
    //        //bellekten işi bitince atılacak. using bloğunu yapmasak da olur ama yapınca daha performanslı bir ürün geliştirmiş oluyoruz.
    //        //IDisposable pattern implementation of C# - C# ın IDisposable pattern denilen bir implemantasyonu. bu using in teknik adı.
    //        //Yani C# a özgü güçlü bir yapı. Basit bir şekilde söylersek using bloğu bittiği anda Garbage Collector a diyorki gel abi
    //        //topla bunları olay bu. Belleği hızlıca temizleme.
    //        using (NorthwindContext context = new NorthwindContext())
    //        {
    //            //addedEntity değişkeni eklenen varlık(entity) demek. var da değişkeni tanımlamadan tanımlayabileceğimiz karşısına ne atanırsa
    //            //o veri tipi olan bir yapıdır. Özelli
    //            //context.Entry() metoduna gelince özellikle InMemory de çalışırken silme ve güncellemede gördüğümüzde demiştik ki biz buraya
    //            //parametre olarak isterse alanları aynı olan bir Product gönder onu listeden silmeye çalış silemezsin. Onun referansına 
    //            //ulaşman lazım önce işte burdada benzer mantık context.Entry(entity) metodunu kullanarak git veri kaynağından benim bu
    //            //gönderdiğim Product da bir tane nesneyi eşleştir.Genel mantık bu. ama bizim işlem için ekleme olacağı için herhangi bir
    //            //şey ile eşleştirmeyecek onun yerine direkt ekleyecektir.Çünkü yeni bir ekleme olduğu için zaten veri kaynağında yok.
    //            var addedEntity = context.Entry(entity); //hepsinin özeti bu method referansı yakalama methodudur.
    //            //peki onu nerden belirtiyorum aşağıdaki satırdan. Yani addedEntity nin state(durum) inden. Bu şu demek yukarıdaki kod çalıştı ben
    //            //şimdi veri kaynağımla ilişkilendirdim bunu demek ama ben bunu ne yapayım. aşağıdaki kodda bu ne yapayım durumunu EntityState
    //            //enum ından belirleyip State atamasını yapmış oluruz. Biz burda ekleme yap dedik.
    //            addedEntity.State = EntityState.Added;
    //            //ve en nihayetinde kaydetme işlemi yaparız. SaveChanges() metodu yukarıdaki tüm işlemleri pıt pıt pıt gerçekleştirir.
    //            //Zaten bir tane işlem var Ekleme o işlemi yapar demek.
    //            context.SaveChanges();
    //            //Yukarıdaki üç satırı özetleyecek olursak; Referansı yakala(1. satır), O aslınada eklenecek bir nesne(2.satır),
    //            //ve şimdi ekle(3. satır)
    //            //Başka bir anlatım ifadesiyle context.Entry(entity); NorthwindContext te bağla bu entity i. yani kısacası veri kaynağı ile
    //            //ilişkilendir. EntityState.Added; ekleme olarak durumunu set et. context.SaveChanges(); ve ekle şimdi.
    //        }
    //    }

    //    public void Delete(Product entity)
    //    {
    //        using (NorthwindContext context = new NorthwindContext())
    //        {
    //            var deletedEntity = context.Entry(entity);
    //            deletedEntity.State = EntityState.Deleted;
    //            context.SaveChanges();
    //        }
    //    }

    //    public Product Get(Expression<Func<Product, bool>> filter)
    //    {
    //        using (NorthwindContext context = new NorthwindContext())
    //        {
    //            //context.Set<Product>() bu aslında benim Products tablom. Biz önceden ne yapmıştık InMemory çalıştığımız zaman elimizde bir liste vardı
    //            //ama bu bizim yerimize o tabloyu bir liste gibi ele alıyor ona SingleOrDefault(filter) uyguluyoruz. 
    //            return context.Set<Product>().SingleOrDefault(filter);
    //        }
    //    }

    //    public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
    //    {
    //        //Eğer filter göndermemişse veri kaynağındaki yani veritabanında ilgili tablodaki tüm datayı getir. Ama filtre vermişse o filtreyi uygula
    //        //ona göre datayı listele diyelim.
    //        using (NorthwindContext context = new NorthwindContext())
    //        {
    //            //filter == null mı veya eğer filtre null ise ? den sonraki kısım çalışır yani ilk kısım çalışır. Değilse : dan sonraki yani diğer
    //            //kısım çalışır. Devam edelim. context.Set<Product>() diyoruz. Set<>() generic metodumuzun generic <> işaretinin içine yani tipini
    //            //belli edip yazıyoruz. Yani bir entity yazıyoruz. Toplayacacak olursak benim Product ıma yerleş ve oradaki tüm datayı ToList()
    //            //diyerek listeye çevir. Arkadaşlar bu şu demek DbSet<Product> deki Product için Products tablosu ya oraya diyo yerleş yani ben kısacası
    //            //Products tablosu ile çalışacam demek. Veritabanındaki bütün tabloyu listeye çevir ve onu bana ver. yani kısacası bu arka planda bizim
    //            //için 'Select * From Products' sorgusunu döndürür. Bu sorguyu çalıştırıyor ve onu bizim için bir Listeye çeviriyor.
    //            //Eğer filtre null değilse demekki bu arkadaşımız bir filtre vermiş onu : ile veriyoruz.
    //            //Filtre null mı ? o. Evetse tümünü getir. Değilse Where(filter) ile filtrelemem lazım. İşte diyorunki ben sana bir filtre verecem.
    //            //Hatırlayalım linq teki Where i parametre olarak bir tane lamda yazıyorduk. işte o lamda gibi bir şey yazabilmemizi sağlayan yapı
    //            //Expression<Func<Product, bool>> filter yapı tam olarak burası. 
    //            //Filre null mı evetse ? den sonraki çalışır. Filtre null değil filtre varsa filtreleyip ver. yani : den sonrakiler çalışır.
    //            //İşte burda GetAll() metodundaki parametre olarak göndereceğimiz şey bir lamda. o yüzden sen buraya ne yazarsan yaz o datayı bize 
    //            //getirecektir. Olay bu kadar basit.
    //            return filter == null
    //                ? context.Set<Product>().ToList()
    //                : context.Set<Product>().Where(filter).ToList();
    //        }
    //    }

    //    public void Update(Product entity)
    //    {
    //        using (NorthwindContext context = new NorthwindContext())
    //        {
    //            var updatedEntity = context.Entry(entity);
    //            updatedEntity.State = EntityState.Modified;
    //            context.SaveChanges();
    //        }
    //    }
    //}
}
