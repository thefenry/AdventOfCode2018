using System;
using day_one.Parts;

namespace day_one
{
    class Program
    {
        private static DayOnePartService _service;

        static void Main(string[] args)
        {
            _service = new DayOnePartService();
            
            DisplayFinalFrequency();
        }

        private static void DisplayFinalFrequency()
        {

            int finalFrequency = _service.GetFinalFrequency();

            Console.WriteLine($"Final Frequency: {finalFrequency}");

            Console.ReadLine();
        }
    }
}
