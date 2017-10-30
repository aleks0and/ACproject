using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsAndComputability_Project
{
    public class ExpertComparison : IComparer<Expert>
    {
        private List<int> _expertSum;
        public ExpertComparison(List<int> expertSum)
        {
            _expertSum = expertSum;
            _expertSum = _expertSum
                .Select((e, i) => new KeyValuePair<int, int>(e, i))
                .OrderBy(e => e.Key)
                .Select(e => e.Value)
                .ToList();
        }
        public int Compare(Expert x, Expert y)
        {
            for (int i = 0; i < _expertSum.Count; i++)
            {
                if (x.expertVector[_expertSum[i]] == 1 && y.expertVector[_expertSum[i]] == 0)
                {
                    return -1;
                }
                else if (x.expertVector[_expertSum[i]] == 0 && y.expertVector[_expertSum[i]] == 1)
                {
                    return 1;
                }
            }
            return y.weight - x.weight;
        }
    }
}
