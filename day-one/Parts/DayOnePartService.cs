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
    }
}
