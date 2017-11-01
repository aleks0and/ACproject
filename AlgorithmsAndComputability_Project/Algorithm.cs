using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    public class Algorithm
    {
        public List<Expert> Run(List<Project> projects, List<Expert> experts, int featureCount)
        {
            List<int> sum = Expert.SumExperts(experts, featureCount);
            Printer.Print(sum, "Sum of experts' features:");
            Expert.CalculateExpertsWeights(experts, sum); //must be executed each time when sum changes
            Expert.SortExperts(experts, sum);
            Printer.PrintVector(experts, "Experts after sorting:");
            return Assignment(projects, experts, featureCount, sum);
        }

        private List<Expert> Assignment(List<Project> projects, List<Expert> experts, int featureCount, List<int> sum)
        {
            int indProj = 0;
            var oldSum = new List<int>();
            int sumProjects = Project.SumProjects(projects);
            var usedExperts = new List<Expert>();
            int[] usedFeatures = new int[featureCount];
            int oldProjects = 0;
            bool sumChanged = true;
            var projectSum = Project.SumProjectsVector(projects, featureCount);
            while ((sumChanged = AreExpertsAssignable(projectSum, sum)) && (experts.Count > 0 || sumProjects > 0))
            {
                SetUsedFeatures(projectSum, sum, usedFeatures);
                oldProjects = sumProjects;
                int diff = 0;
                int indSmall = GetSmallestIndex(sum, usedFeatures);
                if (indSmall == -1)
                {
                    break;
                }
                if (projects[indProj]._projectVector[indSmall] > 0)
                {
                    if (projects[indProj]._projectVector[indSmall] < sum[indSmall])
                    {
                        diff = projects[indProj]._projectVector[indSmall];
                        sumProjects -= projects[indProj]._projectVector[indSmall];
                        projects[indProj]._projectVector[indSmall] = 0;
                        projectSum[indSmall] -= diff;
                    }
                    else
                    {
                        diff = sum[indSmall];
                        projects[indProj]._projectVector[indSmall] -= sum[indSmall];
                        sumProjects -= sum[indSmall];
                        projectSum[indSmall] -= diff;
                    }
                }
                Expert.RemoveExperts(experts, usedExperts, diff, projects[indProj], indSmall);
                Expert.RemoveExpertsFromSum(usedExperts, sum, diff);
                if (indProj == projects.Count - 1)
                {
                    indProj = 0;
                    if (sumProjects == oldProjects)
                    {
                        sumChanged = false;
                    }
                }
                else
                {
                    indProj++;
                }
            }
            return usedExperts;
        }
        private void SetUsedFeatures(List<int> sumProjects, List<int> sumExperts, int[] usedFeatures)
        {
            for (int i = 0; i < sumProjects.Count; i++)
            {
                if (sumProjects[i] == 0 || sumExperts[i] == 0)
                {
                    usedFeatures[i] = 1;
                }
            }
        }
        private bool AreExpertsAssignable(List<int> sumProjects, List<int> sumExperts)
        {
            for (int i = 0; i < sumProjects.Count; i++)
            {
                if (sumProjects[i] > 0 && sumExperts[i] > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private int GetSmallestIndex(List<int> sum, int[] usedFeatures)
        {
            int min = int.MaxValue, index = -1;
            for (int i = 0; i < sum.Count; i++)
            {
                if (sum[i] > 0 && usedFeatures[i] != 1 && sum[i] < min)
                {
                    min = sum[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
