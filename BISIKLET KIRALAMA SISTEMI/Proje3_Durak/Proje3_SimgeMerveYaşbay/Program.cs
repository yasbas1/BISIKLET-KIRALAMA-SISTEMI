using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Proje3_SimgeMerveYaşbay
{
    public class DurakNesnesi
    {
        public string durakadı;
        public int boşpark;
        public int tandembisiklet;
        public int normalbisiklet;
        public List<MusteriNesnesi> musteriler;
        
    }
    public class MusteriNesnesi
    {
        public int ID;
        public string kiralamasaati;
    }
    // Düğüm Sınıfı
    class TreeNode
    {
        public DurakNesnesi data;
        public TreeNode leftChild;
        public TreeNode rightChild;
        public void displayNode() {
            Console.WriteLine();
            Console.WriteLine("DURAK İSMİ : " + data.durakadı +"   BoşPark:"+data.boşpark+"   TandemBisiklet:"+data.tandembisiklet+"    NormalBisiklet:"+data.normalbisiklet);
            Console.WriteLine("MÜŞTERİLER:");
            foreach (MusteriNesnesi item in data.musteriler)
            {
                Console.WriteLine("Müşteri ID: "+item.ID + "\t Kiralama Saati:    "+item.kiralamasaati);

            }
        }
    }

    // Agaç Sınıfı
    class Tree
    {
        private TreeNode root;
        public int MaxDepth = 0;
        public int totalDepth;
        public int leavesDepthTotal;


        public Tree() { root = null; }

        public TreeNode getRoot()
        { return root; }

        // Agacın preOrder Dolasılması
        public void preOrder(TreeNode localRoot)
        {
            if (localRoot != null)
            {
                localRoot.displayNode();
                preOrder(localRoot.leftChild);
                preOrder(localRoot.rightChild);
            }
        }
        public void preOrderileIDarama(TreeNode localroot,int arananId)
        {
            if (localroot!=null)
            {
                foreach (MusteriNesnesi item1 in localroot.data.musteriler)
                {
                    if (arananId == item1.ID)
                    {
                        Console.WriteLine("Aranan ID : " + arananId + "  Kiralanan Durak:  " + localroot.data.durakadı +
                            "\tKiralama Saati: " + item1.kiralamasaati);
                    }


                }
                preOrderileIDarama(localroot.leftChild,arananId);

                preOrderileIDarama(localroot.rightChild,arananId);
                
            }
        }
      
        // Agaca bir dügüm eklemeyi saglayan metot
        public void insert(DurakNesnesi newdata)
        {
            TreeNode newNode = new TreeNode();
            newNode.data = newdata;
            if (root == null)
                root = newNode;
            else
            {
                TreeNode current = root;
                TreeNode parent;
                while (true)
                {
                    parent = current;
                    int donusdegeri = String.Compare(current.data.durakadı, newdata.durakadı);
                    if (donusdegeri==1)
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
        
        private void traverseTreeForInfo(TreeNode node, int depth)
        {
            if (node != null)
            {
                depth++;

                

                if (depth > MaxDepth)
                    MaxDepth = depth; //update max depth

                totalDepth += depth;

                //for calculating the average leaves depth
                if (node.leftChild == null && node.rightChild == null)
                {
                    leavesDepthTotal += depth;
                }

                traverseTreeForInfo(node.leftChild, depth); //traverse left sub-tree
                traverseTreeForInfo(node.rightChild, depth); //traverse right sub-tree

            }
        }

        public void findAndWriteTreeInfo(TreeNode rootNode, int treeSize)
        {

            totalDepth = 0;
            MaxDepth = 0;

            
            //For average leaves depth
            leavesDepthTotal = 0;
            

            traverseTreeForInfo(rootNode, -1);

            Console.WriteLine("Ağacın derinliği : " + MaxDepth);
            
        }


    } 
    class HeapNode
    {
        private DurakNesnesi data;
        public HeapNode(DurakNesnesi key)
        {
            data = key;
        }
        public DurakNesnesi getKey()
        {
            return data;
        }
        public void setKey(DurakNesnesi id)
        {
            data = id;
        }
    }
    class Heap
    {
        public List<HeapNode> heapList;
        public int maxSize;
        public int currentSize;
        public Heap(int mx)
        {
            maxSize = mx;
            currentSize = 0;
            heapList = new List<HeapNode>(maxSize); //creating List
        }
        public Boolean BoşMu()
        {
            return currentSize == 0;
        }
        public Boolean ekleme(DurakNesnesi key)
        {
            if (currentSize == maxSize)
            {
                return false;
            }
            HeapNode newNode = new HeapNode(key);
            heapList.Add(newNode);
            trickleUp(currentSize++);
            return true;

        }
        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            HeapNode bottom = heapList[index];
            while (index > 0 && heapList[parent].getKey().normalbisiklet < bottom.getKey().normalbisiklet)
            {
                heapList[index] = heapList[parent]; // aşağı indir
                index = parent;
                parent = (parent - 1) / 2;
            } 
            heapList[index] = bottom;
        }
        public void trickleDown(int index)
        {
            int largerChild;
            HeapNode top = heapList[index]; // kökü kaydet
            while (index < currentSize / 2)
            { 
                int leftChild = 2*index + 1;
                int rightChild = leftChild + 1;
                // daha büyük child araması
                if (rightChild < currentSize && heapList[leftChild].getKey().normalbisiklet < heapList[rightChild].getKey().normalbisiklet)
                {
                    largerChild = rightChild;

                }

                else
                {
                    largerChild = leftChild;
                }

                if (top.getKey().normalbisiklet >= heapList[largerChild].getKey().normalbisiklet)
                {
                    break;
                }

                heapList[index] = heapList[largerChild];
                index = largerChild; // daha büyük child'ın indexini tutma
            } 
            heapList[index] = top; // o indexdekini kök yapma
        }
        public HeapNode silme()
        {
            HeapNode root = heapList[0];
            heapList[0] = heapList[--currentSize];
            trickleDown(0);
            return root;
        }
        public void HeapYazdırma()
        {
            Console.WriteLine("-------------------------------------- Heap Sıralaması ------------------------------------------------");
            for(int m = 0; m < currentSize; m++)
            {
                if (heapList[m] != null)
                {
                    Console.WriteLine("Durak İsmi : "+heapList[m].getKey().durakadı+" \tBoş Park : "+ heapList[m].getKey().boşpark + " \tTandem Bisiklet: " + heapList[m].getKey().tandembisiklet + " \tNormal Bisiklet: " + heapList[m].getKey().normalbisiklet);
                }
            }
        }
            
        public void insertAt(int index, HeapNode newNode)
        { 
            heapList[index] = newNode;
            
        }
       
    }
    class Program
    {

        static void Main(string[] args)
        {
            String[] duraklar = { "İnciraltı, 28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 7, 0, 5", "Karşıyaka,10,1,9", "Mavişehir,30,2,20", "Çiğli,7,1,5", "Bayraklı,10,0,20", "Bornova,20,2,15" };
            Random rnd = new Random();
            DurakNesnesi NesneOlusturucu(String[] duraklistesi, int z) //Bilgileri ayırıp her bir durak nesnesi oluşturan method
            {

                List<MusteriNesnesi> müsterilistesi = new List<MusteriNesnesi>();
                DurakNesnesi durak = new DurakNesnesi();

                char[] ayrac = { ',', ',', ',' };
                string[] a = duraklar[z].Split(ayrac);

                durak.durakadı = a[0];
                int b = Convert.ToInt32(a[1]);
                int c = Convert.ToInt32(a[2]);
                int d = Convert.ToInt32(a[3]);
                durak.boşpark = b;
                durak.tandembisiklet = c;
                durak.normalbisiklet = d;

                int musterisayısı = rnd.Next(1, 11);

                for (int p = 0; p < musterisayısı; p++)
                {
                    MusteriNesnesi musteri = new MusteriNesnesi();
                    musteri.ID = rnd.Next(1, 21);
                    int saat = rnd.Next(0, 24);
                    int dakika = rnd.Next(0, 60);
                    musteri.kiralamasaati = (saat.ToString() + ":" + dakika.ToString()).ToString();
                    müsterilistesi.Add(musteri);



                }
                durak.musteriler = müsterilistesi;
                return durak;


            }
            DurakNesnesi durak1 = NesneOlusturucu(duraklar, 0);
            DurakNesnesi durak2 = NesneOlusturucu(duraklar, 1);
            DurakNesnesi durak3 = NesneOlusturucu(duraklar, 2);
            DurakNesnesi durak4 = NesneOlusturucu(duraklar, 3);
            DurakNesnesi durak5 = NesneOlusturucu(duraklar, 4);
            DurakNesnesi durak6 = NesneOlusturucu(duraklar, 5);
            DurakNesnesi durak7 = NesneOlusturucu(duraklar, 6);
            DurakNesnesi durak8 = NesneOlusturucu(duraklar, 7);
            DurakNesnesi durak9 = NesneOlusturucu(duraklar, 8);

            DurakNesnesi Bisikletekleyici(DurakNesnesi durak) //müşteri sayısına göre biisklet sayısı güncelleme
            {
                
                if (durak.normalbisiklet < durak.musteriler.Count()) //müşteri sayısı bisiklet sayısından fazla ise sondan başlayarak yeterli olana kadar müşterileri siler
                {

                    for (int j = durak.musteriler.Count(); j > durak.normalbisiklet; j--)
                    {
                        MusteriNesnesi silinecek = durak.musteriler.ElementAt(j - 1);
                        durak.musteriler.Remove(silinecek);
                    }
                    int kiralanan = durak.musteriler.Count();
                    durak.normalbisiklet = durak.normalbisiklet - kiralanan;
                    if (durak.normalbisiklet < 0)
                    {
                        durak.normalbisiklet = 0;

                    }
                    durak.boşpark = durak.boşpark + kiralanan;
                }
                else
                {
                    durak.normalbisiklet = durak.normalbisiklet - durak.musteriler.Count();
                    durak.boşpark = durak.boşpark + durak.musteriler.Count();

                }
                return durak;


            }
            Bisikletekleyici(durak1);
            Bisikletekleyici(durak2);
            Bisikletekleyici(durak3);
            Bisikletekleyici(durak4);
            Bisikletekleyici(durak5);
            Bisikletekleyici(durak6);
            Bisikletekleyici(durak7);
            Bisikletekleyici(durak8);
            Bisikletekleyici(durak9);


            Tree agac = new Tree();

            agac.insert(durak1);
            agac.insert(durak2);
            agac.insert(durak3);
            agac.insert(durak4);
            agac.insert(durak5);
            agac.insert(durak6);
            agac.insert(durak7);
            agac.insert(durak8);
            agac.insert(durak9);

            agac.findAndWriteTreeInfo(agac.getRoot(), 9);
            Console.WriteLine("Ağacın PreOrder Dolaşılması : ");
            agac.preOrder(agac.getRoot());
            Console.WriteLine();
            Console.WriteLine("-----------ID Araması--------------");
            Console.WriteLine();
            Console.WriteLine("Aramak istediğiniz ID girin :");
            int arananID = Convert.ToInt32(Console.ReadLine());
            agac.preOrderileIDarama(agac.getRoot(), arananID);

            //---------------------------------------------------KİRALAMA İŞLEMLERİ---------------------------------//
            Console.WriteLine();
            Console.WriteLine("---------------KİRALAMA İŞLEMİ--------------");
            Console.WriteLine();

            Random abcd = new Random();
            MusteriNesnesi eklenenmüsteri = new MusteriNesnesi();
            
            DurakNesnesi eklenendurak = new DurakNesnesi();
            Console.WriteLine("Hangi Duraktan Kiralama Yapacaksınız ? : ");
            eklenendurak.durakadı = Console.ReadLine();
            Console.WriteLine("ID Numaranız : ");
            eklenenmüsteri.ID = Convert.ToInt32(Console.ReadLine()); 
            int saateklenen = abcd.Next(0, 24);
            int dakikaeklenen = abcd.Next(0, 60);
            eklenenmüsteri.kiralamasaati = (saateklenen.ToString() + ":" + dakikaeklenen.ToString()).ToString();

            DurakNesnesi Kiralama(DurakNesnesi eklenendurakpar,MusteriNesnesi eklenenmusteripar,DurakNesnesi durakpar) //Kiralama Methodu
            {
                if (eklenendurakpar.durakadı == durakpar.durakadı)
                {
                    if (durakpar.normalbisiklet<=0) //istenen durakta bisiklet kalmadıysa
                    {
                        Console.WriteLine(durakpar.durakadı+" Durağında Bisiklet Kalmamıştır.");

                    }
                    else
                    {
                        durakpar.musteriler.Add(eklenenmusteripar);
                        durakpar.boşpark = durakpar.boşpark + 1;
                        durakpar.normalbisiklet = durakpar.normalbisiklet - 1;
                        Console.WriteLine("Eklenen Durak : " + durakpar.durakadı + " Boş Park : " + durakpar.boşpark + " Normal Bisiklet: " + durakpar.normalbisiklet);
                        Console.WriteLine("Müşteriler : ");
                        foreach (MusteriNesnesi item in durakpar.musteriler)
                        {
                            Console.WriteLine("Müşteri ID : " + item.ID + "\t Kiralama Saati : " + item.kiralamasaati);

                        }

                    }
                    

                }
                return durakpar;

            }
            //kullanıcıdan alınan durak hangisi ise karşılaştıırma yaparak müşteriyi o durağa ekliyoruz
            Kiralama(eklenendurak, eklenenmüsteri, durak1);  
            Kiralama(eklenendurak, eklenenmüsteri, durak2);
            Kiralama(eklenendurak, eklenenmüsteri, durak3);
            Kiralama(eklenendurak, eklenenmüsteri, durak4);
            Kiralama(eklenendurak, eklenenmüsteri, durak5);
            Kiralama(eklenendurak, eklenenmüsteri, durak6);
            Kiralama(eklenendurak, eklenenmüsteri, durak7);
            Kiralama(eklenendurak, eklenenmüsteri, durak8);
            Kiralama(eklenendurak, eklenenmüsteri, durak9);





            //--------------------------------------------------HASHTABLE İŞLEMLERi----------------------------------------// 


            Hashtable durakhash = new Hashtable();

            durakhash.Add(durak1.durakadı, durak1);
            durakhash.Add(durak2.durakadı, durak2);
            durakhash.Add(durak3.durakadı, durak3);
            durakhash.Add(durak4.durakadı, durak4);
            durakhash.Add(durak5.durakadı, durak5);
            durakhash.Add(durak6.durakadı, durak6);
            durakhash.Add(durak7.durakadı, durak7);
            durakhash.Add(durak8.durakadı, durak8);
            durakhash.Add(durak9.durakadı, durak9);
            Console.WriteLine();
            Console.WriteLine("-------------------HASHTABLE-------------------");
            Console.WriteLine();
            
            
            foreach (DictionaryEntry oge in durakhash)
            {
                DurakNesnesi bilgiler = (DurakNesnesi)oge.Value;
                Console.WriteLine("Durak ismi : "+ oge.Key  + " \tBoş Park : "+bilgiler.boşpark+" \tTandem Bisiklet : "+bilgiler.tandembisiklet+" \tNormal Bisiklet : "+bilgiler.normalbisiklet);

            }

            DurakNesnesi BeştenFazlaKontrol(DurakNesnesi durak) //boş park sayısı 5den fazla olan duraklara bisiklet ekler
            {
                if (durak.boşpark>5)
                {
                    durak.normalbisiklet = durak.normalbisiklet + 5;
                    durak.boşpark = durak.boşpark - 5;
                }
                return durak;
            }

            BeştenFazlaKontrol(durak1);
            BeştenFazlaKontrol(durak2);
            BeştenFazlaKontrol(durak3);
            BeştenFazlaKontrol(durak4);
            BeştenFazlaKontrol(durak5);
            BeştenFazlaKontrol(durak6);
            BeştenFazlaKontrol(durak7);
            BeştenFazlaKontrol(durak8);
            BeştenFazlaKontrol(durak9);
            Console.WriteLine();
            
            //Durak isimlerine göre durak bilgilerini güncelledim.//
            durakhash["İnciraltı"]= durak1;
            durakhash["Sahilevleri"] = durak2;
            durakhash["Doğal Yaşam Parkı"] = durak3;
            durakhash["Bostanlı İskele"] = durak4;
            durakhash["Karşıyaka"] = durak5;
            durakhash["Mavişehir"] = durak6;
            durakhash["Çiğli"] = durak7;
            durakhash["Bayraklı"] = durak8;
            durakhash["Bornova"] = durak9;


            Console.WriteLine("---------------------------GÜNCELLEMEDEN SONRAKİ HASHTABLE----------------------");
            foreach (DictionaryEntry oge in durakhash)
            {
                DurakNesnesi bilgiler = (DurakNesnesi)oge.Value;
                Console.WriteLine("Durak ismi : " + oge.Key + " \tBoş Park : " + bilgiler.boşpark + " \tTandem Bisiklet : " + bilgiler.tandembisiklet + " \tNormal Bisiklet : " + bilgiler.normalbisiklet);

            }
            Console.WriteLine();


            //-----------------------------------------------------------------HEAP------------------------------//
            Heap theHeap = new Heap(9);
            theHeap.ekleme(durak1);
            theHeap.ekleme(durak2);
            theHeap.ekleme(durak3);
            theHeap.ekleme(durak4);
            theHeap.ekleme(durak5);
            theHeap.ekleme(durak6);
            theHeap.ekleme(durak7);
            theHeap.ekleme(durak8);
            theHeap.ekleme(durak9);

            theHeap.HeapYazdırma();
            Console.WriteLine();
            Console.WriteLine("Normal Bisiklet Sayısı En Fazla Olan 3 İstasyon : ");
            for(int i = 0; i < 3; i++)
            {
                DurakNesnesi deger =theHeap.silme().getKey();
                Console.WriteLine("Durak ismi : " + deger.durakadı + " \tBoş Park : " + deger.boşpark + " \tTandem Bisiklet : " + deger.tandembisiklet + " \tNormal Bisiklet : " + deger.normalbisiklet);
            }


            Console.ReadKey();



        }
    }
}
