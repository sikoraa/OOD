using System;
using System.Collections.Generic;

namespace SolutionReports7
{
    class Program
    {
        public static void Main(string[] args)
        {
            var reports = new List<Report>{ 
                new FinantialReport(),
                new TransportationReport()
            };

            var data = new List<Database>{
                new FinantialDatabase( "MS Finances", 
                                new List<int>{10, 14, 50, -20, 43} ),
                new CarsDatabase( "MS Transportation",
                                new List<int>{10, 4, 5, 8, 4},
                                new List<int>{5, 2, 2, 5, 5} )
             };

            foreach(var r in reports)
                r.Process(data);
        }
    }
    // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY  // BAZY 
    abstract class Database
    {
        public Database(String Id) => this.Id = Id;
        public string Id { get; set; }
        public abstract void accept(Report p);
        public abstract void Connect(Report p);
        
    }

    // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB // FINANTIAL DB 
    class FinantialDatabase : Database
    {
        private List<int> Accounts;

        public FinantialDatabase(String Id, List<int> data) : base(Id) 
        { 
            this.Accounts = data;
        }

        public override void accept(Report p)
        {
            p.connect(this);
            p.Print(this);           
        }

        
        public int GetAccountCount() { return Accounts.Count; }
        public int GetTotalBalance()
        {
            int balance = 0;
            for (int i = 0; i < Accounts.Count; ++i)
                balance += Accounts[i];
            return balance;
        }

        public int GetAccountBalance(int accountNo)
        {
            return Accounts[accountNo];
        }
        public int GetDeposits()
        {
            int deposits = 0;
            for (int i = 0; i < Accounts.Count; ++i)
                if (Accounts[i] > 0) //
                    deposits += Accounts[i];
            return deposits;
        }

        public override void Connect(Report p)
        {
            p.connect(this);
        }



        /* your code here */
    }
    // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB // CAR DB 
    class CarsDatabase : Database
    {
        private List<int> CarsCosts;
        private List<int> CarsCapacity;

        public CarsDatabase(String Id, List<int> CarsCosts, List<int> CarsCapacity) : base(Id) 
        {
            this.CarsCosts = CarsCosts;
            this.CarsCapacity = CarsCapacity;
        }

        public override void accept(Report p)
        {
            p.Print(this);
        }

        public int GetTotalCapacity()
        {
            int capacity = 0;
            for (int i = 0; i < CarsCapacity.Count; ++i)
                capacity += CarsCapacity[i];
            return capacity;

        }

        public int GetTotalCost()
        {
            int cost = 0;
            for (int i = 0; i < CarsCosts.Count; ++i)
                cost += CarsCosts[i];
            return cost;
        }

        public int GetCarsNumber()
        {
            return CarsCosts.Count;
        }

        public int GetNeededPeople()
        {
            int ppl = 0;
            for (int i = 0; i < CarsCapacity.Count; ++i)
                if (CarsCapacity[i] > 4)
                    ppl += 2;
                else
                    ppl += 1;
            return ppl;
        }

        public override void Connect(Report p)
        {
            p.connect(this);
            p.Print(this);
        }
        /* your code here */
    }

    // REPORT ABSTRACT
    abstract class Report
    {
        public abstract void PrintHeader();
        public abstract void PrintFooter();
        internal abstract void Process(List<Database> data);
        public abstract void Print(FinantialDatabase db);
        public abstract void Print(CarsDatabase db);
        internal abstract void connect(FinantialDatabase db);
        internal abstract void connect(CarsDatabase db);


        protected Database db;
        
        protected int width;
        //protected char separator;
        //protected char line;
        /* your code here */
    }
    // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS // REPORTS 
    class FinantialReport : Report
    {
        // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL // FINANTIAL 
        /* your code here */
        FinantialDatabase dd;
        CarsDatabase cd;
        public override void Print(FinantialDatabase d)
        {
            //Console.WriteLine("fReport + fDb");
            db = d;
            dd = d;
            width = 40;
            
            PrintFooter();
        }

