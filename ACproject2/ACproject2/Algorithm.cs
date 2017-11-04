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

            RecursiveAssign(earlierUsed, experts, a, featureCount);
            var path = a.OptimalPath();
            List<Expert> usedExperts = FindUsedExperts(path);
            AssignExpertsToProjects(projects, usedExperts);

            return usedExperts;
        }

        public void RecursiveAssign(List<Expert> earlierUsed, List <Expert> experts, Assignment assignment, int featureCount)
        {
            var usedExperts = new List<Expert>(earlierUsed);
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
                            RecursiveAssign(usedExperts, experts, a, featureCount);
                        }
                    }
                }
            }
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
