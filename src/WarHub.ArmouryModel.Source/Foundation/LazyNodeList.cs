﻿using System;
using System.Collections.Immutable;
using System.Threading;

namespace WarHub.ArmouryModel.Source
{
    internal sealed class LazyNodeList<TNode, TCore>
        : IReadOnlyNodeList<TNode>, INodeListWithCoreArray<TNode, TCore>
        where TNode : SourceNode, INodeWithCore<TCore>
        where TCore : ICore<TNode>
    {
        private LazyNodeList(ImmutableArray<TCore> cores, SourceNode parent)
        {
            Parent = parent;
            Cores = cores;
            Nodes = new ArrayElement<TNode>[Cores.Length];
        }

        private SourceNode Parent { get; }

        private ArrayElement<TNode>[] Nodes { get; }

        public ImmutableArray<TCore> Cores { get; }

        public int Count => Cores.Length;

        public TNode this[int index] => GetNodeSlot(index);

        public TNode GetNodeSlot(int index)
        {
            ref var value = ref Nodes[index].Value;
            if (value == null)
            {
                var coreValue = Cores[index];
                Interlocked.CompareExchange(ref value, coreValue.ToNode(Parent), null);
                value._indexInParent = index;
            }
            return value;
        }

        private class OneElementList : IReadOnlyNodeList<TNode>, INodeListWithCoreArray<TNode, TCore>
        {
            public OneElementList(ImmutableArray<TCore> cores, SourceNode parent)
            {
                Cores = cores;
                Parent = parent;
            }

            private TNode _node;

            public int Count => 1;

            private SourceNode Parent { get; }

            private ImmutableArray<TCore> Cores { get; }

            public TNode this[int index] => GetNodeSlot(index);

            ImmutableArray<TCore> INodeListWithCoreArray<TNode, TCore>.Cores => Cores;

            public TNode GetNodeSlot(int index)
            {
                if (_node == null)
                {
                    Interlocked.CompareExchange(ref _node, Cores[index].ToNode(Parent), null);
                    _node._indexInParent = index;
                }
                return _node;
            }
        }

        private class TwoElementList : IReadOnlyNodeList<TNode>, INodeListWithCoreArray<TNode, TCore>
        {
            public TwoElementList(ImmutableArray<TCore> cores, SourceNode parent)
            {
                Cores = cores;
                Parent = parent;
            }

            private TNode _node0;
            private TNode _node1;

            public int Count => 2;

            private SourceNode Parent { get; }

            private ImmutableArray<TCore> Cores { get; }

            public TNode this[int index] => GetNodeSlot(index);

            ImmutableArray<TCore> INodeListWithCoreArray<TNode, TCore>.Cores => Cores;

            public TNode GetNodeSlot(int index)
            {
                switch (index)
                {
                    case 0: return GetNode(ref _node0, 0);
                    case 1: return GetNode(ref _node1, 1);
                    default:
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                }
            }

            private TNode GetNode(ref TNode node, int index)
            {
                if (node == null)
                {
                    Interlocked.CompareExchange(ref node, Cores[index].ToNode(Parent), null);
                    node._indexInParent = index;
                }
                return node;
            }
        }

        private class ThreeElementList : IReadOnlyNodeList<TNode>, INodeListWithCoreArray<TNode, TCore>
        {
            public ThreeElementList(ImmutableArray<TCore> cores, SourceNode parent)
            {
                Cores = cores;
                Parent = parent;
            }

            private TNode _node0;
            private TNode _node1;
            private TNode _node2;

            public int Count => 3;

            private SourceNode Parent { get; }

            private ImmutableArray<TCore> Cores { get; }

            public TNode this[int index] => GetNodeSlot(index);

            ImmutableArray<TCore> INodeListWithCoreArray<TNode, TCore>.Cores => Cores;

            public TNode GetNodeSlot(int index)
            {
                switch (index)
                {
                    case 0: return GetNode(ref _node0, 0);
                    case 1: return GetNode(ref _node1, 1);
                    case 2: return GetNode(ref _node2, 2);
                    default:
                        throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                }
            }

            private TNode GetNode(ref TNode node, int index)
            {
                if (node == null)
                {
                    Interlocked.CompareExchange(ref node, Cores[index].ToNode(Parent), null);
                    node._indexInParent = index;
                }
                return node;
            }
        }

        internal static IReadOnlyNodeList<TNode> CreateContainer(ImmutableArray<TCore> cores, SourceNode parent)
        {
            switch (cores.Length)
            {
                case 0: return default;
                case 1: return new OneElementList(cores, parent);
                case 2: return new TwoElementList(cores, parent);
                case 3: return new ThreeElementList(cores, parent);
                default: return new LazyNodeList<TNode, TCore>(cores, parent);
            }
        }
    }

    internal static class LazyNodeListExtensions
    {
        public static NodeList<TNode> ToNodeList<TNode, TCore>(this ImmutableArray<TCore> cores, SourceNode parent = null)
            where TCore : ICore<TNode>
            where TNode : SourceNode, INodeWithCore<TCore>
        {
            var container = LazyNodeList<TNode, TCore>.CreateContainer(cores, parent);
            return new NodeList<TNode>(container);
        }
    }
}
