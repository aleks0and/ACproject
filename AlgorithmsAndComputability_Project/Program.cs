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
        static void Main(string[] args)
        {
            //initializing arguments
            List<List<int>> projects = new List<List<int>>();
            List<List<int>> experts = new List<List<int>>();
            int noOfProjects = 0;
            int noOfExperts = 0;
            int noOfFeatures = 0;

            //processing csv
            ProcessCSV(ref projects, ref experts, ref noOfProjects, ref noOfExperts, ref noOfFeatures);

            ////CHECKING: printing numbers of projects, experts, features
            //Console.WriteLine("Number of projects: " + noOfProjects);
            //Console.WriteLine("Number of experts: " + noOfExperts);
            //Console.WriteLine("Number of features: " + noOfFeatures);

            ////CHECKING: printing projects
            //Console.Write(Environment.NewLine);
            //Console.Write("Projects vectors: ");
            //Console.Write(Environment.NewLine);
            //for (int k = 0; k < noOfProjects; k++)
            //{
            //    for (int j = 0; j < noOfFeatures; j++)
            //    {
            //        Console.Write(projects.ElementAt(k).ElementAt(j) + " ");
            //    }
            //    Console.Write(Environment.NewLine);
            //}

            ////CHECKING: printing experts
            //Console.Write(Environment.NewLine);
            //Console.Write("Experts vectors: ");
            //Console.Write(Environment.NewLine);
            //for (int p = 0; p < noOfExperts; p++)
            //{
            //    for (int j = 0; j < noOfFeatures; j++)
            //    {
            //        Console.Write(experts.ElementAt(p).ElementAt(j) + " ");
            //    }
            //    Console.Write(Environment.NewLine);
            //}
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
