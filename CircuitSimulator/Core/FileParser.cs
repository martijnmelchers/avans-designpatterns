using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Core
{
    public class FileParser
    {
        public static List<string> ReadFile(string name)
        {
            return File.ReadAllLines($"Circuits/{name}.txt").ToList();
        }
        
        public static (Dictionary<string, string> nodes, Dictionary<string, string[]> edges) ParseFile(List<string> lines)
        {
            var invalidLines = lines.Where(StringExtensions.InvalidEntry).ToList();

            // If there are any invalid lines we can't proceed!
            if (invalidLines.Any())
                throw new SyntaxErrorException($"Invalid syntax at following lines:\n{string.Join("\n", invalidLines)}");

            // Filter out the lines that start with an #, those are comments.
            lines = lines.Where(x => !x.StartsWith("#")).ToList();

            // Get all nodes until an empty line is found
            var nodes = lines.TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();
            // Get all edges after the empty line
            var edges = lines.SkipWhile(x => !string.IsNullOrEmpty(x)).Skip(1).ToList();
            
            
            // Parse each node and fill a dictionary<name, type>
            var nodeDictionary = nodes
                .Select(input => input.ParseLine())
                .ToDictionary(parts => parts[0], parts => parts[1]);
            
            // Parse each edge and fill a dictionary<name, edges[]>
            var edgeDictionary = edges
                .Select(edge => edge.ParseLine())
                .ToDictionary(parts => parts[0], parts => parts[1].Split(","));
                
            return (nodeDictionary, edgeDictionary);
        }

       
    }
    
    
}