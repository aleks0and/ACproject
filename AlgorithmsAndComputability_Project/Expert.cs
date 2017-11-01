using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    public class Expert
    {
        public List<int> expertVector;
        public int weight;
        public Project assignedProject;
        public int featureUsed;

        public Expert(List<int> ev, int w)
        {
            expertVector = ev;
            weight = w;     
        }

        public override string ToString()
        {
            string s = "[";
            for (int i = 0; i < expertVector.Count; i++)
            {
                s += expertVector[i] + ", ";
            }
            s += "] weight: " + weight;
            if (assignedProject != null)
            {
                s += " project assigned: " + assignedProject.Id + " feature used: " + (featureUsed);
            }
            return s;
        }

        public static void SortExperts(List<Expert> le, List<int> expertSum)
        {
            var expertComparison = new ExpertComparison(expertSum);
            le.Sort(expertComparison);
        }

        public static List<int> SumExperts(List<Expert> le, int noOfFeatures)
        {
            List<int> sum = new List<int>();

            for (int i = 0; i < noOfFeatures; i++)
            {
                int sumField = 0;
                for (int j = 0; j < le.Count; j++)
                {
                    sumField += le[j].expertVector[i];
                }
                sum.Add(sumField);
            }

            return sum;
        }
        public static void CalculateExpertsWeights(List<Expert> le, List<int> sum)
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
            for (int j = 0; j < le.Count; j++)
            {
                int weight = 0;
                for (int y = 0; y < sum.Count; y++)
                {
                    weight += le[j].expertVector[y] * sumWeight[y];
                }
                le[j] = new Expert(le[j].expertVector, weight);
            }
        }
        public static void RemoveExpertsFromSum(List<Expert> experts, List<int> sum, int diff)
        {
            for (int i = experts.Count - 1; i > experts.Count - diff - 1; i--)
            {
                for (int j = 0; j < sum.Count; j++)
                {
                    sum[j] -= experts[i].expertVector[j];
                }
            }
        }
        public static void RemoveExperts(List<Expert> experts, List<Expert> usedExperts, int diff, Project project, int featureUsed)
        {

            int countDown = diff;
            List<int> expertsToRemove = new List<int>();
            int j = 0;
            while (countDown != 0)
            {
                if (experts[j].expertVector[featureUsed] == 1)
                {
                    expertsToRemove.Add(j);
                    countDown--;
                }
                j++;
            }
            for (int i = expertsToRemove.Count - 1; i >= 0; i--)
            {
                usedExperts.Add(experts[expertsToRemove[i]]);
                experts.RemoveAt(expertsToRemove[i]);

            }
            for (int i = usedExperts.Count - 1; i > usedExperts.Count - diff - 1; i--)
            {
                usedExperts[i].assignedProject = project;
                usedExperts[i].featureUsed = featureUsed;
            }

        }

    }
}
