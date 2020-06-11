using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class FileParser
    {
        public List<string> ReadFile(string name)
        {
            return File.ReadAllLines($"Circuits/{name}.txt").ToList();
        }
        
        public void ParseFile(List<string> lines)
        {
        }
    }
}