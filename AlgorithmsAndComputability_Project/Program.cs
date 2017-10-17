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
            int[][] projects = null;
            int[][] experts = null;
            int noOfProjects = 0;
            int noOfExperts = 0;
            int noOfFeatures = 0;

            //processing csv
            ProcessCSV(ref projects, ref experts, ref noOfProjects, ref noOfExperts, ref noOfFeatures);

            //CHECKING: printing projects
            Console.Write(Environment.NewLine);
            Console.Write("Projects vectors: ");
            Console.Write(Environment.NewLine);
            for (int k = 0; k < noOfProjects; k++)
            {
                for (int j = 0; j < noOfFeatures; j++)
                {
                    Console.Write(projects[k][j] + " ");
                }
                Console.Write(Environment.NewLine);
            }

            //CHECKING: printing experts
            Console.Write(Environment.NewLine);
            Console.Write("Experts vectors: ");
            Console.Write(Environment.NewLine);
            for (int p = 0; p < noOfExperts; p++)
            {
                for (int j = 0; j < noOfFeatures; j++)
                {
                    Console.Write(experts[p][j] + " ");
                }
                Console.Write(Environment.NewLine);
            }
        }

        public static void ProcessCSV(ref int[][] projects, ref int[][] experts, ref int noOfProjects, ref int noOfExperts, ref int noOfFeatures)
        {
            string fileName = "INPUT.csv";
            string path = Path.Combine(Environment.CurrentDirectory, @"input\", fileName);

            using (StreamReader sr = new StreamReader(path))
            {
                //getting data from first line
                string currentLine = sr.ReadLine();
                char delimiter = ';';
                string[] values = currentLine.Split(delimiter);
                noOfProjects = Convert.ToInt32(values[0]);
                noOfExperts = Convert.ToInt32(values[1]);
                noOfFeatures = Convert.ToInt32(values[2]);

                //CHECKING: printing numbers of projects, experts, features
                Console.WriteLine("Number of projects: " + noOfProjects);
                Console.WriteLine("Number of experts: " + noOfExperts);
                Console.WriteLine("Number of features: " + noOfFeatures);

                //initializing projects and experts 2D arrays
                projects = new int[noOfProjects][];
                for (int size = 0; size < noOfProjects; size++)
                {
                    projects[size] = new int[noOfFeatures];
                }
                experts = new int[noOfExperts][];
                for (int size = 0; size < noOfExperts; size++)
                {
                    experts[size] = new int[noOfFeatures];
                }

                //reading projects
                int ind = 0;
                int temp = noOfProjects;
                while (temp != 0)
                {
                    currentLine = sr.ReadLine();
                    char delimiter_p = ';';
                    string[] values_p = currentLine.Split(delimiter_p);

                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        projects[ind][i] = Convert.ToInt32(values_p[i]);
                    }
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

                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        experts[ind][i] = Convert.ToInt32(values_p[i]);
                    }
                    ind++;
                    temp--;
                }
            }
        }
        
    }
}
