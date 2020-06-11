using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core.Nodes;
using Core.Nodes.Strategies;

namespace Core
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var nodeFactory = new NodeFactory();

            nodeFactory
                .RegisterStrategy<InputStrategy>("input_high", "input_low")
                .RegisterStrategy<ProbeStrategy>("probe")
                .RegisterStrategy<NotStrategy>("not")
                .RegisterStrategy<AndStrategy>("and")
                .RegisterStrategy<OrStrategy>("or")
                .RegisterStrategy<NorStrategy>("nor")
                .RegisterStrategy<NandStrategy>("nand")
                .RegisterStrategy<XorStrategy>("xor");

            var parser = new FileParser();

            var lines = parser.ReadFile("Circuit3_Encoder");

            lines = lines.Where(x => !x.StartsWith("#")).ToList();

            var nodes = new List<Node>();

            var inputs = lines.TakeWhile(x => !string.IsNullOrEmpty(x)).ToList();
            var edges = lines.SkipWhile(x => !string.IsNullOrEmpty(x)).Skip(1).ToList();

            foreach (var s in inputs)
            {
                var parts = s
                    .ToLower()
                    .Replace("\t", "")
                    .Replace(" ", "")
                    .Replace(";", "")
                    .Split(":");


                var name = parts[0];
                var type = parts[1];

                var node = nodeFactory.CreateNode(type, name);

                if (node == null)
                    throw new Exception("An error occured while parsing the file!");

                if (type == "input_low" || type == "input_high")
                    node.SetOutput(type == "input_low" ? NodeOutput.Off : NodeOutput.On);

                nodes.Add(node);
            }

            foreach (var s in edges)
            {
                var parts = s
                    .ToLower()
                    .Replace("\t", "")
                    .Replace(" ", "")
                    .Replace(";", "")
                    .Split(":");

                var name = parts[0];
                var edgePoints = parts[1].Split(",");

                var currNode = nodes.FirstOrDefault(x => x.Name == name);

                if (currNode == null)
                    throw new Exception("An error occured while parsing the file!");

                foreach (var edgePoint in edgePoints)
                {
                    var node = nodes.FirstOrDefault(x => x.Name == edgePoint);

                    if (node == null)
                        throw new Exception("An error occured while parsing the file!");

                    currNode.AddOutput(node);
                }
            }
            
            var a = new ConnectionValidatorVisitor();
            
            foreach (var node in nodes)
            {
                node.Accept(a);
            }
            

            var watch = new Stopwatch();
            
            watch.Start();

            foreach (var inputNode in nodes.Where(x => x.Strategy.GetType() == typeof(InputStrategy)))
            {
                if (DetectLoop(inputNode))
                    throw new Exception("Loop found!");
                
                inputNode.Process();
            }
            watch.Stop();

            var totalTimesTraversed = nodes.Sum(node => node.TimesTraversed);


            Console.Out.WriteLine($"Total time to run circuit: {watch.ElapsedTicks}");
            Console.Out.WriteLine($"Actual time: {totalTimesTraversed * 15} milliseconds");

            foreach (var test in nodes.Where(x => x.Strategy.GetType() == typeof(ProbeStrategy)))
            {
                Console.Out.WriteLine($"{test.Name}.Output = {test.Output}");
            }

            
        }

        public static bool DetectLoop(Node node)
        {
            var hashSet = new HashSet<Node>();

            while (node != null)
            {
                if (hashSet.Contains(node))
                    return true;

                hashSet.Add(node);

                node = node.Outputs.FirstOrDefault();
            }

            return false;
        }
    }
}