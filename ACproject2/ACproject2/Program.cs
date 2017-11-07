using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new Dictionary<string, int>();

            /*
            tests.Add("INPUT.csv", 100);
            tests.Add("INPUT3.csv", 14);
            tests.Add("test1.csv", 3);
            tests.Add("test2.csv", 3);
            tests.Add("test3.csv", 3);
            tests.Add("test4.csv", 9);
            
            
            var loader = new InputLoader(';', @"..\..\..\Specification\input");
            var tester = new Tester(loader);
            foreach (var test in tests)
            {
                tester.RunTest(test.Key, test.Value);
            } 
            */
            //first argument is the path to the files second is the name of the files.
            if (args.Length < 3)
            {
                Console.WriteLine("To use the application properly please write \"ACproject2\" directory_of_the_input input_file_name ");
            }
            else if (args.Length == 3)
            {
                int expectedResult;
                if (Int32.TryParse(args[2],out expectedResult))
                { 
                    tests.Add(args[1], expectedResult);
                    var loader = new InputLoader(',', args[0]);
                    var tester = new Tester(loader);
                    foreach (var test in tests)
                    {
                        tester.RunTest(test.Key, test.Value);
                    }
                }
                
            }
            else
                Console.WriteLine("incorrect use of the program");
            Console.ReadLine();
        }
    }
}
