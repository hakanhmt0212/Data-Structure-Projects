using System;
using System.Collections;
using System.Collections.Generic;

namespace Deneme3
{
    class ShellSort
    {
        private long[] arr;
        private int nElems;

        public ShellSort(int max)
        {
            arr = new long[max];
            nElems = 0;
        }

        public void insert(long value)
        {
            arr[nElems] = value;
            nElems++;
        }

        public void display()
        {
            for (int i = 0; i < nElems; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }

        public void shellsort()
        {
            int inner, outer;
            long temp;

            int h = 1;

            while (h <= nElems / 3)
            {
                h = h * 3 + 1;
            }

            while (h > 3)
            {
                for (outer = h; outer < nElems; outer++)
                {
                    temp = arr[outer];
                    inner = outer;

                    while (inner > h - 1 && arr[inner - h] >= temp)
                    {
                        arr[inner] = arr[inner - h];
                        inner -= h;
                    }
                    arr[inner] = temp;
                }
                h = (h - 1) / 3;
            }
        }
    }
    class SelectionSort
    {
        private long[] arr;
        private int nElems;

        public SelectionSort(int max)
        {
            arr = new long[max];
            nElems = 0;
        }

        public void inset(long value)
        {
            arr[nElems] = value;
            nElems++;
        }

        public void display()
        {
            for (int i = 0; i < nElems; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }

        public void selectionsort()
        {
            int a, b, c;
            for (a = 0; a < nElems-1 ; a++)
            {
                c = a;
                for (b = a + 1; b < nElems; b++)
                    if (arr[b] < arr[c])
                    {
                        c = b;
                    }
                swap(a, c);
            }
        }
        public void swap(int a, int b)
        {
            long temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
    class Musteri
    {
        public string ID;
        public TimeSpan saat;
        public Musteri()
        {
            Random random = new Random();
            ID = random.Next(1, 20).ToString();
            saat = new TimeSpan(0, 0, 0, random.Next(86400));
        }
    }
    class DurakNesnesi
    {
        public string durakAdi;
        public int BP;
        public int TB;
        public int NB;
        public List<Musteri> list;

        public DurakNesnesi(string[] arr)
        {
            durakAdi = arr[0];
            BP = Convert.ToInt32(arr[1]);
            TB = Convert.ToInt32(arr[2]);
            NB = Convert.ToInt32(arr[3]);
            list = new List<Musteri>();
        }
    }
    class DurakNode
    {
        public DurakNesnesi data;
        public DurakNode leftChild;
        public DurakNode rightChild;
        public void displayNode()
        {
            Console.WriteLine("Duragin Adi: " + data.durakAdi);
            Console.WriteLine("Bos Park Sayisi: " + data.BP);
            Console.WriteLine("Tandem Bisiklet Sayisi: " + data.TB);
            Console.WriteLine("Normal Bisiklet Sayisi: " + data.NB);
            foreach (Musteri musteri in data.list)
            {
                Console.WriteLine("ID: " + musteri.ID);
                Console.WriteLine("Kiralama Saati: " + musteri.saat);
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }
    class DurakAgaci
    {
        private DurakNode root;

        public DurakAgaci()
        {
            root = null;
        }

        public DurakNode getRoot()
        {
            return root;
        }
        public void preOrder(DurakNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.displayNode();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }
        public void inOrder(DurakNode localRoot)
        {
            if (localRoot != null)
            {
                inOrder(localRoot.leftChild);
                localRoot.displayNode();
                inOrder(localRoot.rightChild);
            }
        }
        public void postOrder(DurakNode localRoot)
        {
            if (localRoot != null)
            {
                postOrder(localRoot.leftChild);
                postOrder(localRoot.rightChild);
                localRoot.displayNode();
            }
        }

        public DurakNesnesi preOrderID(DurakNode durakNode)
        {
            if (durakNode != null)
            {
                DurakNesnesi durakNesnesi = IDileBulma(durakNode);
                if (durakNesnesi != null)
                {
                    return durakNesnesi;
                }
                preOrderID(durakNode.leftChild);
                preOrderID(durakNode.rightChild);
                return null;
            }
            return null;

        }
        public int depth(DurakNode root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                int lDepth = depth(root.leftChild);
                int rDepth = depth(root.rightChild);
                if (lDepth > rDepth)
                    return (lDepth + 1);
                else
                    return (rDepth + 1);
            }
        }

        public DurakNesnesi IDileBulma(DurakNode root)
        {
            Console.WriteLine("İstediğiniz ID'yi giriniz(1-20): ");
            string girilenID = Console.ReadLine();

            DurakNode current = root;

            foreach (Musteri musteri in current.data.list)
            {
                if (girilenID == musteri.ID)
                {
                    return current.data;
                }
            }
            return null;
        }

        public void insert(DurakNesnesi newData)
        {
            DurakNode newNode = new DurakNode();
            newNode.data = newData;

            if (root == null)
            {
                root = newNode;
            }
            else
            {
                DurakNode current = root;
                DurakNode parent;
                while (true)
                {
                    parent = current;
                    if (String.Compare(newData.durakAdi, current.data.durakAdi) == -1)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            return;

                        }
                    }
                }
            }
        }
    }
    class Program
    {
        public static Hashtable hashtableOlusturucu(string[] arr)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < arr.Length; i++)
            {
                string[] list = arr[i].Split(',');
                string bilgiler = "Boş Park Sayısı: " + list[1] + "," + "Tandem Bisiklet Sayısı :" + list[2] + "," + "Normal Bisiklet Sayısı" + list[3];
                hashtable[list[0]] = bilgiler;
            }
            return hashtable;
        }

