using day_three.Services;
using System;

namespace day_three
{
    class Program
    {
        private static FabricService _fabricService;

        static void Main(string[] args)
        {
            _fabricService = new FabricService();

            MeasureFabric();
            GetSoloCloth();

            Console.ReadLine();
        }
               
        private static void MeasureFabric()
        {
            var fabricArray = _fabricService.StartMeasuringTheFabric();

            //for (int r = 0; r < fabricArray.GetLength(0); r++)
            //{
            //    for (int c = 0; c < fabricArray.GetLength(1); c++)
            //    {
            //        if (c < fabricArray.GetLength(1) - 1)
            //        {
            //            Console.Write(fabricArray[r, c]);
            //        }
            //        else
            //        {
            //            Console.WriteLine(fabricArray[r, c]);
            //        }
            //    }
            //}


            Console.WriteLine($"There are {_fabricService.GetSquareInchCount()} squares");
        }

        private static void GetSoloCloth()
        {
            Console.WriteLine($"This is the id that does not overlap: {_fabricService.GetSoloClaim()}");
        }
    }
}
