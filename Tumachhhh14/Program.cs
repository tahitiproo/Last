#define DEBUG
using System;
using static System.Attribute;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tumachhhh14
{
    [DevInfoAttributeForBank("Даниил Морозов", Organization = "КФУ")] //14.1 HW
    public class Bank_Account
    {
        private string owner;
        private byte id;
        private decimal Balance { get; set; }
        public enum Acc_Type : byte
        {
            Сберегательный,
            Накопительный
        }
        private Acc_Type type;
        public byte ID
        {
            get => id;
            set => id = value;
        }
        public Acc_Type Type
        {
            get => type;
            set => type = value;
        }
        public string Owner
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
        [Conditional("DEBUG")] //14.1 CW
        public void DumpToScreen()
        {
            Console.WriteLine($"{Owner}\n{ID}\n{Balance}\n{Type}");
        }
        public void Print() => Console.WriteLine($"Id: {ID}\nBalace: {Balance}\nType: {Type}");
        public void Add(decimal cash)
        {
            Balance += cash;
            Console.WriteLine($"Done! Balance: {Balance}");
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
                Console.WriteLine("Not enought money");
            }
        }

    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor)]
    class DevInfoAttributeForBank : Attribute
    {
        public string Developer;
        public string Organization
        {
            get;
            set;
        }
        public DevInfoAttributeForBank(string developer)
        {
            Developer = developer;
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor)]
    class DeveloperInfoAttribute : Attribute
    {
        public string Developer;
        public string Date
        {
            get;
            set;
        }
        public DeveloperInfoAttribute(string developer)
        {
            Developer = developer;
        }
    }
    [DeveloperInfoAttribute("Даниил Морозов", Date = "16.12.2022")] // 14.2 CW
    public class RationalDigits
    {
        public int Chislitel { get; set; }
        public int Znamenatel { get; set; }
        public RationalDigits(int n, int m)
        {
            Chislitel = n;
            Znamenatel = m;
        }
        public RationalDigits() { }
        public static bool operator ==(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel == d2.Chislitel && d1.Znamenatel == d2.Znamenatel;
        }
        public static bool operator !=(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel != d2.Chislitel || d1.Znamenatel != d2.Znamenatel;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is RationalDigits d1)
                return Chislitel == d1.Chislitel && Znamenatel == d1.Znamenatel;
            return false;
        }
        public static bool operator >(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel * d2.Znamenatel > d2.Chislitel * d1.Znamenatel;
        }
        public static bool operator <(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel * d2.Znamenatel < d2.Chislitel * d1.Znamenatel;
        }
        public static bool operator <=(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel / d1.Znamenatel <= d2.Chislitel / d2.Znamenatel;
        }
        public static bool operator >=(RationalDigits d1, RationalDigits d2)
        {
            return d1.Chislitel / d1.Znamenatel >= d2.Chislitel / d2.Znamenatel;
        }
        public static RationalDigits operator +(RationalDigits d1, RationalDigits d2)
        {
            return new RationalDigits()
            {
                Chislitel = (d1.Chislitel * d2.Znamenatel + d2.Chislitel * d1.Znamenatel),
                Znamenatel = (d1.Znamenatel * d2.Znamenatel)
            };
        }
        public static RationalDigits operator -(RationalDigits d1, RationalDigits d2)
        {
            return new RationalDigits()
            {
                Chislitel = (d1.Chislitel * d2.Znamenatel - d2.Chislitel * d1.Znamenatel),
                Znamenatel = (d1.Znamenatel * d2.Znamenatel)
            };
        }
        public static RationalDigits operator ++(RationalDigits d1)
        {
            return new RationalDigits() { Chislitel = d1.Chislitel += d1.Znamenatel, Znamenatel = d1.Znamenatel };
        }
        public static RationalDigits operator --(RationalDigits d1)
        {

            return new RationalDigits() { Chislitel = d1.Chislitel -= d1.Znamenatel, Znamenatel = d1.Znamenatel };
        }
        public override string ToString()
        {
            string t = $"{Chislitel} / {Znamenatel}";
            return t;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Bank_Account b = new Bank_Account(123, 1000, Bank_Account.Acc_Type.Сберегательный);
            b.DumpToScreen();//14.1 CW

            MemberInfo info;//14.2 CW
            info = typeof(RationalDigits);
            object[] attr = info.GetCustomAttributes(false);
            foreach (object o in attr) // получаем информацию из метаданных при помощи Рефлексии
            {
                DeveloperInfoAttribute r = (DeveloperInfoAttribute)o;
                Console.WriteLine("Разработчик: {0}\nДата: {1}", r.Developer, r.Date);
            }

            MemberInfo info1;//14.1 HW 
            info1 = typeof(Bank_Account);
            object[] attr1 = info1.GetCustomAttributes(false);
            foreach (object o in attr1) // получаем информацию из метаданных при помощи Рефлексии
            {
                DevInfoAttributeForBank r = (DevInfoAttributeForBank)o;
                Console.WriteLine("Разработчик: {0}\nОрганизация: {1}", r.Developer, r.Organization);
            }

            Console.ReadKey();
        }
    }
}
