using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using day_two.Models;

namespace day_two.Services
{
    public class ChecksumService
    {
        public string[] GetInputValues()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\boxIds.txt");
            string[] inputs = File.ReadAllLines(path);

            return inputs;
        }

        public List<IdIntance> GetGroupings(string value)
        {
            return value.GroupBy(c => c).Where(c => c.Count() == 2 || c.Count() == 3).Select(c => new IdIntance { Character = c.Key, Count = c.Count() }).ToList();
        }

        public string GetMatchingIDsByOneOff(string[] listOfIds)
        {
            var matchingId = string.Empty;

            for (int i = 0; i < listOfIds.Length; i++)
            {
                var idToCheck = listOfIds[i];

                for (int j = 0; j < listOfIds.Length; j++)
                {
                    if (listOfIds[j] != idToCheck)
                    {
                        var comparatorString = listOfIds[j];
                        int numberOfMatchingCharacters = 0;
                        string matchingString = string.Empty;

                        for (int k = 0;  k < comparatorString.Length;  k++)
                        {
                            if (comparatorString[k] == idToCheck[k])
                            {
                                numberOfMatchingCharacters++;
                                matchingString += comparatorString[k];
                            }
                        }

                        if (numberOfMatchingCharacters == comparatorString.Length-1)
                        {
                            matchingId = matchingString;
                            break;
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(matchingId))
                {
                    break;
                }
            }

            return matchingId;
        }
    }
}
