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

            public Expert(List<int> ev, int w, int a)
            {
                expertVector = ev;
                weight = w;
                assignedProject = a;
            }

            public override string ToString()
            {
                string s = "[";
                for(int i = 0; i < expertVector.Count; i++)
                {
                    s += expertVector[i] + ", ";
                }
                s += "] " + weight;
                return s;
            }
        }

        public class ExpertComparison : IComparer<Expert>
        {
            private List<int> _expertSum;
            public ExpertComparison(List<int> expertSum)
            {
                _expertSum = expertSum;
                _expertSum = _expertSum
                    .Select((e, i) => new KeyValuePair<int, int>(e, i))
                    .OrderBy(e => e.Key)
                    .Select(e => e.Value)
                    .ToList();
            }
            public int Compare(Expert x, Expert y)
            {
                for(int i = 0; i < _expertSum.Count; i++)
                {
                    if(x.expertVector[_expertSum[i]] == 1 && y.expertVector[_expertSum[i]] == 0)
                    {
                        return -1;
                    }
                    else if(x.expertVector[_expertSum[i]] == 0 && y.expertVector[_expertSum[i]] == 1)
                    {
                        return 1;
                    }
                }
                return y.weight - x.weight;
            }
        }

        public struct Project
        {
            public List<int> projectVector;

            public Project(List<int> p)
            {
                projectVector = p;
            }
        }

        public static void PrintExperts(List<Expert> experts)
        {
            foreach(var expert in experts)
            {
                Console.WriteLine(expert);
            }
        }

        static void Main(string[] args)
        {
            //initializing arguments
            List<Project> projects = new List<Project>();
            List<Expert> experts = new List<Expert>();
            int noOfProjects = 0;
            int noOfExperts = 0;
            int noOfFeatures = 0;

            //processing csv (assigning values to projects, experts, noOfProjects, noOfExperts, noOfFeatures)
            ProcessCSV(ref projects, ref experts, ref noOfProjects, ref noOfExperts, ref noOfFeatures);
            //after having above, we can go with implementation of the pseudocode

            List<int> sum = SumExperts(experts, noOfFeatures, noOfExperts);

            CalculateExpertsWeights(ref experts, sum); //must be executed each time when sum changes

            SortExperts(ref experts, sum);
            foreach(var feat in sum)
            {
                Console.Write(feat + " ");
            }
            Console.WriteLine();
            PrintExperts(experts);
            int indProj = 0;
            List<int> oldSum = new List<int>();
            
            while(oldSum != sum /*&& (experts are not finished || projects are not finished)*/)
            {
                //TODO
            }

        }

        public static void CalculateExpertsWeights(ref List<Expert> le, List<int> sum)
        {
            //finding sumWeight vector (weights of each feature)
            List<int> sumTemp = new List<int>(sum);
            List<int> sumWeight = new List<int>(sumTemp);
            int features = sumTemp.Count;
            int temp = 0;
            while (temp != features)
            {
                int max = -1;
                int maxind = -1;
                for (int i = 0; i < features; i++)
                {
                    if (sumTemp.ElementAt(i) > max)
                    {
                        max = sumTemp.ElementAt(i);
                        maxind = i;
                    }
                }
                sumTemp[maxind] = -99;
                sumWeight[maxind] = temp + 1;
                temp++;
            }

            //assigning weights to experts
            for(int j = 0; j < le.Count; j++)
            {
                int weight = 0;
                for(int y = 0; y < sum.Count; y++)
                {
                    weight += le[j].expertVector[y] * sumWeight[y];
                }
                le[j] = new Expert(le[j].expertVector, weight, -1);
            }
        }

        public static void SortExperts(ref List<Expert> le, List<int> expertSum)
        {
            var expertComparison = new ExpertComparison(expertSum);
            le.Sort(expertComparison);
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

        public static void ProcessCSV(ref List<Project> projects, ref List<Expert> experts, ref int noOfProjects, ref int noOfExperts, ref int noOfFeatures)
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
                    Project p = new Project(proj);
                    projects.Insert(ind, p);
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
                    Expert e = new Expert(exp, -1, -1);
                    experts.Insert(ind, e);
                    ind++;
                    temp--;
                }
            }
        }
        

    }
}