        public override void Print(CarsDatabase d)
        {

            Console.WriteLine("Cars Current Costs Per Month: " + d.GetTotalCost());
        }
      
        public override void PrintFooter()//FinantialDatabase d)
        {
            string s = "";
            s += "Accounts Total Balance: " + dd.GetTotalBalance();

            s += '\n';

            s += "Accounts Total Deposits: " + dd.GetDeposits();
            s += '\n';
            for (int i = 0; i < dd.GetAccountCount(); ++i)
                s += "-- Account:" + i + " : " + dd.GetAccountBalance(i) + '\n';
            Console.WriteLine(s);
        }
        public void printline(int width)
        {
            string s = "";
            for (int i = 0; i < width; ++i)
            {
                if (i == 0 || i == width - 1)
                    s += '|';
                else
                    s += '=';
            }
            Console.WriteLine(s);
        }
        public override void PrintHeader()
        {
            int width = 40;
            string s = "Finantial Report";
            for (int i = width - "Finantial Report".Length; i > 0; --i)
                s += " ";
            s += '\n';
            Console.WriteLine(s);
        }

        internal override void Process(List<Database> data)
        {
            int width = 40;
            PrintHeader();
            printline(width);
            for (int i = 0; i < data.Count; ++i)
            {
                data[i].accept(this);           
            }
            printline(width);

        }

        internal override void connect(FinantialDatabase db)
        {
            this.db = db;
        }

        internal override void connect(CarsDatabase db)
        {
            this.db = null;
        }

        //internal override void Process(List<Database> data)
        //{
        //    ;
        //}

        //internal override void Process(List<Database> data)
        //{
        //    throw new NotImplementedException();
        //}
    }






    // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION  // TRANSPORTATION 

    class TransportationReport : Report
    {
        TransportationReport dd;
        FinantialReport fd;
        public override void Print(FinantialDatabase db)
        {
            string s = "Possible Transportation Investment: " + db.GetDeposits();
            int p = s.Length;
            for (int i = 50 - p; i > 0; --i)
                s += "";
            s += '\n';
            Console.WriteLine(s);
        }

        public override void Print(CarsDatabase db)
        {
            string s = "Cars Number: " + db.GetCarsNumber();
            int p = s.Length;
            for (int i = 50 - p; i > 0; --i)
                s += " ";
            s += '\n';

            string tmp = "People Needed: " + db.GetNeededPeople();
            p = tmp.Length;
            for (int i = 50 - p; i > 0; --i)
                tmp += " ";
            tmp += '\n';

            string tmp2 = "Total Cargo Load:" + db.GetTotalCapacity();
            p = tmp2.Length;
            for (int i = 50 - p; i > 0; --i)
                tmp2 += " ";
            tmp2 += '\n';

            s += tmp;
            s += tmp2;
            Console.WriteLine(s);
        }

        /* your code here */
        public override void PrintFooter()
        {
            
        }

        public override void PrintHeader()
        {
            int width = 50;
            string s = "Transportation Report";
            for (int i = width - "Transportation Report".Length; i > 0; --i)
                s += " ";
            s += '\n';
            Console.WriteLine(s);
        }

        internal override void connect(FinantialDatabase db)
        {
            ;
        }

        internal override void connect(CarsDatabase db)
        {
            this.db = db;
        }

        public void printline(int width)
        {
            string s = "";
            for (int i = 0; i < width; ++i)
            {
                if (i == 0 || i == width - 1)
                    s += '|';
                else
                    s += '-';
            }
            Console.WriteLine(s);
        }
        internal override void Process(List<Database> data)
        {
            //int width = 50;
            //string s = "Transportation Report";
            //for (int i = width - "Transportation Report".Length; i > 0; --i)
            //    s += " ";
            //s += '\n';
            //Console.WriteLine(s);
            PrintHeader();
            printline(50);
            Console.WriteLine("Transportation Report");
            for (int i = 0; i < data.Count; ++i)
            {
                data[i].accept(this);
            }
            printline(50);
        }
    }
}