using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context demek veritabanı ile kendi classlarımızı ilişkilendirdiğimiz classın ta kendisi.
    //ORM demek veritabanındaki tabloyu sanki class mış gibi onunla ilişkilendirip bundan böyle bütün operasyonları yani o sql leri bizim linq ile
    //yaptığımız bir ortam.(ORM olarak EntityFramworkCore kullandığımız ve  linq desteklediği için). ORM veritabanı nesneleri ile kodlar arasında bir
    //ilişki kurup bir bağ kurup veritabanı işlerini yapma süreci
    //Context Nesnesi : Db tabloları ile proje classlarını bağlamak
    //EntityframeworkCore kurdukya içinde DbContext adında base bir class vardır.
    //DbContext aslında bizim Contextimizn ta kendisi. Ne dedik db tabloları ile proje classlarını bağlıyoruz. Yani bizim öncelikle şunu
    //söylememiz gerekmez mi benim veritabanım tam olarak şurada demem gerekiyor. 
    public class NorthwindContext : DbContext
    {
        //override yazıp boşluk bırakıp seçip enter dediğimizde aşağıdaki metodu oluşturabiliriz. Bu metot bizim projemiz hangi veritabanı
        //ile ilişkiliyi belirteceğimiz yer.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Burda diyoruzki SqlServer kullanacağız. O zaman SqlServer a nasıl bağlanacağını belirtmem yeterli.
            //UseSqlServer() metodunun parametresine "" içerisinde hangi veritabanına bağlanacağımızı söyleyeceğiz. yani çift tırnak içerisine
            //bir connection string yazacağız. Yalnız çift tırnağın başına bir @ koyalım çünkü ters slaşın normalde bir anlamı var stringin
            //içinde bile yazsak. O yüzden C# da ters slaş kullandığımız yerlerde çift \\ kullanırız. Başına @ koyduğumuzda ters slaşı normal
            //ters slaş olarak algıla demek olur veya buna benzer özel karakterler içinde. "" içerisindeki; 
            //Server=175.45.2.12 SqlServer ımızın nerede olduğu demek. 175.45.2.12 bir ip yani bu SqlServer ın nerde olduğunu anlatır. Gerçek
            //projelerde böyle olur. Ancak bizim SqlServer ımız (localdb)\MSSQLLocalDB tam olarak buradadır. Şimdi yine adım adım gidelim.
            //Bu server da((localdb)\MSSQLLocalDB) hangi veritabanı onu belirtmem lazım. ; bırakıp diyoruzki oradaki database lerden ismi
            //Northwind olan varya ona bağlan diyoruz. Tekrar ; koyarız. Sonra
            //Trusted_Connection=true vererek kullanıcı adı şifre gerektirmeden çünkü sonuçta biz bağlanırken kullanıcı adı ve şifresiz
            //bağlandığımız için. Gerçek sistemlerde eğer güçlü bir domain yönetimi varsa yine böyle kullanırız. Ama öyle olmayan Domain
            //yönetiminin zayıf olduğu ortamlarda kullanıcı adı ve şifre yazılır. Bitti hepsi bu kadar.
            //Bu nedemek biz bu projeyi çalıştırdığımızda hemen buraya bakıyor EntityFrameworkCore diyorki ben nereye bağlanacağım.
            //Bizde ne yaptık onu verdik. Sonra aşağıdaki property i oluştururuz.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
        }

        //Peki tamamda ben veritabanımın ne olduğunu söyledim ama benim hangi nesnem hangi nesneye karşılık gelecek. Bunuda DbSet<> dediğimiz
        //bir nesne ile yapıyoruz. Diyorumki aşağıdaki ilk property deki gibi benim Product nesnemi veritabanındaki Products tablosu ile bağla
        //demek bu. Tekrarlarsak Context; hangi veritabanına bağlanacağını buldu yukarıdaki metotda Property de ise diyorki benim hangi
        //clasım hangi tabloya karşılık geliyor. İlk configürasyonumuz böyle. Sonra bunu profosyonelleştirecez tabiki.
        public DbSet<Product> Products { get; set; }
        //Sonra diyorum bende bir tane de Category var diyorum onuda sana zahmet Categories e bağla.
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //Artık biz EntityFrameworkCore kullanarak Ürünlerle, Categorilerle ve Müşterilerle ilgili kodlarımızı yazabiliriz.
    }
}
