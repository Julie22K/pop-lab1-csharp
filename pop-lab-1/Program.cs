using System.Threading;
using System;

namespace threaddemo
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Program()).Start();
            Console.ReadKey();
        }

        private List<int> timers = new List<int>();
        public List<int> Timers { get { return timers; } }

        int number_threads = 8;
        void Start()
        {

            for (int i = 0; i < number_threads; i++)
            {
                Thread newThread = new Thread(new ParameterizedThreadStart(Calculator));
                Random rnd = new Random();
                int time = rnd.Next(1, 21);

                timers.Add(time);

                newThread.Start(time);
            }

            timers.Sort();

            (new Thread(Stoper)).Start();

        }
        int step = 2;
        void Calculator(object? time)
        {
            long sum = 0;
            long numeric = 0;
            long number = 0;
            do
            {
                number += step;
                numeric++;
                sum += number;
            } while (timers.Contains(Convert.ToInt32(time)));
            Console.WriteLine($"Thread id:{Thread.CurrentThread.ManagedThreadId}\t sum:{sum}\t numeric:{numeric}\ttime:{time} sec");
        }

        /*private bool canStop = false;
        public bool CanStop { get { return canStop; } }*/

        public void Stoper()
        {
            List<int> distance_timers = new List<int>() { 0 };

            for (int i = 0; i < timers.Count() - 1; i++)
            {
                distance_timers.Add(timers[i + 1] - timers[i]);
            }

            for (int i = 0; i < timers.Count(); i++)
            {
                Thread.Sleep(distance_timers[i] * 1000);
                timers[i] = 0;
                /*for (int t = 0; t < timers.Count(); t++)
                {
                    if ((timers[t] - timers[i]) <= 0) timers[t] -= timers[i];
                }*/
            }


        }

    }
}