using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Assignment //assignment of all the experts to all the projects
    {
        public Expert exp;
        List<int> projectsSum;
        List<Assignment> la; //list of children of a node
        public Assignment parent;
        public Assignment(List<int> sum)
        {
            projectsSum = new List<int>(sum);
            la = new List<Assignment>();
        }

        public Assignment addExpert(Expert e, int indexFeature)
        {
            if(projectsSum[indexFeature] > 0)
            {
                e.featureUsed = indexFeature;
                Assignment a = new Assignment(projectsSum);
                a.exp = e;
                a.projectsSum[indexFeature]--;

                this.la.Add(a);
                a.parent = this;
                return a;
            }
            return null;
        }

        public Assignment OptimalPath()
        {
            int maxPathLength = 0;
            int pathLength = 0;

            Assignment ret = RecursiveLongestPath(ref maxPathLength, pathLength, this);

            return ret;
        }

        public Assignment RecursiveLongestPath(ref int maxPathLength, int pathLength, Assignment current)
        {
            pathLength++;
            Assignment a = null;
            if(current.la.Count == 0)
            {
                if (pathLength > maxPathLength)
                {
                    maxPathLength = pathLength;
                    return current;
                }
            }
            foreach(var assi in current.la)
            {
                Assignment ret = RecursiveLongestPath(ref maxPathLength, pathLength, assi);
                if (ret != null)
                    a = ret;
            }
            return a;
        }
    }
}
