using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGameKunskapsspel
{
    public class Problems
    {
        public Dictionary<string, (int,List<string>)> problemAndAnswers = new();
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
                problemAndAnswers.Add(File.ReadLines(problemsFileName).Skip(i).Take(1).First().Split(';')[0],
                (int.Parse(File.ReadLines(problemsFileName).Skip(i).Take(1).First().Split(';')[1]),
                File.ReadLines(solutionFileName).Skip(i).Take(1).First().Split(';').ToList()));
            }

        }

        public (string, int, List<string>) GetCurrentProblem()
        {
            var problem = (problemAndAnswers.Keys.First(), problemAndAnswers.Values.First().Item1, problemAndAnswers.Values.First().Item2);
            NextProblem();
            return problem;
        }

        public void NextProblem()
        {
            problemAndAnswers.Remove(problemAndAnswers.Keys.First());
        }

        public (string, int, List<string>) GetLastProblem()
        {
            return (problemAndAnswers.Keys.Last(), problemAndAnswers.Values.Last().Item1, problemAndAnswers.Values.Last().Item2);
        }
    }
}