        public static Hashtable hashtableGuncelleme(string[] arr)
        {
            Hashtable hashtable = new Hashtable();
            for (int i = 0; i < arr.Length; i++)
            {
                string[] list = arr[i].Split(',');
                int BP = Convert.ToInt32(list[1]);
                int NB = Convert.ToInt32(list[3]);

                if (BP > 5)
                {
                    NB += 5;
                }

                list[3] = NB.ToString();
                string bilgiler = "Boş Park Sayısı: " + list[1] + "," + "Tandem Bisiklet Sayısı :" + list[2] + "," + "Normal Bisiklet Sayısı" + list[3];
                hashtable[list[0]] = bilgiler;
            }
            return hashtable;
        }
        static void Main(string[] args)
        {
            string[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 7, 0, 5", "Hasan Ağa Parkı, 5, 1, 8", "Karşıyaka Evlendirme Dairesi, 8, 1, 3", "Bayraklı, 10, 0, 5", "Buz Pisti, 11, 0, 9", "Bornova Metro, 5, 0, 7" };
            DurakAgaci durakAgaci = new DurakAgaci();
            int musteriSayisi = 20;
            Random random = new Random();

            for (int i = 0; i < duraklar.Length; i++)
            {
                string[] list = duraklar[i].Split(',');
                DurakNesnesi durakNesnesi = new DurakNesnesi(list);
                int durakMusteriSayisi = 0;
                
                if (musteriSayisi > 0)
                {
                    if (musteriSayisi > 10)
                    {
                        int duraktakiMusteri = random.Next(0, 11);
                        musteriSayisi -= duraktakiMusteri;
                        for (int j = 0; j < duraktakiMusteri; j++)
                        {
                            Musteri musteri = new Musteri();
                            durakNesnesi.list.Add(musteri);
                        }
                    }
                    else
                    {
                        int duraktakiMusteri = random.Next(0, musteriSayisi);
                        musteriSayisi -= duraktakiMusteri;
                        for (int j = 0; j < duraktakiMusteri; j++)
                        {
                            Musteri musteri = new Musteri();
                            durakNesnesi.list.Add(musteri);
                        }
                    }
                }
                musteriSayisi -= durakMusteriSayisi;
                durakAgaci.insert(durakNesnesi);
            }
            Console.WriteLine("----------AGAC BİLGİLERİ----------");
            durakAgaci.preOrder(durakAgaci.getRoot());
            int derinlik = durakAgaci.depth(durakAgaci.getRoot());
            Console.WriteLine("Derinlik: " + derinlik);
            Console.WriteLine("----------ID İLE MÜŞTERİ BULMA----------");
            DurakNesnesi data = durakAgaci.preOrderID(durakAgaci.getRoot());
            Console.WriteLine("----------HASHTABLE OLUŞTURMA----------");
            Hashtable hashtable = new Hashtable();
            hashtable =hashtableOlusturucu(duraklar);
            foreach (DictionaryEntry entry in hashtable)
            {
                Console.WriteLine(entry.Key.ToString() + " " + entry.Value);
            }
            Console.WriteLine("----------GÜNCEL HASHTABLE----------");
            Hashtable güncelHash = new Hashtable();
            güncelHash = hashtableGuncelleme(duraklar);
            foreach (DictionaryEntry entry in güncelHash)
            {
                Console.WriteLine(entry.Key.ToString() + " " + entry.Value);
            }
        }
    }
}