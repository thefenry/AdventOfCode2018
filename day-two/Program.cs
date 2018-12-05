using System;
using System.Collections.Generic;
using System.Linq;
using day_two.Models;
using day_two.Services;

namespace day_two
{
    class Program
    {
        static void Main(string[] args)
        {
            ChecksumService csService = new ChecksumService();

            //ShowChecksumOfInstances(csService);

            GetOneOffMatchingId(csService);
        }

        private static void GetOneOffMatchingId(ChecksumService csService)
        {
            string results = csService.GetMatchingIDsByOneOff(csService.GetInputValues());
            Console.WriteLine(results);
            Console.ReadLine();
        }

        private static void ShowChecksumOfInstances(ChecksumService csService)
        {
            string[] valuesList = csService.GetInputValues();

            int twoOfSameValueInstance = 0;
            int threeOfSameValueInstance = 0;

            foreach (string codeValue in valuesList)
            {
                List<IdIntance> valueList = csService.GetGroupings(codeValue);

                if (valueList.Any(c => c.Count == 2))
                {
                    twoOfSameValueInstance++;
                }

                if (valueList.Any(c => c.Count == 3))
                {
                    threeOfSameValueInstance++;
                }
            }

            Console.WriteLine($"The checksum of the ids is : {twoOfSameValueInstance * threeOfSameValueInstance}");

            Console.ReadLine();
        }
    }
}
