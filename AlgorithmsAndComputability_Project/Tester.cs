using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    public class Tester
    {
        private InputLoader _loader;
        public Tester(InputLoader loader)
        {
            _loader = loader;
        }

        public void RunTest(string filename, int expectedResult)
        {
            var projects = new List<Project>();
            var experts = new List<Expert>();
            int featureCount = 0;

            Console.WriteLine("Test name: " + filename);
            Console.WriteLine("##################################");
            _loader.ProcessInput(projects, experts, ref featureCount, filename);
            var projectsCopy = projects.ConvertAll(p => new Project(p));
            var algorithm = new Algorithm();
            var result = algorithm.Run(projects, experts, featureCount);
            Printer.PrintVector(projectsCopy, "Projects before assignment:");

            Printer.PrintVector(projects, "Projects after assignment: ");
            Printer.PrintVector(experts, "Unused experts after assignment: ");
            Printer.PrintVector(result, "Used experts: ");
            if(result.Count == expectedResult)
            {
                Console.WriteLine("SUCCESS RESULT:{0} EXPECTED:{1}", result.Count, expectedResult);
            }
            else
            {
                Console.WriteLine("FAILURE RESULT:{0} EXPECTED:{1}", result.Count, expectedResult);
            }
            Console.WriteLine("##################################");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
