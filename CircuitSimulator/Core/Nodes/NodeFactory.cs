using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Nodes.Strategies;

namespace Core.Nodes
{
    public class NodeFactory
    {
        public NodeFactory()
        {
            _types = new Dictionary<string, Type>();
        }
        
        private readonly Dictionary<string, Type> _types;

        public NodeFactory RegisterStrategy<T>(params string[] aliases) where T : INodeStrategy
        {
            _types.Add(typeof(T).Name.ToLower(), typeof(T));
            
            foreach (var alias in aliases)
                _types.Add(alias, typeof(T));

            return this;
        }

        public Node CreateNode(string name, string type)
        {
            return _types.TryGetValue(type, out var instanceType) 
                ? new Node(name, Activator.CreateInstance(instanceType) as INodeStrategy) 
                : throw new Exception($"Invalid strategy passed: {type}. Check your input or register this strategy.");
        }
    }
}