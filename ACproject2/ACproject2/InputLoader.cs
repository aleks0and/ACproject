using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACproject2
{
    public class InputLoader
    {
        private char _separator;
        private string _path;
        public InputLoader(char separator, string path)
        {
            _separator = separator;
            _path = path;
        }
        public void ProcessInput(List<Project> projects, List<Expert> experts, ref int featureCount, string filename)
        {
            string fullPath = Path.Combine(Environment.CurrentDirectory, _path, filename);
            using (StreamReader sr = new StreamReader(fullPath))
            {
                //getting data from first line
                string currentLine = sr.ReadLine();
                string[] values = currentLine.Split(_separator);
                int projectCount = Convert.ToInt32(values[0]);
                int expertCount = Convert.ToInt32(values[1]);
                featureCount = Convert.ToInt32(values[2]);
                //reading projects
                int ind = 0;
                int temp = projectCount;
                while (temp != 0)
                {
                    currentLine = sr.ReadLine();
                    char delimiter_p = _separator;
                    string[] values_p = currentLine.Split(delimiter_p);

                    List<int> proj = new List<int>();
                    for (int i = 0; i < featureCount; i++)
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
                temp = expertCount;
                while (temp != 0)
                {
                    currentLine = sr.ReadLine();
                    char delimiter_p = _separator;
                    string[] values_p = currentLine.Split(delimiter_p);
                    List<int> exp = new List<int>();
                    for (int i = 0; i < featureCount; i++)
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
