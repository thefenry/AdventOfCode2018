using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChallengesLibrary
{
    public class Day7Instructions
    {
        private readonly string[] _instructions;
        private List<char> _AllSteps;

        public Day7Instructions()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\instructionsInput.txt");
            _instructions = File.ReadAllLines(path);
        }

        public string GetInstructionsOrder()
        {
            Dictionary<char, List<char>> instructionSets = GetSimplifiedInstructionSets();
            List<char> instructionsOrder = new List<char>();
            int stepsIndex = 0;
            do
            {
                char stepToCheck = _AllSteps[stepsIndex];

                if (!instructionSets.ContainsKey(stepToCheck))
                {
                    instructionsOrder.Add(stepToCheck);

                    foreach (KeyValuePair<char, List<char>> instruction in instructionSets)
                    {
                        char key = instruction.Key;
                        List<char> dependencies = instruction.Value;

                        if (dependencies.Contains(stepToCheck))
                        {
                            dependencies.Remove(stepToCheck);
                        }
                    }

                    _AllSteps.Remove(stepToCheck);

                    stepsIndex = 0;
                }
                else
                {
                    if (!instructionSets[stepToCheck].Any())
                    {
                        instructionSets.Remove(stepToCheck);
                    }
                    else
                    {
                        stepsIndex++;
                    }
                }


            } while (stepsIndex < _AllSteps.Count);

            return string.Join(' ', instructionsOrder);
        }

        private Dictionary<char, List<char>> GetSimplifiedInstructionSets()
        {
            Dictionary<char, List<char>> stepsAndTheirRequirements = new Dictionary<char, List<char>>();
            _AllSteps = new List<char>();

            foreach (string instruction in _instructions)
            {
                string[] splitInstruction = instruction.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                char stepBefore = splitInstruction[7][0];
                char stepAfter = splitInstruction[1][0];

                if (stepsAndTheirRequirements.ContainsKey(stepBefore))
                {
                    if (!stepsAndTheirRequirements[stepBefore].Contains(stepAfter))
                    {
                        stepsAndTheirRequirements[stepBefore].Add(stepAfter);
                    }
                }
                else
                {
                    stepsAndTheirRequirements.Add(stepBefore, new List<char> { stepAfter });

                }

                GenerateAllStepList(stepAfter);
                GenerateAllStepList(stepBefore);

            }

            _AllSteps.Sort();
            return stepsAndTheirRequirements;
        }

        private void GenerateAllStepList(char step)
        {
            if (!_AllSteps.Contains(step))
            {
                _AllSteps.Add(step);
            }
        }
    }
}
