using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

            var output = FileParser.ParseFile(FileParser.ReadFile("Circuit1_FullAdder"));
            
            var circuitBuilder = new CircuitBuilder();

            foreach (var (name, type) in output.nodes)
            {
                var node = nodeFactory.CreateNode(name, type);

                if (node.StrategyType == typeof(InputStrategy))
                    node.SetOutput(type == "input_low" ? NodeOutput.Off : NodeOutput.On);

                circuitBuilder.Add(node);
            }

            foreach (var (name, edges) in output.edges)
            {
                foreach (var edge in edges)
                {
                    circuitBuilder.Connect(name, edge);
                }
            }

            var circuit = circuitBuilder.Build();


            var watch = new Stopwatch();

            watch.Start();
            circuit.Simulate();
            watch.Stop();

            Console.Out.WriteLine($"Propogation delay: {circuit.ExecutionTime()} nanoseconds");

            foreach (var test in circuit.OutputNodes)
            {
                Console.Out.WriteLine($"{test.Name}.Output = {test.Output}");
            }
        }
    }
}