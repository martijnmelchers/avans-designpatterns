using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core
{
    public class FileReader
    {
        public static List<string> ReadFile(string name)
        {
            return File.ReadAllLines(name).ToList();
        }

        public static List<string> GetFiles(string directory)
        {
            return Directory
                .GetFiles(directory)
                .Select(x => x.Replace($"{directory}\\", ""))
                .ToList();
        }
    }
}