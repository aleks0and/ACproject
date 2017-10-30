using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    class Program
    {
        public static void PrintVector<T>(List<T> vectors)
        {
            int no = 0;
            foreach(var vector in vectors)
            {
                Console.WriteLine(vector);
                no++;
            }
            Console.WriteLine("Number of printed lines: " + no);
        }

        public static int GetSmallestIndex(List<int> sum, int[] usedFeatures)
        {
            int min = int.MaxValue, index = -1;
            for(int i = 0; i < sum.Count; i++)
            {
                if(sum[i] > 0 && usedFeatures[i] != 1 && sum[i] < min)
                {
                    min = sum[i];
                    index = i;
                }
            }
            return index;
        }

        public static bool AreExpertsAssignable(List<int> sumProjects, List<int> sumExperts)
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

        public static void SetUsedFeatures(List<int> sumProjects, List<int> sumExperts, int[] usedFeatures)
        {
            for (int i = 0; i < sumProjects.Count; i++)
            {
                if (sumProjects[i] == 0 || sumExperts[i] == 0)
                {
                    usedFeatures[i] = 1;
                }
            }
        }

        static void Main(string[] args)
        {
            //initializing arguments
            List<Project> projectsCopy = new List<Project>();
            List<Project> projects = new List<Project>();
            List<Expert> experts = new List<Expert>();
            int noOfProjects = 0;
            int noOfExperts = 0;
            int noOfFeatures = 0;
            //processing csv (assigning values to projects, experts, noOfProjects, noOfExperts, noOfFeatures)
            InputLoader.ProcessInput(ref projects, ref experts, ref noOfProjects, ref noOfExperts, ref noOfFeatures, ',', Path.Combine(Environment.CurrentDirectory, @"..\..\..\Specification\input", "inputtext.txt"));

            //after having above, we can go with implementation of the pseudocode
            projectsCopy = projects.ConvertAll(p => new Project(p));
            List<int> sum = Expert.SumExperts(experts, noOfFeatures);
            Expert.CalculateExpertsWeights(ref experts, sum); //must be executed each time when sum changes
            Expert.SortExperts(ref experts, sum);
            Console.WriteLine("Projects vectors before assignment of experts: ");
            PrintVector(projects);
            Console.WriteLine();
            Console.WriteLine("Sum of all experts' features:");
            foreach (var feat in sum)
            {
                Console.Write(feat + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Sorted experts vectors with their weights:");
            PrintVector(experts);
            int indProj = 0;
            List<int> oldSum = new List<int>();
            int sumProjects = Project.SumProjects(projects);
            List<Expert> usedExperts = new List<Expert>();
            int[] usedFeatures = new int[noOfFeatures];
            int oldProjects = 0;
            bool sumChanged = true;
            var projectSum = Project.SumProjectsVector(projects, noOfFeatures);
            while ((sumChanged = AreExpertsAssignable(projectSum, sum)) && (experts.Count > 0 || sumProjects > 0))
            {
                SetUsedFeatures(projectSum, sum, usedFeatures);
                oldProjects = sumProjects;
                int diff = 0;
                //condition is wrong
                int indSmall = GetSmallestIndex(sum, usedFeatures);
                if(indSmall == -1)
                {
                    break;
                }
                if(projects[indProj]._projectVector[indSmall] > 0)
                {
                    if(projects[indProj]._projectVector[indSmall] < sum[indSmall])
                    {
                        diff = projects[indProj]._projectVector[indSmall];
                        //sum[indSmall] -= projects[indProj]._projectVector[indSmall];
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
                        //sum[indSmall] = 0;
                    }
                }
                Expert.RemoveExperts(experts, usedExperts, diff, projects[indProj], indSmall);
                Expert.RemoveExpertsFromSum(usedExperts, sum, diff);
                if(indProj == projects.Count - 1)
                {
                    indProj = 0;
                    if(sumProjects == oldProjects)
                    {
                        sumChanged = false;
                    }
                }
                else
                {
                    indProj++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Projects vectors after assignment of experts:");
            PrintVector(projects);
            Console.WriteLine();
            Console.WriteLine("Used experts vectors:");
            PrintVector(usedExperts);
            Console.WriteLine();
            //PrintVector(projectsCopy);
            Console.ReadLine();
        }
    }
}
