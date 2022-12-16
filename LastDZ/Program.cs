using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LastDZ
{
    public class BankTransaction
    {
        readonly DateTime data = DateTime.Now;
        private decimal summa;
        public decimal Summa // 13.1 СW
        {
            get { return summa; }
            set { summa = value; }
        }
        public BankTransaction(decimal summa)
        {
            Summa = summa;
        }
        public void PrintInfo()
        {
            Console.WriteLine($" Date:{data} Sum:{summa}");
        }
        public override string ToString()
        {
            return $"time: {data} money: {Summa}";
        }
    }
    public class Bank_Account
    {
        private Queue<BankTransaction> Queue = new Queue<BankTransaction>();
        public BankTransaction this[int index] => Queue.ToArray()[index]; // 13.2 CW индексаторы

        private string owner;
        private byte id;
        private decimal Balance { get; set; }
        public enum Acc_Type : byte
        {
            Сберегательный,
            Накопительный
        }

        private Acc_Type type;
        public byte ID //13.1 CW
        {
            get => id;
            set => id = value;// 13.1 HW
        }
        public Acc_Type Type // 13.1 CW
        {
            get => type;
            set => type = value; // 13.1 HW
        }
        public string Owner // 13.1 CW
        {
            get => owner;
            set => owner = value;
        }
        public Bank_Account() { }
        public Bank_Account(byte iD, decimal balance, Acc_Type type)
        {
            ID = iD;
            Balance = balance;
            Type = type;
        }
        public void Print() => Console.WriteLine($"Id: {ID}\nBalace: {Balance}\nType: {Type}");
        public void Add(decimal cash)
        {
            Balance += cash;
            Console.WriteLine($"Done! Balance: {Balance}");
            Queue.Enqueue(new BankTransaction(cash));
        }
        public void Lower(decimal cash)
        {
            if (Balance > 0)
            {
                if (Balance - cash > 0)
                {
                    Balance -= cash;
                    Console.WriteLine($"Done! Balance: {Balance}");
                }
                else
                {
                    Console.WriteLine($"Not enougth money! Balance: {Balance}");
                }
            }
            else
            {
                Console.WriteLine("Something is wrong!");
            }
            Queue.Enqueue(new BankTransaction(cash));
        }
        public void Transition(Bank_Account acc1, decimal perevod)
        {
            if (acc1.Balance > perevod)
            {
                acc1.Balance -= perevod;
                Balance += perevod;
            }
            else
            {
                Console.WriteLine("Недостаточно средств");
            }
            Queue.Enqueue(new BankTransaction(perevod));
        }
        public void Dispose()
        {
            foreach (var i in Queue)
            {
                StreamWriter t = new StreamWriter("TextFile.txt");
                t.WriteLine(i.ToString());
            }
            GC.SuppressFinalize(this);
        }

    }
    public class Buiding
    {
        public Buiding() { }
        public uint id, height, floors, flat_count, entrance_count;
        private static HashSet<uint> Last_Random_ID = new HashSet<uint>(0);
        public Buiding(uint id, uint height, uint floors, uint flat_count, uint entrance_count)
        {
            this.id = id;
            this.height = height;
            this.floors = floors;
            this.flat_count = flat_count;
            this.entrance_count = entrance_count;
        }
        public uint new_id()
        {
            Random r = new Random();
            id = (uint)r.Next(0, 255);
            if (!Last_Random_ID.Contains(id))
            {
                id++;
            }
            return id;
        }

        public void HeightOfFloor()
        {
            Console.WriteLine($"Height of floor is: {(double)height / (double)floors}");
        }
        public void FlatCountPerEntrance()
        {
            Console.WriteLine($"Flats per entrance: {flat_count / entrance_count}");
        }
        public void FlatCountPerFloor()
        {
            Console.WriteLine($"Flats per floor : {flat_count / entrance_count / floors}");
        }
        public void Print() => Console.WriteLine($"ID: {id}\nHeight: {height}\nFloors: {floors}\nAmount of flats: {flat_count}\nAmount of entrances: {entrance_count}");
    }
    public class BuildingContainer
    {
        Buiding[] buidings = new Buiding[10];
        public Buiding this[int index]
        {
            get => buidings[index];
            set => buidings[index] = value;
        }
        public BuildingContainer(Buiding[] buidings)
        {
            this.buidings = buidings;
        }
    }
    
    class Program
    {
        public static void Lab13dot1()
        {
            Console.WriteLine("Лаба 13.1");
            Bank_Account bank_Account1 = new Bank_Account();
            bank_Account1.Type = (Bank_Account.Acc_Type)0;
            bank_Account1.ID = 123;
            bank_Account1.Owner = "Данисимус";
            bank_Account1.Print();
        }
        public static void Lab13dot2()
        {
            Console.WriteLine("Лаба 13.2");
            BuildingContainer bc = new BuildingContainer(new[]
            {
                new Buiding(1,100,5,10,2),
                new Buiding(2,1000,10,20,3),
                new Buiding(3,200,6,30,4),
                new Buiding(4,250,7,40,5),
                new Buiding(5,300,12,50,6),
                new Buiding(6,500,11,60,7),
                new Buiding(7,600,580,70,7),
                new Buiding(8,666,40,80,9),
                new Buiding(9,123,13,90,10),
                new Buiding(10,420,333,100,12),
            });
            Console.WriteLine("Enter index");
            int index = int.Parse(Console.ReadLine());
            if (index < 0 || index > 9)
            {
                throw new IndexOutOfRangeException();
            }
            bc[index].Print();
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Bank_Account bank_Account = new Bank_Account();
            bank_Account.Add(100);
            bank_Account.Add(1000);
            bank_Account.Lower(200);
            Console.WriteLine(bank_Account[0].ToString());

            Lab13dot1();

            Lab13dot2();
        }
    }
}
