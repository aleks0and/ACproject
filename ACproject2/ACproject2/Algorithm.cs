using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACproject2
{
    public class Algorithm
    {
        public List<Expert> Run(List<Project> projects, List<Expert> experts, int featureCount)
        {
            List<Expert> earlierUsed = new List<Expert>();
            List<int> projectsSum = Project.SumProjectsVector(projects, featureCount);
            Assignment a = new Assignment(projectsSum);
            int count = 0, max = 0;
            Assignment maxPathLeaf = null;
            RecursiveAssign(earlierUsed, experts, a, featureCount, count, ref max, ref maxPathLeaf);
            List<Expert> usedExperts = FindUsedExperts(maxPathLeaf);
            AssignExpertsToProjects(projects, usedExperts);

            return usedExperts;
        }

        public bool RecursiveAssign(List<Expert> earlierUsed, List<Expert> experts, Assignment assignment, int featureCount, int lengthCount, ref int maxLength, ref Assignment maxPathLeaf)
        {
            var usedExperts = new List<Expert>(earlierUsed);
            bool isLeaf = true, isMax = false;
            int maxIndex = -1;
            int assignIndex = 0;
            for (int i = 0; i < experts.Count; i++)
            {
                if(!earlierUsed.Contains(experts[i]))
                {
                    for(int k = 0; k < featureCount; k++)
                    {
                        Assignment a = assignment.addExpert(experts[i], k);
                        if(a != null)
                        {
                            usedExperts.Add(experts[i]);
                            var tempMax = RecursiveAssign(usedExperts, experts, a, featureCount, lengthCount + 1, ref maxLength, ref maxPathLeaf);
                            if(tempMax)
                            {
                                isMax = true;
                                if(maxIndex != -1)
                                    assignment.la[maxIndex] = null;
                                maxIndex = assignIndex;
                            }
                            else
                            {
                                assignment.la[assignIndex] = null;
                            }
                            isLeaf = false;
                            assignIndex++;
                        }
                    }
                }
            }
            if(isLeaf && lengthCount > maxLength)
            {
                maxLength = lengthCount;
                isMax = true;
                maxPathLeaf = assignment;
            }
            return isMax;
        }

        public List<Expert> FindUsedExperts(Assignment a)
        {
            List<Expert> expe = new List<Expert>();
            while(a.exp != null)
            {
                a.exp.featureUsed = a.featureUsed;
                expe.Add(a.exp);
                a = a.parent;
            }
            return expe;
        }

        public void AssignExpertsToProjects(List<Project> projects, List<Expert> usedExperts)
        {
            for(int i = usedExperts.Count - 1; i >= 0; i--)
            {
                for(int j = 0; j < projects.Count; j++)
                {
                    if(projects[j]._projectVector[usedExperts[i].featureUsed] > 0)
                    {
                        projects[j]._projectVector[usedExperts[i].featureUsed]--;
                        usedExperts[i].assignedProject = projects[j];
                        break;
                    }
                }
            }
        }
    }
}
