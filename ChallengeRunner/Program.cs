using ChallengesLibrary;
using System;

namespace ChallengeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter which day your wish to run from 1-25");
            string day = Console.ReadLine();

            switch (day)
            {
                case "5":
                    RunDayFiveChallenges();
                    break;
                case "7":
                    RunDaySevenChallenges();
                    break;
                default:
                    break;
            }

            Console.ReadLine();
        }

        private static void RunDayFiveChallenges()
        {
            Day5Polymers polymerProblem = new Day5Polymers();

            //string cleanPolymerSegment = polymerProblem.ScanAndCleanPolymer();

            //Console.WriteLine(cleanPolymerSegment.Length);

            int polymerCount = polymerProblem.FindAndRemoveBreakingUnit();

            Console.WriteLine($"Smallest Polymer: {polymerCount}");
        }

        private static void RunDaySevenChallenges()
        {
            Day7Instructions day7 = new Day7Instructions();
            Console.WriteLine($"steps order: {day7.GetInstructionsOrder()}");
        }
    }
}
