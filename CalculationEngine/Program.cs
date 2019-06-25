using Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Program
    {
        private const int HOURS = 1000;//3600000;
        private const int DAYS = 1000 * 24;//86400000;
        private const int WEEKS = 1000 * 24 * 7;//604800000;

        private DBConnection dbConn;

        private APICallHandler apiCall;

        private Calculation calcul;

        public Program()
        {
            dbConn = DBConnection.Instance();
            apiCall = new APICallHandler();
            calcul = new Calculation();
        }

        public void begin()
        {


            //if (dbConn.IsConnect())
            //{
            //    //suppose col0 and col1 are defined as VARCHAR in the DB
            //    string query = "SELECT id, device_id FROM devices";
            //    var cmd = new MySqlCommand(query, dbConn.Connection);
            //    var reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        string someStringFromColumnZero = reader.GetString(0);
            //        string someStringFromColumnOne = reader.GetString(1);
            //        Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
            //    }

            //    //string query2 = "INSERT INTO `devices` (`id`, `Device_ID`) VALUES (NULL, '2');";
            //    //var cmd2 = new MySqlCommand(query2, dbCon.Connection);
            //    //var writer = cmd2.ExecuteReader();

            //    //string query3 = "SELECT id, device_id FROM devices";
            //    //var cmd3 = new MySqlCommand(query3, dbCon.Connection);
            //    //var reader3 = cmd3.ExecuteReader();
            //    //while (reader3.Read())
            //    //{
            //    //    string someStringFromColumnZero = reader3.GetString(0);
            //    //    string someStringFromColumnOne = reader3.GetString(1);
            //    //    Console.WriteLine(someStringFromColumnZero + "," + someStringFromColumnOne);
            //    //}

            //    dbConn.Close();

            //    Console.ReadLine();
            //}


            Task taskHours = new Task(() => SendMessage(HOURS));
            Task taskDays = new Task(() => SendMessage(DAYS));
            Task taskWeeks = new Task(() => SendMessage(WEEKS));

            taskHours.Start();
            taskDays.Start();
            taskWeeks.Start();

            Console.Read();
        }

        private async void SendMessage(int mili)
        {
            while (true)
            {
                //mut.WaitOne();

                Console.WriteLine("Time : " + mili);
                float[] values = await apiCall.PostAsync();
                Thread.Sleep(mili);

                //mut.ReleaseMutex();
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
