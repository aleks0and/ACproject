using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Expert
    {
        public List<int> expertVector;
        public Project assignedProject;
        public int featureUsed;

        public Expert(List<int> ev, int w)
        {
            expertVector = ev;
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < expertVector.Count; i++)
            {
                s += expertVector[i] + ", ";
            }
            s += "] ";
            if (assignedProject != null)
            {
                s += " project assigned: " + assignedProject.Id + " feature used: " + (featureUsed);
            }
            return s;
        }
    }
}
