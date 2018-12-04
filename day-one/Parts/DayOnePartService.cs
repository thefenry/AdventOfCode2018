using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace day_one.Parts
{
    public class DayOnePartService
    {
        public int GetFinalFrequency()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\FrequencyInputs.txt");
            string[] inputs = File.ReadAllLines(path);

            int valueFrequency = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                var validNumber = int.TryParse(inputs[i], out int value);
                if (validNumber)
                {
                    valueFrequency += value;
                }
            }

            return valueFrequency;
        }

        public int GetFirstFrequencyRepeat()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\FrequencyInputs.txt");
            string[] inputs = File.ReadAllLines(path);

            //string[] inputs = new string[] { "+1", " -1", " +4", " -2", " -4" };
            int valueFrequency = 0;
            bool foundRepeatedFrequency = false;
            List<int> frequenciesHistories = new List<int> { valueFrequency };

            int listIndex = 0;
            do
            {
                var validNumber = int.TryParse(inputs[listIndex], out int value);
                if (validNumber)
                {
                    valueFrequency += value;

                    if (frequenciesHistories.Contains(valueFrequency))
                    {
                        foundRepeatedFrequency = true;
                    }

                    frequenciesHistories.Add(valueFrequency);
                }

                if (inputs.Length - 1 <= listIndex)
                {
                    listIndex = 0;
                }
                else
                {
                    listIndex++;
                }
            } while (!foundRepeatedFrequency);


            return valueFrequency;
        }
    }
}
