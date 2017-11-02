using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Project
    {
        public List<int> _projectVector;
        public int _id;

        public Project(List<int> p, int id)
        {
            _projectVector = p;
            _id = id;
        }

        public Project(Project proj)
        {
            _id = proj.Id;
            _projectVector = new List<int>(proj._projectVector);
        }

        public int Id { get { return _id; } set { _id = value; } }


        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < _projectVector.Count; i++)
            {
                s += _projectVector[i] + ", ";
            }
            s += "] ";
            return s;
        }

        public static List<int> SumProjectsVector(List<Project> p, int featureCount)
        {
            List<int> sum = new List<int>();

            for (int i = 0; i < featureCount; i++)
            {
                sum.Add(0);
                for (int j = 0; j < p.Count; j++)
                {
                    sum[i] += p[j]._projectVector[i];
                }
            }
            return sum;
        }
        //suma moze sie *****ć przy przekroczeniu limitu inta : (
        public static int SumProjects(List<Project> p)
        {
            int sum = 0;
            for (int i = 0; i < p.Count; i++)
            {
                for (int j = 0; j < p[i]._projectVector.Count; j++)
                {
                    sum += p[i]._projectVector[j];
                }
            }
            return sum;
        }
    }
}
