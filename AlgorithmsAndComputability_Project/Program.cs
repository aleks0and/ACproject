using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    class Program
    {
        public struct Expert
        {
            public List<int> expertVector;
            public int weight;
            public int assignedProject;
        }

        public struct Project
        {
            public List<int> projectVector;
        }

        static void Main(string[] args)
        {
            //initializing arguments
            List<List<int>> projects = new List<List<int>>();
            List<List<int>> experts = new List<List<int>>();
            int noOfProjects = 0;
            int noOfExperts = 0;
            int noOfFeatures = 0;

            //processing csv (assigning values to projects, experts, noOfProjects, noOfExperts, noOfFeatures)
            ProcessCSV(ref projects, ref experts, ref noOfProjects, ref noOfExperts, ref noOfFeatures);

            //creating struct Expert for each expert with weight = -1 and assignedProject = -1
            List<Expert> expertStructs = new List<Expert>();
            for(int i = 0; i < noOfExperts; i++)
            {
                Expert e = new Expert();
                e.assignedProject = -1;
                e.weight = -1;
                e.expertVector = experts.ElementAt(i);

                expertStructs.Insert(i, e);
            }
            List<Project> projectStructs = new List<Project>();
            for (int i = 0; i < noOfProjects; i++)
            {
                Project p = new Project();
                p.projectVector = projects.ElementAt(i);

                projectStructs.Insert(i, p);
            }



            //after having above, we can go with implementation of the pseudocode
            //using List<List<int>> projects and List<Expert> expertStructs for assigning experts to projects

            List<int> sum = SumExperts(expertStructs, noOfFeatures, noOfExperts);
            for (int y = 0; y < noOfFeatures; y++)
            {
                Console.WriteLine(sum[y]);
            }
            Console.WriteLine();

            CalculateExpertsWeights(ref expertStructs, sum);

            SortExperts(ref expertStructs);
            
            int indProj = 0;
            List<int> oldSum = new List<int>();
            
            while(oldSum != sum /*&& (experts are not finished || projects are not finished)*/)
            {
                //TODO
            }

        }

        public static void CalculateExpertsWeights(ref List<Expert> le, List<int> sum)
        {
            List<int> sumWeight = new List<int>(sum);
            int features = sum.Count;
            int temp = 0;
            while (temp != features)
            {
                int max = -1;
                int maxind = -1;
                for (int i = 0; i < features; i++)
                {
                    if (sum.ElementAt(i) > max)
                    {
                        max = sum.ElementAt(i);
                        maxind = i;
                    }
                }
                sum[maxind] = -99;
                sumWeight[maxind] = temp + 1;
                temp++;
            }

            for(int y=0;y<features;y++)
            {
                Console.WriteLine(sumWeight[y]);
            }
        }

        public static void SortExperts(ref List<Expert> le)
        {
            //TODO
        }

        public static List<int> SumExperts(List<Expert> le, int noOfFeatures, int noOfExperts)
        {
            List<int> sum = new List<int>();

            for(int i = 0; i < noOfFeatures; i++)
            {
                int sumField = 0;
                for(int j = 0; j < noOfExperts; j++)
                {
                    sumField += le.ElementAt(j).expertVector.ElementAt(i);
                }
                sum.Insert(i, sumField);
            }

            return sum;
        }

        public static void ProcessCSV(ref List<List<int>> projects, ref List<List<int>> experts, ref int noOfProjects, ref int noOfExperts, ref int noOfFeatures)
        {
            string fileName = "INPUT.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Specification\input", fileName);

            using (StreamReader sr = new StreamReader(path))
            {
                //getting data from first line
                string currentLine = sr.ReadLine();
                char delimiter = ';';
                string[] values = currentLine.Split(delimiter);
                noOfProjects = Convert.ToInt32(values[0]);
                noOfExperts = Convert.ToInt32(values[1]);
                noOfFeatures = Convert.ToInt32(values[2]);

                //reading projects
                int ind = 0;
                int temp = noOfProjects;
                while (temp != 0)
                {
                    currentLine = sr.ReadLine();
                    char delimiter_p = ';';
                    string[] values_p = currentLine.Split(delimiter_p);

                    List<int> proj = new List<int>();
                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        proj.Insert(i, Convert.ToInt32(values_p[i]));
                    }
                    projects.Insert(ind, proj);
                    ind++;
                    temp--;
                }

                //reading experts
                ind = 0;
                temp = noOfExperts;
                while (temp != 0)
                {
                    currentLine = sr.ReadLine();
                    char delimiter_p = ';';
                    string[] values_p = currentLine.Split(delimiter_p);

                    List<int> exp = new List<int>();
                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        exp.Insert(i, Convert.ToInt32(values_p[i]));
                    }
                    experts.Insert(ind, exp);
                    ind++;
                    temp--;
                }
            }
        }
        



    }
}
