using Database;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Program
    {
        private const int HOURS = 100 * 60;//3600000;
        private const int DAYS = HOURS * 2;//86400000;
        private const int WEEKS = HOURS * 3;//604800000;

        private DBConnection dbConn;

        private APICallHandler apiCall;

        private Calculation calcul;

        private Mutex mut = new Mutex();

        private object lockObject = new object();

        public Program()
        {
        }

        public void begin()
        {
            ThreadPool.QueueUserWorkItem(SendMessage, HOURS);
            ThreadPool.QueueUserWorkItem(SendMessage, DAYS);
            ThreadPool.QueueUserWorkItem(SendMessage, WEEKS);

            //Task taskHours = new Task(() => SendMessage(HOURS));
            //Task taskDays = new Task(() => SendMessage(DAYS));
            //Task taskWeeks = new Task(() => SendMessage(WEEKS));

            //taskHours.Start();
            //Thread.Sleep(1000);
            //taskDays.Start();
            //Thread.Sleep(1000);
            //taskWeeks.Start();

            Console.Read();
        }

        private async void SendMessage(object time)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

            int mili = (int)time;

            APICallHandler apiCall = new APICallHandler();
            Calculation calcul = new Calculation();
            //DBConnection db = DBConnection.Instance();
            //MySqlCommand cmd;
            //MySqlDataReader reader;

            string period = "";
            switch(mili)
            {
                case HOURS:
                    period = "HOUR";
                    break;
                case DAYS:
                    period = "DAY";
                    break;
                case WEEKS:
                    period = "WEEk";
                    break;
            }

            String jsonmetrics;
            String query;

            List<Metric> metrics;

            Dictionary<long, List<Metric>> dMetric;

            List<float> t;
            float sum, average;

            List<Metric> test = new List<Metric>();
            test.Add(new Metric(1, 1, "10.5", DateTime.Now));
            test.Add(new Metric(1, 1, "11.5", DateTime.Now));
            test.Add(new Metric(1, 1, "12.5", DateTime.Now));
            test.Add(new Metric(1, 1, "13.5", DateTime.Now));
            test.Add(new Metric(1, 1, "14.5", DateTime.Now));
            test.Add(new Metric(2, 2, "15.5", DateTime.Now));
            test.Add(new Metric(2, 2, "16.5", DateTime.Now));
            test.Add(new Metric(4, 4, "17.5", DateTime.Now));
            test.Add(new Metric(3, 3, "18.5", DateTime.Now));
            test.Add(new Metric(4, 4, "19.5", DateTime.Now));
            test.Add(new Metric(3, 3, "10", DateTime.Now));

            while (true)
            {
                dMetric = new Dictionary<long, List<Metric>>();
                metrics = new List<Metric>();

                jsonmetrics = await apiCall.PostAsync();

                metrics = test;// JsonConvert.DeserializeObject<List<Metric>>(jsonmetrics);

                foreach (var m in metrics)
                {
                    if(! dMetric.ContainsKey(m.Device_ID))
                        dMetric.Add(m.Device_ID, new List<Metric>());
                    dMetric[m.Device_ID].Add(m);
                }

                using (var db = new MySqlConnection("Server=localhost; database=calculatedmetrics; UID=root; password="))
                using (var cmd = db.CreateCommand())
                {
                    db.Open();
                    foreach (var o in dMetric)
                    {
                        t = new List<float>();
                        foreach (var m in o.Value)
                            t.Add(float.Parse(m.MetricValue, CultureInfo.InvariantCulture.NumberFormat));

                        sum = calcul.Sum(t);
                        average = calcul.Average(t);
                        //if (db.IsConnect())
                        //{

                        cmd.CommandText = "INSERT INTO `calculatedmetric` (`id`, `device_ID`, `MetricDate`, `period`, `sum`, `average`, `percent`)" +
                            " VALUES (NULL, '" + o.Value[0].Device_ID + "', '" + o.Value[0].MetricDate.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + period + "', '" +
                            sum + "', '" + average + "', '0');";
                        Console.WriteLine(cmd.CommandText);
                        using (var reader = cmd.ExecuteReader())
                        {
                            //cmd = new MySqlCommand(query, db.Connection);
                            //reader = cmd.ExecuteReader();
                            //reader.Close();
                        }
                        //}
                    }
                }


                Thread.Sleep(mili);
            }
        }

        

        static void Main(string[] args)
        {
            Program prog = new Program();
            Console.WriteLine("Begin admin... Press any key to end it");
            prog.begin();
            Console.Read();
            Console.WriteLine("End admin...");
        }
    }
}
