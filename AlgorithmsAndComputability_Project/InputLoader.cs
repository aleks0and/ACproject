using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace AlgorithmsAndComputability_Project
{
    class InputLoader
    {
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
                    Project p = new Project(proj, ind);
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
                    Expert e = new Expert(exp, -1);
                    experts.Insert(ind, e);
                    ind++;
                    temp--;
                }
            }
        }
        public static void ProcessInput(ref List<Project> projects, ref List<Expert> experts, ref int noOfProjects, ref int noOfExperts, ref int noOfFeatures, char delimiter, string path)
        {
            //string fileName = "inputtext.txt";
            //string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Specification\input", fileName);
            using (StreamReader sr = new StreamReader(path))
            {
                //getting data from first line
                string currentLine = sr.ReadLine();
                //char delimiter = ',';
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
                    char delimiter_p = ',';
                    string[] values_p = currentLine.Split(delimiter_p);

                    List<int> proj = new List<int>();
                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        proj.Insert(i, Convert.ToInt32(values_p[i]));
                    }
                    Project p = new Project(proj, ind);
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
                    char delimiter_p = ',';
                    string[] values_p = currentLine.Split(delimiter_p);
                    List<int> exp = new List<int>();
                    for (int i = 0; i < noOfFeatures; i++)
                    {
                        exp.Insert(i, Convert.ToInt32(values_p[i]));
                    }
                    Expert e = new Expert(exp, -1);
                    experts.Insert(ind, e);
                    ind++;
                    temp--;
                }
            }
        }
    }
}
