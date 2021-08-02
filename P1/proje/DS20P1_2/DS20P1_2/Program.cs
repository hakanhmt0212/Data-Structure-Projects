using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace DSP201_2
{
    class paraVeri
    {
        public double varyans, carpiklik, basiklik, entropi, uzaklik;//Sınıfın değişkenlerini tanımladık.
        public int sinif;
        public paraVeri(double varyans, double carpiklik, double basiklik, double entropi, int sinif, double uzaklik)//Tüm değişkenleri içeren yapıcı metot oluşturduk.
        {
            this.varyans = varyans;
            this.carpiklik = carpiklik;
            this.basiklik = basiklik;
            this.entropi = entropi;
            this.sinif = sinif;
            this.uzaklik = uzaklik;
        }
        public paraVeri()//Parametresiz yapıcı metot oluşturduk.
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            paraVeri[] veriseti1 = verisetiOlusturma("veriseti1.txt", 1372, 5);//verisetiOlusturma metodunu çağırarak txt dosyalarımızı okutuyoruz.
            paraVeri[] veriseti2 = verisetiOlusturma("veriseti2.txt", 200, 5);
            paraVeri[] veriseti3 = verisetiOlusturma("veriseti3.txt", 1172, 5);

            paraVeri girilenPara = paraBilgileri();//paraBilgileri metodunu çağırarak kullanıcıdan paramızın bilgilerini alıyoruz.
            int kDeger = kDegeri();//k değerini kullanıcıdan alan metodumuzu çağırıyoruz.
            knnhesapla(girilenPara, veriseti1, kDeger);//Paramızın uzaklığına en yakın veriseti1 içindeki k tane değer ile kıyaslayarak paramızın türünü bulan metodu çağırıyoruz. 
            tabloyazdir(kDeger, girilenPara, veriseti1);//Paramızın özelliklerini kıyaslandığı paraların özellikleriyle birlikte ekrana tablo halinde yazdıran metodu çağırıyoruz. 
            Console.WriteLine();
            Console.WriteLine("200 verinin karşılaştırılması yapılacak.");
            basariOrani(veriseti2, veriseti3);//veriseti2 içinde bulunan her parayı veriseti3 içindeki en yakın k adet uzaklık ile kıyaslyoruz. Sonrasında her para için kNN'den gelen tür ile bizim paramızın gerçek türüne bakarak başarı oranını hesaplıyoruz.
            Console.WriteLine("Listeleme işlemi için Enter'a basın.");
            Console.ReadLine();
            listeleme(veriseti1);//veriseti1 txt dosyasını ekrana yazdırıyoruz.
            Console.WriteLine("Çıkmak için herhangi bir tuşa basın.");
            Console.ReadLine();

        }
        static int kDegeri()//k değerini kullanıcıdan alan metot.
        {
            int k;

            Console.WriteLine("İstediğiniz K Değerini Giriniz: ");
            k = Convert.ToInt32(Console.ReadLine());
            return k;
        }
        static int knnhesapla(paraVeri para, paraVeri[] veritabani, int k)
        {
            for (int i = 0; i < veritabani.GetLength(0); i++)//veritabanı dosyası içindeki para sayısı kadar dönen döngü açtık.
            {
                double uzaklık = Math.Sqrt(Math.Pow(veritabani[i].varyans - para.varyans, 2) + Math.Pow(veritabani[i].carpiklik - para.carpiklik, 2) + Math.Pow(veritabani[i].basiklik - para.basiklik, 2) + Math.Pow(veritabani[i].entropi - para.entropi, 2));//bizim girdiğimiz paranın verisetindeki paralar arasındaki uzaklığı buluyoruz.
                veritabani[i].uzaklik = uzaklık;//uzaklıkları veritabani dosyasındaki her para nesnesine uzaklık değerlerini ekliyoruz.
            }
            var sonuc = veritabani.OrderBy(x => x.uzaklik);//veritabanı dosyalarımızı sıralıyoruz.
            paraVeri[] yakinlikdizisi = sonuc.ToArray();//Sıralanmış sonuc değerini paraVeri değerlerini içeren diziye çeviriyoruz.
            int sinif0sayaci = 0;
            int sinif1sayaci = 0;
            for (int x = 0; x < k; x++)//k değeri kadar dönen döngü açıyoruz.
            {
                if (yakinlikdizisi[x].sinif == 1)//paranın türü 1 ise sinif1sayaci değerini arttırıyoruz.
                    sinif1sayaci++;
                else//paranın türü 0 ise sinif0sayaci değerini arttırıyoruz.
                    sinif0sayaci++;
            }
            if (sinif0sayaci > sinif1sayaci)//0 türü 1 türünden fazlaysa paramızın türü 0 olarak kabul edilir. 
                return 0;
            else if (sinif0sayaci < sinif1sayaci)//1 türü 0 türünden fazlaysa paramızın türü 1 olarak kabul edilir.
                return 1;
            else
                return yakinlikdizisi[0].sinif;//1 türü ile 0 türü eşitse en yakın uzaklığın türünü alıyoruz.

        }
        static void tabloyazdir(int k, paraVeri para, paraVeri[] veritabani)
        {
            int parasinif = knnhesapla(para, veritabani, k);//Paramızın türünü tutan değişken tanımladık.
            var sonuc = veritabani.OrderBy(x => x.uzaklik);//veritabani dizimizi sıralıyoruz.
            int sayac = 0;
            Console.WriteLine("---------------------------------------------------------------------------------|");
            Console.WriteLine("Varyans    | Çarpıklık  | Basıklık   | Entropi    | Sınıf      | Uzaklık   ");
            Console.WriteLine("---------------------------------------------------------------------------------|");

            foreach (paraVeri x in sonuc)//sonuc içindeki her nesneye foreach döngüsü ile erişiyoruz.
            {
                if (sayac < k)//Uzaklığı en yakın k tane paranın bilgileri yazdırıyoruz.
                {
                    Console.WriteLine(string.Format("{0,-10} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-20}", x.varyans, x.carpiklik, x.basiklik, x.entropi, x.sinif, x.uzaklik));
                    sayac++;
                }
                else//Bizim girdiğimiz paranın özelliklerini yazdırıyoruz. 
                {
                    Console.WriteLine("{0,-10} | {1,-10} | {2,-10} | {3,-10} | {4,-10} | {5,-20}", para.varyans, para.carpiklik, para.basiklik, para.entropi, parasinif, "Bizim Girdiğimiz Para");
                    break;
                }
            }//Paranın türünü yazdırıyoruz.
            if (parasinif == 1)
                Console.WriteLine("PARANIZ GERÇEK.");
            else
                Console.WriteLine("PARANIZ SAHTE.");
            Console.WriteLine();
        }

        static paraVeri paraBilgileri()
        {
            double varyans;
            double basiklik;
            double carpiklik;
            double entropi;

            Console.WriteLine("Paranızın Varyans Değerini Giriniz: ");//Paranın bilgilerini kullanıcıdan alıyoruz.
            varyans = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Paranızın Çarpıklık Değerini Giriniz: ");
            carpiklik = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Paranızın Basıklık Değerini Giriniz: ");
            basiklik = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            Console.WriteLine("Paranızın Entropi Değerini Giriniz: ");
            entropi = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            paraVeri girilenPara = new paraVeri();//Boş paraVeri nesnesi oluşturuyoruz.
            girilenPara.varyans = varyans;//Kullanıcıdan aldığımız para bilgilerini nesnenin içine aktarıyoruz.
            girilenPara.carpiklik = carpiklik;
            girilenPara.basiklik = basiklik;
            girilenPara.entropi = entropi;
            return girilenPara;//paraVeri nesnemizi döndürüyoruz.
        }

        static paraVeri[] verisetiOlusturma(string dosyakonumu, int satir, int sutun)//verilen dosyaların satır sütun sayısını bildiğimiz için direkt olarak parametrede yazabiliriz.
        {

            FileStream fs = new FileStream(dosyakonumu, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);

            string[,] veriSeti = new string[satir, sutun];//verileri diziye aktarmak için çift boyutlu dizi açıyoruz.
            paraVeri[] veriseti = new paraVeri[veriSeti.GetLength(0)];//Oluşturacağımız nesneleri saklamak için satır uzunluğunda paraVeri dizisi açıyoruz.

            int i = 0;
            while (sr.EndOfStream != true)//txt dosyası bitene kadar dönecek döngü açıyoruz.
            {
                string[] bolunmusSatir = sr.ReadLine().Split(',');//değerleri alabilmek için satırları "," ile bölüp diziye atıyoruz.
                for (int j = 0; j < 5; j++)//her satırda almamız gereken 5 değer olduğu için 5 kez dönen for döngüsü açıyoruz.
                {
                    veriSeti[i, j] = bolunmusSatir[j];//değerleri veriSetinde gerekli yerlere koyuyoruz.
                }
                i++;
            }

            for (int j = 0; j < veriSeti.GetLength(0); j++)
            {
                veriseti[j] = new paraVeri();//Boş paraVeri nesnesi oluşturuyoruz ve veriseti[j] içine atıyoruz.
                veriseti[j].varyans = double.Parse(veriSeti[j, 0], CultureInfo.InvariantCulture);//Nesnelerimizin içine okumuş olduğumuz bilgileri double veya int değerlerine çevirerek atıyoruz.
                veriseti[j].carpiklik = double.Parse(veriSeti[j, 1], CultureInfo.InvariantCulture);
                veriseti[j].basiklik = double.Parse(veriSeti[j, 2], CultureInfo.InvariantCulture);
                veriseti[j].entropi = double.Parse(veriSeti[j, 3], CultureInfo.InvariantCulture);
                veriseti[j].sinif = Convert.ToInt32(veriSeti[j, 4]);
            }
            return veriseti;//Nesnelerimizin olduğu verisetini döndürüyoruz.
        }
        static void basariOrani(paraVeri[] para, paraVeri[] veriseti)
        {
            int kDeger2 = kDegeri();//kNN için k değeri alıyoruz.
            double dogruSayisi = 0;//Başarı oranını hesaplayabilmek için başarılı sınıflandırılan banknotlar için ardoğruSayisi değişkeni açıyoruz.
            for (int i = 0; i < para.GetLength(0); i++)
            {

                double tahmin = knnhesapla(para[i], veriseti, kDeger2);
                tabloyazdir(kDeger2, para[i], veriseti);
                if (tahmin == para[i].sinif)
                {
                    dogruSayisi++;
                }
            }
            double basariOrani = (dogruSayisi * 100) / para.GetLength(0);

            Console.WriteLine("Programın Başarı Oranı: %{0}", basariOrani);
        }
        static void listeleme(paraVeri[] veriseti)
        {
            Console.WriteLine("--------------------------------------------------------------------|");
            Console.WriteLine("Varyans     | Çarpıklık   | Basıklık    | Entropi     | Sınıf       |");
            Console.WriteLine("--------------------------------------------------------------------|");
            Console.WriteLine("");
            for (int j = 0; j < veriseti.GetLength(0); j++)//Verilen verisetinin satır sayısı kadar dönen for döngüsü açıyoruz.
            {
                Console.WriteLine(string.Format("{0,-11} | {1,-11} | {2,-11} | {3,-11} | {4,-11} |", veriseti[j].varyans, veriseti[j].carpiklik, veriseti[j].basiklik, veriseti[j].entropi, veriseti[j].sinif));//yazdırıyoruz
                Console.WriteLine("");//Anlaşılır gözükmesi için her satırdan sonra boşluk bırakıyoruz. 
            }
        }
    }
}
