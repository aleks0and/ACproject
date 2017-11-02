using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
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
            var usedExperts = algorithm.Run(projects, experts, featureCount);
            Printer.PrintVector(projectsCopy, "Projects before assignment:");

            Printer.PrintVector(projects, "Projects after assignment: ");
            Printer.PrintVector(experts, "All experts after assignment: ");
            Printer.PrintVector(usedExperts, "Used experts: ");
            if (usedExperts.Count == expectedResult)
            {
                Console.WriteLine("SUCCESS RESULT:{0} EXPECTED:{1}", usedExperts.Count, expectedResult);
            }
            else
            {
                Console.WriteLine("FAILURE RESULT:{0} EXPECTED:{1}", usedExperts.Count, expectedResult);
            }
            Console.WriteLine("##################################");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
