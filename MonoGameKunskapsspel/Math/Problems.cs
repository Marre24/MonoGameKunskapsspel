using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class Problems
    {
        public Dictionary<string, List<string>> problemAndAnswers = new Dictionary<string, List<string>>();
        private const string problemsFileName = "Math/Problems.txt";
        private const string solutionFileName = "Math/Solutions.txt";
        public Problems()
        {
            CreateProblem();
        }

        private void CreateProblem()
        {
            if (File.ReadAllLines(problemsFileName).Length != File.ReadAllLines(solutionFileName).Length)
                return;


            for (int i = 0; i < File.ReadAllLines(problemsFileName).Length; i++)
            {
                problemAndAnswers.Add(File.ReadLines(problemsFileName).Skip(i).Take(1).First(),
                File.ReadLines(solutionFileName).Skip(i).Take(1).First().Split(';').ToList());
            }

            problemAndAnswers = Shuffle(problemAndAnswers);
        }

        public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            Random r = new Random();
            return source.OrderBy(x => r.Next())
               .ToDictionary(item => item.Key, item => item.Value);
        }

        public (string, List<string>) GetCurrentProblem()
        {
            return (problemAndAnswers.Keys.First() , problemAndAnswers.Values.First());
        }

        public void NextProblem()
        {
            problemAndAnswers.Remove(problemAndAnswers.Keys.First());
        }

    }
}
