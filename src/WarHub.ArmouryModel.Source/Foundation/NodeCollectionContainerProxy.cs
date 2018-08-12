﻿using System.Collections.Immutable;

namespace WarHub.ArmouryModel.Source
{
    internal struct NodeCollectionContainerProxy<TChildNode> : IReadOnlyNodeList<TChildNode>
        where TChildNode : SourceNode
    {
        public NodeCollectionContainerProxy(ImmutableArray<TChildNode> nodes)
        {
            Nodes = nodes;
        }

        internal ImmutableArray<TChildNode> Nodes { get; }

        public int Count => Nodes.Length;

        public TChildNode this[int index] => GetNodeSlot(index);

        public TChildNode GetNodeSlot(int index)
        {
            return Nodes[index];
        }
    }
}
