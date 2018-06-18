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

    abstract class Database
    {
        public Database(String Id) => this.Id = Id;
        public string Id { get; set; }
        public abstract void accept(Report p);
    }

    class FinantialDatabase : Database
    {
        private List<int> Accounts;

        public FinantialDatabase(String Id, List<int> data) : base(Id) 
        { 
            this.Accounts = data;
        }

        /* your code here */
    }

    class CarsDatabase : Database
    {
        private List<int> CarsCosts;
        private List<int> CarsCapacity;

        public CarsDatabase(String Id, List<int> CarsCosts, List<int> CarsCapacity) : base(Id) 
        {
            this.CarsCosts = CarsCosts;
            this.CarsCapacity = CarsCapacity;
        }

        /* your code here */
    }


    abstract class Report
    {
        public abstract void PrintHeader();
        public abstract void PrintFooter();

        /* your code here */
    }

    class FinantialReport:Report
    {
        /* your code here */
    }

    class TransportationReport:Report
    {
        /* your code here */
    }
}