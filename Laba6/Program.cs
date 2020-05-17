using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    interface Tours
    {
        List<TouringTrip> ReadDate(string path);
        void SaveDate(List<TouringTrip> Date, string path);
    } 
    interface Meteo{
        void sort(List<MeteData> Days, out MeteData Most, out MeteData Less);
    }
    abstract public class MusicGroup:Tours
    {
        public string Name { get; set; }
        public string HeadName { get; set; }

        public List<TouringTrip> ReadDate(string path)
        {
            List<TouringTrip> g = new List<TouringTrip>();
            string text = "";
            using (StreamReader sr = new StreamReader(path))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach (string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if (MetaDete.Length == 5)
                {
                    TouringTrip d = new TouringTrip
                    {
                        City = MetaDete[0],
                        Date = MetaDete[1],
                        Count = Convert.ToInt32(MetaDete[2]),
                        Name = MetaDete[3],
                        HeadName = MetaDete[4]
                    };
                    g.Add(d);
                }
            }
            return g;
        }
        public void SaveDate(List<TouringTrip> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (TouringTrip g in Date)
                {

                    sw.WriteLine(g.City.Trim() + "|" + g.Date + "|" + g.Count + "|" + g.Name + "|" + g.HeadName + "/");

                }
            }
        }
        public static void ChangeDate(List<TouringTrip> Date)
        {
            Console.WriteLine("Enter Date of trip that`s need to change");
            string Nam = Console.ReadLine();
            TouringTrip Choosen = new TouringTrip();
            Choosen.Name = "";
            foreach (TouringTrip g in Date)
            {
                if (g.Date == Nam)
                {
                    Choosen = g;
                    break;
                }
            }
            if (Choosen.Name != "")
            {
                Console.WriteLine();
                Console.WriteLine("1)Change City\n2)Change Date\n3)Change Count\n4)Change Name\n5)Change Head Name\n6)Delete");
                char key = Console.ReadKey().KeyChar;
                Console.WriteLine("Enter new value");
                try
                {
                    if (key == '1')
                    {
                        Choosen.City = Console.ReadLine();
                    }
                    if (key == '2')
                    {

                        Choosen.Date = Console.ReadLine();
                    }
                    if (key == '3')
                    {
                        Choosen.Count = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(Choosen.Count);

                    }
                    if (key == '4')
                    {
                        Choosen.Name = Console.ReadLine();
                    }
                    if (key == '5')
                    {
                        Choosen.HeadName = Console.ReadLine();
                    }
                    if (key == '6')
                    {
                        Date.Remove(Choosen);
                    }
                }
                catch
                {
                    Console.WriteLine("New value is incorrect");
                }
                //Lviv|29.03.2019|7|Great Pistols|Ridme/
            }
            else
            {
                Console.WriteLine("TouringTrip Not found");
            }

        }
        public static void AddNew(List<TouringTrip> Date)
        {
            Console.WriteLine("Enter City");
            TouringTrip neww = new TouringTrip();
            neww.City = Console.ReadLine();
            Console.WriteLine("Enter Date");
            neww.Date = Console.ReadLine();
            Console.WriteLine("Enter Count");
            neww.Count = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Group Name");
            neww.Name = Console.ReadLine();
            Console.WriteLine("Enter Head Name");
            neww.HeadName = Console.ReadLine();
            Date.Add(neww);
        }
        public static void ShowTable(List<TouringTrip> TouringTrip)
        {
            int MaxI = 8;
            int MaxN = 12;
            int MaxW = 7;
            int MaxC = 15;
            int MaxL = 11;
            Console.WriteLine("|  City  |    Date    | Count |   GroupName   | Head Name |");
            foreach (TouringTrip g in TouringTrip)
            {
                int ni = MaxI - Convert.ToString(g.City.Trim()).Length;
                int nn = MaxN - g.Date.Count();
                int nw = MaxW - Convert.ToString(g.Count).Length;
                int nc = MaxC - Convert.ToString(g.Name).Length;
                int nl = MaxL - Convert.ToString(g.HeadName).Length;
                Console.WriteLine("|" + Convert.ToString(g.City.Trim()) + PS(ni) + "|" + g.Date + PS(nn) + "|" +
                 Convert.ToString(g.Count) + PS(nw) + "|" + Convert.ToString(g.Name) + PS(nc) + "|"
                 + Convert.ToString(g.HeadName) + PS(nl) + "|");
            }
            Console.WriteLine(" p - Edit/Delete,\n d - Create\n n - search,\n m - Most Count\n t - To search city\nEnter - exit");
        }
        public static string PS(int count)
        {
            string s = "";
            for (int i = 0; i < count; i++)
            {
                s += " ";
            }
            return s;
        }
        //--------------------------------------------------------------------
        public abstract int MaxCount(List<TouringTrip> lst);
        public abstract void ToCity(List<TouringTrip> lst);
        public abstract char LastLetter(List<TouringTrip> lst);
    }
    public class TouringTrip : MusicGroup
    {
        public string City { get; set; }
        public string Date { get; set; }
        public int Count { get; set; }
        //-------------------------------------------------------------------
        public override int MaxCount(List<TouringTrip> lst)
        {
            Console.Clear();
            int IndexMax = 0;
            foreach (TouringTrip gs in lst)
            {
                if (gs.Count > lst[IndexMax].Count)
                {
                    IndexMax = lst.IndexOf(gs);
                }
            }
            int MaxI = 8;
            int MaxN = 12;
            int MaxW = 7;
            int MaxC = 15;
            int MaxL = 11;
            TouringTrip g = lst[IndexMax];
            Console.WriteLine("|  City  |    Date    | Count |   GroupName   | Head Name |");
            int ni = MaxI - Convert.ToString(g.City.Trim()).Length;
            int nn = MaxN - g.Date.Count();
            int nw = MaxW - Convert.ToString(g.Count).Length;
            int nc = MaxC - Convert.ToString(g.Name).Length;
            int nl = MaxL - Convert.ToString(g.HeadName).Length;
            Console.WriteLine("|" + Convert.ToString(g.City.Trim()) + PS(ni) + "|" + g.Date + PS(nn) + "|" +
             Convert.ToString(g.Count) + PS(nw) + "|" + Convert.ToString(g.Name) + PS(nc) + "|"
             + Convert.ToString(g.HeadName) + PS(nl) + "|");
            return g.Count;
        }
        public override void ToCity(List<TouringTrip> lst)
        {

            Console.WriteLine("Enter City name");
            string cim = Console.ReadLine();
            Console.Clear();
            int MaxI = 8;
            int MaxN = 12;
            int MaxW = 7;
            int MaxC = 15;
            int MaxL = 11;
            Console.WriteLine("|  City  |    Date    | Count |   GroupName   | Head Name |");
            foreach (TouringTrip g in lst)
            {
                if (g.City.Trim() == cim.Trim())
                {
                    int ni = MaxI - Convert.ToString(g.City.Trim()).Length;
                    int nn = MaxN - g.Date.Count();
                    int nw = MaxW - Convert.ToString(g.Count).Length;
                    int nc = MaxC - Convert.ToString(g.Name).Length;
                    int nl = MaxL - Convert.ToString(g.HeadName).Length;
                    Console.WriteLine("|" + Convert.ToString(g.City.Trim()) + PS(ni) + "|" + g.Date + PS(nn) + "|" +
                     Convert.ToString(g.Count) + PS(nw) + "|" + Convert.ToString(g.Name) + PS(nc) + "|"
                     + Convert.ToString(g.HeadName) + PS(nl) + "|");
                }

            }
            Console.WriteLine("Press any key for beak into full table");
            Console.ReadKey();
        }
        public override char LastLetter(List<TouringTrip> lst)
        {
            Console.WriteLine("Enter date of trip");
            string dt = Console.ReadLine();
            Console.Clear();
            foreach (TouringTrip g in lst)
            {
                if (g.Date == dt)
                {
                    Console.WriteLine(g.HeadName[g.HeadName.Length - 1]);
                    Console.WriteLine("Press any key to return into table");
                    Console.ReadKey();
                    return g.HeadName[g.HeadName.Length - 1];
                }
            }
            return '0';


        }
    }
    public class MeteData: Meteo
    {
        public string Data { get; set; }
        public float Tempreture { get; set; }
        public int AtmospherePressure { get; set; }
        public MeteData(string Data, float Temp, int Atmosphere)
        {
            this.Data = Data;
            Tempreture = Temp;
            AtmospherePressure = Atmosphere;
        }
        public MeteData()
        {
            this.Data = "";
            Tempreture = 0;
            AtmospherePressure = 0;
        }
        public void sort(List<MeteData> Days,out MeteData Most, out MeteData Less)
        {
            Most = Days[0];
            Less = Days[0];
            for (int i = 0; i < Days.Count; i++)
            {


                for (int k = 0; k < Days.Count; k++)
                {
                    if (Most.AtmospherePressure < Days[k].AtmospherePressure)
                    {
                        Most = Days[i];
                    }
                    if (Less.AtmospherePressure > Days[k].AtmospherePressure)
                    {
                        Less = Days[i];
                    }
                    if (Days[i].AtmospherePressure > Days[k].AtmospherePressure)
                    {
                        MeteData temp = Days[k];
                        Days[k] = Days[i];
                        Days[i] = temp;
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("Enter number of the task \n1)Meteo Date\n2)Goods storage");
            char d = '1';
            if ((d = (Char)Console.ReadKey().KeyChar) == '1')
            {
                Console.WriteLine();
                task1();
            }
            if (d == '2')
            {
                Console.WriteLine();
                task2();
            }
            Console.WriteLine((Char)Console.ReadKey().KeyChar);
        }
        static void task1()
        {
            MeteData Day5 = new MeteData("23.05.2020", 16, 737);
            MeteData Day4 = new MeteData("19.05.2020", 19, 730);
            MeteData Day3 = new MeteData("16.05.2020", 16, 748);
            MeteData Day1 = new MeteData("21.05.2020", 17, 747);
            MeteData Day2 = new MeteData("14.05.2020", 14, 740);
            MeteData Day6 = new MeteData("30.05.2020", 10, 737);
            MeteData Day7 = new MeteData("11.05.2020", 11, 730);
            MeteData Day8 = new MeteData("05.05.2020", 34, 707);
            MeteData Day9 = new MeteData("10.05.2020", 15, 732);
            MeteData Day10 = new MeteData("01.05.2020", 20, 750);
            List<MeteData> Days = new List<MeteData>();
            Days.Add(Day1); Days.Add(Day2); Days.Add(Day3); Days.Add(Day4); Days.Add(Day5);
            Days.Add(Day6); Days.Add(Day7); Days.Add(Day8); Days.Add(Day9); Days.Add(Day10);
            MeteData Most = Days[0];
            MeteData Less = Days[0];
            Days[0].sort(Days,out Most,out Less);
            Console.WriteLine("Date      |Temperature | Atmosphere Pressure");
            for (int i = 0; i < Days.Count; i++)
            {
                Console.WriteLine(Days[i].Data + "|" + Days[i].Tempreture + "          |" + Days[i].AtmospherePressure);
            }
            Console.WriteLine("\nThe greatest Atmosphere Pressure");
            Console.WriteLine(Most.Data + "|" + Most.AtmospherePressure);
            Console.WriteLine("The least Atmosphere Pressure");
            Console.WriteLine(Less.Data + "|" + Less.AtmospherePressure);
        }
        static void task2()
        {
            string path = "";
            TouringTrip nw = new TouringTrip();
            List<TouringTrip> goods = new List<TouringTrip>();
            Console.WriteLine("Enter path to file like '' or enter any to create new file");
            path = Console.ReadLine();
            try
            {
                goods = nw.ReadDate(path);
            }
            catch
            {
                path = "Data.txt";
            }

            while (true)
            {
                Console.Clear();
                MusicGroup.ShowTable(goods);
                var press = Console.ReadKey().Key;
                if (press.ToString() == "Enter")
                {
                    Main();
                }
                if (press.ToString() == "P")
                {
                    Console.WriteLine();
                    MusicGroup.ChangeDate(goods);
                    nw.SaveDate(goods, path);
                }
                if (press.ToString() == "D")
                {
                    Console.WriteLine();
                    MusicGroup.AddNew(goods);
                    nw.SaveDate(goods, path);
                }
                if (press.ToString() == "M")
                {
                    Console.WriteLine();
                    if (goods.Count > 0)
                    {
                        goods[0].MaxCount(goods);
                        Console.WriteLine("Press any key to return into table");
                        Console.ReadKey();
                    }
                    nw.SaveDate(goods, path);
                }
                if (press.ToString() == "T")
                {
                    Console.WriteLine();
                    if (goods.Count > 0)
                    {
                        goods[0].ToCity(goods);
                    }
                    nw.SaveDate(goods, path);
                }
                if (press.ToString() == "N")
                {
                    Console.WriteLine();
                    if (goods.Count > 0)
                    {
                        goods[0].LastLetter(goods);
                    }
                    nw.SaveDate(goods, path);
                }

            }

        }
    }
}
