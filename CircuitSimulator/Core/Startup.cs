using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Core.Builders;
using Core.Nodes;
using Core.Nodes.Strategies;

namespace Core
{
    public class Startup
    {
        static void Main(string[] args)
        {
            var nodeFactory = NodeFactory.GetInstance;

            nodeFactory
                .RegisterStrategy<InputStrategy>("input_high", "input_low")
                .RegisterStrategy<ProbeStrategy>("probe")
                .RegisterStrategy<NotStrategy>("not")
                .RegisterStrategy<AndStrategy>("and")
                .RegisterStrategy<OrStrategy>("or")
                .RegisterStrategy<NorStrategy>("nor")
                .RegisterStrategy<NandStrategy>("nand")
                .RegisterStrategy<XorStrategy>("xor");

            var (nodes, edges) = FileParser.ParseFile(FileReader.ReadFile("Circuit3_Encoder"));
            
            var circuitBuilder = new CircuitBuilder();
            
            circuitBuilder.BuildNodes(nodes);
            circuitBuilder.BuildEdges(edges);

            var circuit = circuitBuilder.BuildCircuit();


            var watch = new Stopwatch();

            watch.Start();
            circuit.Simulate();
            watch.Stop();

            Console.Out.WriteLine($"Propagation delay: {circuit.ExecutionTime()} nanoseconds");

            foreach (var test in circuit.OutputNodes)
            {
                Console.Out.WriteLine($"{test.Name}.Output = {test.Output}");
            }
        }
    }
}