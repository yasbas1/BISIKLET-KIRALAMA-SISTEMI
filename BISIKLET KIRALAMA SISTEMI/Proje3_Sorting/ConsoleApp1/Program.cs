using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Bubble
    {
        public int[] a;
        private int elemansayısı;

        public Bubble(int max) // constructor method
        {
            a = new int[max];
            elemansayısı = 0;
        }

        public void ekleme(int value) //arraye eleman ekleme
        {
            a[elemansayısı] = value;
            elemansayısı++;
        }

        public void yazdırma()
        {
            for (int j = 0; j < elemansayısı; j++)
            {
                Console.Write(a[j] + "  ");
            }

        }
        public void BubbleSort()
        {
            int sonraki;
            int önceki;
            for (sonraki = elemansayısı - 1; sonraki > 1; sonraki--) //bütün elemanları gözden geçirene kadar devam ediyor
            {
                for (önceki = 0; önceki < sonraki; önceki++) //ilk elemandan başlıyor
                {
                    if (a[önceki] > a[önceki + 1]) //önceki eleman kendinden bir sonrakinden büyükse yer değiştiriyor
                    {
                        swap(önceki, önceki + 1);
                    }
                }
            }

        }
        private void swap(int birinci, int ikinci) //yer değiştirme methodu
        {
            int değişken = a[birinci];
            a[birinci] = a[ikinci];
            a[ikinci] = değişken;
        }
        

                
    }
    class Shell
    {
        private int[] liste;
        private int elemansayisi;
        public Shell(int max)
        {
            liste = new int[max];
            elemansayisi = 0;
        }
        public void ekleme2(int değer)
        {
            liste[elemansayisi] = değer;
            elemansayisi++;
        }
        public void yazdırma2()
        {
            for (int j = 0; j < elemansayisi; j++)
            {
                Console.Write(liste[j] + "  ");
            }
        }
        public void ShellSort()
        {
            int inner;
            int outer;
            int değişken;
            int h = 1; // find initial value of h
            while (h <= elemansayisi / 3)
            {
                h = h * 3 + 1; // (1, 4, 13, 40, 121, ...)

            }

            while (h > 0) // decreasing h, until h=1
            {
                // h-sort the file
                for (outer = h; outer < elemansayisi; outer++)
                {
                    değişken = liste[outer];
                    inner = outer;
                    while (inner > h - 1 && liste[inner - h] >= değişken)
                    {
                        liste[inner] = liste[inner - h];
                        inner -= h;
                    }
                    liste[inner] = değişken;
                }
                h = (h - 1) / 3;
            }
        }



    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------------BUBBLE SORTING-------------");
            int maxsize = 100;
            Bubble array = new Bubble(maxsize);
            array.ekleme(75);
            array.ekleme(95);
            array.ekleme(46);
            array.ekleme(58);
            array.ekleme(23);
            array.ekleme(81);
            array.ekleme(19);
            array.ekleme(0);
            array.ekleme(61);
            array.ekleme(37);
            Console.WriteLine("Sıralamadan Önce:");
            array.yazdırma();
            Console.WriteLine();
            array.BubbleSort();
            Console.WriteLine("Sıralamadan Sonra:");
            array.yazdırma();
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");


            Console.WriteLine("--------------------SHELLSORT----------------");
            int maxsize2 = 100;
            Shell liste = new Shell(maxsize2);
            liste.ekleme2(58);
            liste.ekleme2(98);
            liste.ekleme2(75);
            liste.ekleme2(88);
            liste.ekleme2(44);
            liste.ekleme2(31);
            liste.ekleme2(13);
            liste.ekleme2(63);
            liste.ekleme2(23);
            liste.ekleme2(3);
            Console.WriteLine("Sıralamadan Önce:");
            liste.yazdırma2();
            Console.WriteLine();
            Console.WriteLine("Sıralamadan Sonra:");
            liste.ShellSort();
            liste.yazdırma2();
            Console.ReadKey();
            


        }





    }
}
