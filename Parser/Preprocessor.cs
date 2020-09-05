namespace score.Parser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ProcessScore
    {
        public List<string> Score { get; private set; }

        public ProcessScore(string fileName)
        {
            Score = new List<string>();

            ProcessRepeats(fileName);
        }

        private string CleanLine(string line)
        {
            var s = line;

            s = s.Replace("|", " ");
             s = s.Replace(">", " ");

            s = s.Trim();

            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }

            return s;
        }

        private void ProcessRepeats(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName, Encoding.UTF8);

            var queue = new Queue<string>();

            bool silent = false;

            foreach (string line in lines)
            {
                if (line.Trim().StartsWith(";") | string.IsNullOrWhiteSpace(line)) { continue; }

                if (line.Trim().StartsWith("/*"))
                {
                    silent = true;
                    continue;
                }

                if (line.Trim().StartsWith("*/"))
                {
                    silent = false;
                    continue;
                }

                if (silent)
                {
                    continue;
                }

                var s = CleanLine(line);

                queue.Enqueue(s);
            }

            Score.AddRange(queue);

        }

    }
}