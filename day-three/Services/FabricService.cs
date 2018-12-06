using day_three.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace day_three.Services
{
    class FabricService
    {
        string[,] _fabricTemplateArray = new string[1000, 1000];

        List<string> _inputs;
        private List<FabricMeasurements> _fabricMeasurementsList;
        private int _overlappingSquares = 0;
        private List<int> _overlappingIds = new List<int>();

        public FabricService()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Inputs\fabricDataInputs.txt");
            _inputs = File.ReadAllLines(path).ToList();

            GenerateFabricTemplate();
            ConvertInputs();
        }

        private void GenerateFabricTemplate()
        {
            for (int r = 0; r < _fabricTemplateArray.GetLength(0); r++)
            {
                for (int c = 0; c < _fabricTemplateArray.GetLength(1); c++)
                {
                    _fabricTemplateArray[r, c] = ".";
                }
            }
        }

        private void ConvertInputs()
        {
            _fabricMeasurementsList = new List<FabricMeasurements>();

            for (int i = 0; i < _inputs.Count; i++)
            {
                string[] splitInput = _inputs[i].Split(new char[] { ' ', ',', '@', 'x', '#', ':' }, StringSplitOptions.RemoveEmptyEntries);

                FabricMeasurements fabricMeasurements = new FabricMeasurements
                {
                    Id = int.Parse(splitInput[0]),
                    LeftEdgeDistance = int.Parse(splitInput[1]),
                    TopEdgeDistance = int.Parse(splitInput[2]),
                    Width = int.Parse(splitInput[3]),
                    Height = int.Parse(splitInput[4]),
                };

                _fabricMeasurementsList.Add(fabricMeasurements);
            }
        }

        public string[,] StartMeasuringTheFabric()
        {
            foreach (FabricMeasurements fabric in _fabricMeasurementsList)
            {
                int topEdge = fabric.TopEdgeDistance;
                int leftEdge = fabric.LeftEdgeDistance;

                for (int r = 0; r < fabric.Height; r++)
                {
                    for (int c = 0; c < fabric.Width; c++)
                    {
                        if (_fabricTemplateArray[topEdge + r, leftEdge + c] == ".")
                        {
                            _fabricTemplateArray[topEdge + r, leftEdge + c] = fabric.Id.ToString();                            
                        }
                        else
                        {
                            if (_fabricTemplateArray[topEdge + r, leftEdge + c] != "x")
                            {                                
                                if (!_overlappingIds.Contains(int.Parse(_fabricTemplateArray[topEdge + r, leftEdge + c])))
                                {
                                    _overlappingIds.Add(int.Parse(_fabricTemplateArray[topEdge + r, leftEdge + c]));
                                }

                                _overlappingSquares++;
                                _fabricTemplateArray[topEdge + r, leftEdge + c] = "x";
                            }

                            if (!_overlappingIds.Contains(fabric.Id))
                            {
                                _overlappingIds.Add(fabric.Id);
                            }
                        }
                    }
                }
            }

            return _fabricTemplateArray;
        }

        public int GetSquareInchCount()
        {
            return _overlappingSquares;
        }

        public int GetSoloClaim()
        {
            var soloItem = _fabricMeasurementsList.Where(x => !_overlappingIds.Contains(x.Id)).Select(x => x.Id).ToList();
            if (soloItem.Count > 1)
            {
                throw new Exception("Too many Items");
            }
            else
            {
                return soloItem.FirstOrDefault();
            }
        }
    }
}
