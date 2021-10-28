using System;
using System.Diagnostics;

namespace ACA_TCP_AD
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table(50, 50, 200);//создание города и задание начальных данных занимет +-26сек при 200 вершинах;
            //table.PrintPheromonesMap();
            //Console.WriteLine("Start");
            //table.PrintPheromonesMap();
            for (int i = 0; i < 30; i++)
            {
                table.GetBestWay();
                //table.PrintPheromonesMap();
                //Console.WriteLine();
            }
            
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //table.GetBestWay();

            //stopwatch.Stop();
            //TimeSpan ts = stopwatch.Elapsed;
            //string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //Console.WriteLine(elapsedTime, "RunTime");
        }
    }
}
