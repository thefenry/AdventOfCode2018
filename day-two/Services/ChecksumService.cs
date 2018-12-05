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
            return value.GroupBy(c => c).Where(c => c.Count() == 2 || c.Count() == 3).Select(c => new IdIntance { Character = c.Key, Count = c.Count() }).ToList();//.ToDictionary(c => c.Char, c => c.Count);
        }
    }
}
