﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingLab
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Runner runner = new Runner();

            while (true)
            {
                runner.Start();

                Thread.Sleep(10000);

                //continue;

                runner.Stop();


            }
        }


    }
}
