﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Printer
    {
        public static void PrintVector<T>(List<T> vectors, string vectorName)
        {
            int no = 0;
            Console.WriteLine(vectorName);
            foreach (var vector in vectors)
            {
                Console.WriteLine(vector);
                no++;
            }
            Console.WriteLine("Number of printed lines: " + no);
            Console.WriteLine();
        }
    }
}
