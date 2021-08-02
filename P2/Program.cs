using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Proje2Deneme
{
    class pQueueArtan
    {
        public int maxSize;
        public List<Musteri> pQueArr;
        public int nItems;

        public pQueueArtan(int maxSize)
        {
            this.maxSize = maxSize;
            pQueArr = new List<Musteri>();
            nItems = 0;
        }

        public void Insert(Musteri musteri)
        {
            pQueArr.Add(musteri);
            nItems++;
        }

        public Musteri remove()
        {
            int min = pQueArr.ElementAt(0).siparisSayisi;
            int minIndex = 0;

            for (int i = 1; i < pQueArr.Count; i++)
            {
                if (pQueArr.ElementAt(i).siparisSayisi < min)
                {
                    min = pQueArr.ElementAt(i).siparisSayisi;
                    minIndex = i;
                }
            }
            Musteri temp = pQueArr.ElementAt(minIndex);
            pQueArr.RemoveAt(minIndex);
            return temp;
        }

        public bool isEmpty()
        {
            return (nItems == 0);
        }

    }
    class priorityQueue
    {
        public int maxSize;
        public List<Musteri> pQueArr;
        public int nItems;

        public priorityQueue(int maxSize)
        {
            this.maxSize = maxSize;
            pQueArr = new List<Musteri>();
            nItems = 0;
        }

        public void Insert(Musteri musteri)
        {
            pQueArr.Add(musteri);
            nItems++;
        }

        public Musteri remove()
        {
            int max = pQueArr.ElementAt(0).siparisSayisi;
            int maxIndex = 0;

            for (int i = 0; i < pQueArr.Count; i++)
            {
                if (pQueArr.ElementAt(i).siparisSayisi > max)
                {
                    max = pQueArr.ElementAt(i).siparisSayisi;
                    maxIndex = i;
                }
            }
            Musteri temp = pQueArr.ElementAt(maxIndex);
            pQueArr.RemoveAt(maxIndex);
            return temp;
        }

        public bool isEmpty()
        {
            return (nItems == 0);
        }

    }
    class Queue
    {
        public int maxSize;
        public Musteri[] queArr;
        public int front;
        public int rear;
        public int nItems;

        public Queue(int maxSize)
        {
            this.maxSize = maxSize;
            queArr = new Musteri[maxSize];
            front = 0;
            rear = -1;
            nItems = 0;
        }

        public void Insert(Musteri musteri)
        {
            if (rear == maxSize - 1)
            {
                rear = -1;
            }
            queArr[++rear] = musteri;
            nItems++;
        }

        public Musteri remove()
        {
            Musteri temp = queArr[front++];
            if (front == maxSize)
                front = 0;
            nItems--;
            return temp;
        }

        public Musteri peekFront()
        {
            return queArr[front];
        }

        public bool isEmpty()
        {
            return (nItems == 0);
        }

        public bool isFull()
        {
            return (nItems == maxSize);
        }

        public int size()
        {
            return nItems;
        }
    }
    class StackClass
    {
        public int maxSize;
        public int top;
        public Musteri[] stackArray;

        public StackClass(int maxSize)
        {
            this.maxSize = maxSize;
            stackArray = new Musteri[maxSize];
            top = -1;
        }

        public void push(Musteri musteri)
        {
            stackArray[++top] = musteri;
        }

        public Musteri pop()
        {
            return stackArray[top--];
        }

        public Musteri peek()
        {
            return stackArray[top];
        }

        public bool isEmpty()
        {
            return (top == -1);
        }

        public bool isFull()
        {
            return (top == maxSize - 1);
        }
    }
    class Musteri 
    {
        public string musteriAdi;
        public int siparisSayisi;

        public Musteri(string musteriAdi, int siparisSayisi)
        {
            this.musteriAdi = musteriAdi;
            this.siparisSayisi = siparisSayisi;
        }

        public Musteri()
        {

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] MusteriAdi = { "Ali", "Merve", "Veli", "Gülay", "Okan", "Zekiye", "Kemal", "Banu", "İlker", "Songül", "Nuri", "Deniz" };
            int[] ÜrünSayısı = { 8, 11, 16, 5, 15, 14, 19, 3, 18, 17, 13, 15 };

            ConsoleRenk(); //Konsolun rengini değiştiren metot.
            yazdirma(listGenerator(MusteriAdi, ÜrünSayısı)); //Oluşturduğumuz ArrayListi projede istenildiği şekilde yazdırdım.
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Stack");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            stackYazdirma(MusteriAdi, ÜrünSayısı); //Oluşturduğumuz Stack'i projede istenildiği şekilde yazdırdım
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Queue & Ortalama İşlem Tamamlanma Süresi");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            ortalamaMusteriQueue(queueYazdirma(MusteriAdi, ÜrünSayısı)); //Oluşturduğumuz Queue'yu projede istenildiği şekilde yazdırdım
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Priority Queue & Ortalama İşlem Tamamlanma Süresi");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            ortalamaMusteripQueue(pQueueYazdirma(MusteriAdi, ÜrünSayısı)); //Oluşturduğumuz Priority Queue'yu projede istenildiği şekilde yazdırdım
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Güncellenmiş Priority Queue");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            pQueueArtanYazdirma(MusteriAdi, ÜrünSayısı); //Güncellediğimiz Priority Queue'yu projede istenildiği şekilde yazdırdım
            Console.ReadKey();

        }
        public static ArrayList listGenerator(string[] dizi, int[] dizi2)
        {
            ArrayList arrlist = new ArrayList(); //Müşteri verilerini tutacağım bir arraylist oluşturdum.
            int musteriSayaci = 0; //Müşteri sayısı
            Musteri musteri;
            int arrlistSayisi = 0; //ArrayList eleman sayısı.

            List<Musteri> genericlist; //ArrayListin içinde tutacağım verileri genericlist halinde tutmak için bir eleman oluşturdum.
            while (musteriSayaci < dizi.Length)
            {
                genericlist = new List<Musteri>();
                Random random = new Random(); 
                int listLength = random.Next(1, 6);
                

                for (int i = 0; i < listLength; i++) //Genericlist in içine kaç eleman gireceğini belirleyip for döngüsü döndürttüm.
                {
                    musteri = new Musteri(dizi[musteriSayaci], dizi2[musteriSayaci]); //Diziden aldığım veriler ile Müşteri tipinde objeler oluşturdum. 
                    genericlist.Add(musteri); //Bunları genericliste ekledim
                    musteriSayaci++;

                    if (musteriSayaci == dizi.Length)
                        break;
                }
                arrlist.Add(genericlist); //oluşturduğum genericListleri arrayliste ekledim.
                arrlistSayisi++;
            }
            Console.WriteLine("ArrayList sayısı: " +Convert.ToString(arrlistSayisi));
            Console.WriteLine("Ortalama eleman sayısı: " +Convert.ToString(dizi.Length/arrlistSayisi));
            Console.WriteLine();
            return arrlist;
        } 
        static void yazdirma(ArrayList list)//ArrayListin içindeki verileri yazdırma metodu.
        {
            foreach (List<Musteri> item in list)
            {
                foreach (Musteri item1 in item) 
                {
                    Console.WriteLine("Müsteri adı: " + item1.musteriAdi + ", Sipariş sayısı: " + item1.siparisSayisi);
                }
                Console.WriteLine();
            }
        } 
        static void stackYazdirma(string[] MusteriAdi, int[] ÜrünSayısı)
        {
            Musteri[] musteriList = new Musteri[MusteriAdi.Length]; //Müşteri tipinde bir dizi oluşturdum.

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], ÜrünSayısı[i]);
                musteriList[i] = musteri; //Bu diziye bize verilen verileri ekledim.

            }

            StackClass stack = new StackClass(musteriList.Length); //Bİr stack oluşturdum.
            for (int i = 0; i < musteriList.Length; i++)
            {
                stack.push(musteriList[i]); //Dizideki elemanları stacke ekledim.
            }

            while (!stack.isEmpty())
            {
                Musteri musteri = stack.pop(); //Stackden çıkardığım elemanı müşteri adında ve müşteri tipinde bir objeye atadım.
                Console.WriteLine(musteri.musteriAdi + " " + musteri.siparisSayisi); //musteri objesinin bilgilerini yazdırdım.
            }
            Console.WriteLine();
        } 
        static Queue queueYazdirma(string[] MusteriAdi, int[] ÜrünSayısı)
        {
            Musteri[] musteriList = new Musteri[MusteriAdi.Length]; //Müşteri tipinde bir dizi oluşturdum.

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], ÜrünSayısı[i]);
                musteriList[i] = musteri;//Bu diziye bize verilen verileri ekledim.

            }

            Queue queue = new Queue(musteriList.Length); //Bir queue oluşturdum.
            for (int i = 0; i < musteriList.Length; i++)
            {
                queue.Insert(musteriList[i]); //Dizideki elemanları queueya ekledim.
            }
            Queue temp = new Queue(queue.maxSize); //Yedek bir queue oluşturdum.

            while (!queue.isEmpty())
            {
                Musteri musteri = queue.remove(); //Queuedan çıkardığım elemanı müşteri adında ve müşteri tipinde bir objeye atadım.
                temp.Insert(musteri); //Oluşturduğum kopya queuenin içine çıkardığım objeyi atadım.
                Console.WriteLine(musteri.musteriAdi + " " + musteri.siparisSayisi); //musteri objesinin bilgilerini yazdırdım.
            }
            Console.WriteLine();
            return temp;
        } 
        static priorityQueue pQueueYazdirma(string[] MusteriAdi, int[] ÜrünSayısı)
        {
            Musteri[] musteriList = new Musteri[MusteriAdi.Length]; //Müşteri tipinde bir dizi oluşturdum.

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], ÜrünSayısı[i]);
                musteriList[i] = musteri; //Bu diziye bize verilen verileri ekledim.

            }
            priorityQueue pQueue = new priorityQueue(musteriList.Length); //Bir priority queue oluşturdum.
            for (int i = 0; i < musteriList.Length; i++)
            {
                pQueue.Insert(musteriList[i]); //Dizideki elemanları priority queueya ekledim.
            }

            priorityQueue temp = new priorityQueue(pQueue.maxSize); //Yedek oluşturdum.

            for (int i = 0; i < pQueue.maxSize; i++)
            {
                Musteri musteri = pQueue.remove(); //Priority queuedan çıkardığım elemanı müşteri adında ve müşteri tipinde bir objeye atadım.
                temp.Insert(musteri); //Oluşturduğum kopya priority queuenin içine çıkardığım objeyi atadım.
                Console.WriteLine(musteri.musteriAdi + " " + musteri.siparisSayisi); //musteri objesinin bilgilerini yazdırdım.
            }
            Console.WriteLine();
            return temp;
        } 
        static void pQueueArtanYazdirma(string[] MusteriAdi, int[] ÜrünSayısı) //Güncellenmiş priority queueyi yazdıran metot.
        {
            Musteri[] musteriList = new Musteri[MusteriAdi.Length];

            for (int i = 0; i < MusteriAdi.Length; i++)
            {
                Musteri musteri = new Musteri(MusteriAdi[i], ÜrünSayısı[i]);
                musteriList[i] = musteri;
            }
            pQueueArtan pQueue = new pQueueArtan(musteriList.Length);
            for (int i = 0; i < musteriList.Length; i++)
            {
                pQueue.Insert(musteriList[i]);
            }

            for (int i = 0; i < pQueue.maxSize; i++)
            {
                Musteri musteri = pQueue.remove();
                Console.WriteLine(musteri.musteriAdi + " " + musteri.siparisSayisi); 
            }
            Console.WriteLine();
            
        } 
        static void ortalamaMusteriQueue(Queue queue) //Queue için ortalama işlem süresini hesaplayan methot.
        {
            int sonuc = 0;
            int musteriSayisi = queue.maxSize;
            for (int i = 1; i < queue.maxSize+1; i++)
            {
                Musteri musteri = queue.remove();
                for (int j = 0; j < i; j++)
                {
                    sonuc += musteri.siparisSayisi;
                }
            }
            sonuc = sonuc / musteriSayisi;
            Console.WriteLine("Ortalama işlem tamamlanma süresi: " + sonuc);
        }
        static void ortalamaMusteripQueue(priorityQueue pQueue) //Priority Queue için ortalama işlem süresini hesaplayan methot.
        {
            int sonuc = 0;
            int musteriSayisi = pQueue.maxSize;
            for (int i = 1; i < pQueue.maxSize + 1; i++)
            {
                Musteri musteri = pQueue.remove();
                for (int j = 0; j < i; j++)
                {
                    sonuc += musteri.siparisSayisi;
                }
            }
            sonuc = sonuc / musteriSayisi;
            Console.WriteLine("Ortalama işlem tamamlanma süresi: "+sonuc);
        }
        static void ConsoleRenk()//Konsolun rengini değiştimek için kullandığım metot.
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
        } 
    }
}