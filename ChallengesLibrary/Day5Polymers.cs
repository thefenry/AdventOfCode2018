using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ChallengesLibrary
{
    public class Day5Polymers
    {
        private string _polymerSegments;

        public Day5Polymers(string input = null)
        {
            if (input == null || input.Length == 0)
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\polymerInput.txt");
                input = File.ReadAllLines(path)[0];
            }

            _polymerSegments = input;
        }

        public string ScanAndCleanPolymer()
        {
            int stringIndex = 0;
            do
            {
                char currentElement = _polymerSegments[stringIndex];
                char nextElement = _polymerSegments[stringIndex + 1];

                if (currentElement != nextElement && char.ToLowerInvariant(currentElement) == char.ToLowerInvariant(nextElement))
                {
                    _polymerSegments = _polymerSegments.Remove(stringIndex, 2);
                    stringIndex--;

                    stringIndex = Math.Max(stringIndex, 0); // This avoid negatives in the index
                }
                else
                {
                    stringIndex++;
                }

            } while (stringIndex < _polymerSegments.Length - 1);

            return _polymerSegments;
        }

        public int FindAndRemoveBreakingUnit()
        {
            List<char> listOfAvailableUnits = _polymerSegments.ToLowerInvariant().Distinct().ToList();
            string originalPolymer = _polymerSegments;
            int smallestPolymerCombination = _polymerSegments.Length;
            string smallestPolymerSegment = string.Empty;

            foreach (char unit in listOfAvailableUnits)
            {
                var unitsToRemove = new char[] { unit, char.ToUpperInvariant(unit) };

                _polymerSegments = _polymerSegments.Replace(unit.ToString(), "", StringComparison.InvariantCultureIgnoreCase);
                //_polymerSegments = _polymerSegments.TrimStart(unitsToRemove);

                string cleanedPolymer = this.ScanAndCleanPolymer();

                if (cleanedPolymer.Length < smallestPolymerCombination)
                {
                    smallestPolymerCombination = cleanedPolymer.Length;
                    smallestPolymerSegment = cleanedPolymer;
                }

                _polymerSegments = originalPolymer;
            }


            return smallestPolymerCombination;
        }
    }
}
