using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS20P1_1
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            int widht1 = 100;
            int height1 = 100;
            int n1 = 10;

            double[,] matrix1 = matrixolustur(widht1, height1, n1);//n=10 height=100 widht=100 değerleriyle n x 2'lik matrix1 oluşturduk. 
            matrixolusturYazdirma(matrix1);//matrix1 dizisindeki random üretilen noktalarımızı ekrana yazdırıyoruz.
            n1 = 100;
            double[,] matrix2 = matrixolustur(widht1, height1, n1);//n=100 height=100 widht=100 değerleriyle n x 2'lik matrix2 oluşturduk.
            matrixolusturYazdirma(matrix2);//matrix1 dizisindeki random üretilen noktalarımızı ekrana yazdırıyoruz.
            double[,] matrixDm1 = MatrixDm(matrix1);//n=10 height=100 widht=100 değerleriyle oluşturulmuş n x 2'lik matrix1'i n x n'lik uzaklık matrisine çeviren metodu çağırdık.
            matrixDmyazdirtablo(matrixDm1);//n x n'lik matrixDm1 dizisini ekrana yazdırıyoruz.
            Console.ReadLine();
        }
        static double[,] matrixolustur(int widht, int height, int noktasayisi)
        {
            double[,] matrix = new double[noktasayisi, 2];
            double nokta;
            for (int i = 0; i < noktasayisi; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        nokta = random.NextDouble() * height;//0 ile height arasında bize rasgele x koordinatı verir
                        nokta = Math.Round(nokta, 1);//Random sayının virgülden sonra 1 basamağını alırız.
                        matrix[i, j] = nokta;//Random sayımızı matrisimizin içindeki gerekli yere atarız.
                    }
                    else
                    {
                        nokta = random.NextDouble() * widht;//0 ile widht arasında bize rasgele y koordinatı verir
                        nokta = Math.Round(nokta, 1);//Random sayının virgülden sonra 1 basamağını alırız.
                        matrix[i, j] = nokta;//Random sayımızı matrisimizin içindeki gerekli yere atarız.
                    }
                }
            }
            return matrix;//matrisimizi döndürüyoruz.
        }

        static void matrixolusturYazdirma(double[,] matrix1)
        {
            int sayi = 1;
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                Console.WriteLine("{0}. noktanın x kordinatı: {1} {0}. noktanın y kordinatı: {2}", sayi, matrix1[i, 0], matrix1[i, 1]);//n x 2'lik matrisimizdeki her random sayının x ve y koordinatlarını yazıyoruz.
                Console.WriteLine();
                sayi++;
            }
            Console.WriteLine("---------------------------------------------------------------");
        }

        static double[,] MatrixDm(double[,] matrix)
        {
            double[,] matrixDm = new double[matrix.GetLength(0), matrix.GetLength(0)];//n x n'lik matris oluşturuyoruz.
            for (int i = 0; i < matrixDm.GetLength(0); i++)
            {
                for (int j = 0; j < matrixDm.GetLength(0); j++)
                {
                    matrixDm[i, j] = Math.Round(Math.Sqrt(Math.Pow(matrix[i, 0] - matrix[j, 0], 2) + Math.Pow(matrix[i, 1] - matrix[j, 1], 2)), 1);//Her noktanın arasındaki uzaklığı bulup matrisin içinde olması gereken yere atıyoruz.
                }
            }
            return matrixDm;//matrixDm'yi döndürüyoruz.
        }
        static void matrixDmyazdirtablo(double[,] matrix2)//n>23 olursa konsol ekranında yer kalmayacağı için düzgün tablo düzgün olmayacaktır.
        {
            double[,] matrix = MatrixDm(matrix2);
            Console.Write("      |");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("{0,-5} |", i + 1 + ".");//en üstte matrisin kaçıncı nokta olduğunu göstermek için yazıyoruz.
            }
            Console.WriteLine("");
            Console.Write("------|");//tablonun hizalanması için yazdırdık.
            for (int i = 0; i < matrix.GetLength(0) * 7 - 1; i++)
            {
                Console.Write("-");//tablonun hizalanması için yazdırdık.
            }
            Console.WriteLine("|");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write("{0,-5} |", i + 1 + ".");//En sol tarafta aşağı doğru kaçıncı sayıda olduğumuzu göstermek amaçlı yazdırıyoruz.
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    String virgulludeger = String.Format("{0:0.0}", matrix[i, j]);//0 53 gibi int değerleri de 0.0 53.0 gibi yazmak için formatlıyoruz.
                    Console.Write("{0,-5} |", virgulludeger);//Değerlerimizi ekrana yazdırıyoruz.
                }
                Console.WriteLine("");//Aşağaı satıra geçmek için yazıyoruz.

            }

        }
        
    }
}