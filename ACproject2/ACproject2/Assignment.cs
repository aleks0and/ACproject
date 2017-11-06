using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Assignment
    {
        public Expert exp;
        List<int> projectsSum;
        public List<Assignment> la; //list of children of a node
        public Assignment parent;
        public int featureUsed;
        public Assignment(List<int> sum)
        {
            projectsSum = new List<int>(sum);
            la = new List<Assignment>();
        }

        public Assignment addExpert(Expert e, int indexFeature)
        {
            if(projectsSum[indexFeature] > 0 && e.expertVector[indexFeature] > 0)
            {
                Assignment a = new Assignment(projectsSum);
                a.featureUsed = indexFeature;
                a.exp = e;
                a.projectsSum[indexFeature]--;

                this.la.Add(a);
                a.parent = this;
                return a;
            }
            return null;
        }

    }
}
