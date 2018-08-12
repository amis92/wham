using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace WarHub.ArmouryModel.Source
{
    /// <summary>
    /// An abstract base class for source node that represents
    /// a set of other nodes of the same type.
    /// </summary>
    /// <typeparam name="T">Type of all child nodes.</typeparam>
    public abstract class ListNode<T> : SourceNode, IListNode, IReadOnlyList<T>
        where T : SourceNode
    {
        protected ListNode(SourceNode parent) : base(null, parent)
        {
        }

        /// <summary>
        /// Gets count of this node's child elements.
        /// </summary>
        public int Count => NodeList.Count;

        /// <summary>
        /// Gets the kind of elements this list node contains.
        /// </summary>
        public abstract SourceKind ElementKind { get; }

        /// <summary>
        /// Gets <c>true</c> because this node is a list.
        /// </summary>
        public sealed override bool IsList => true;

        /// <summary>
        /// Gets a list of this node's child elements.
        /// </summary>
        public abstract NodeList<T> NodeList { get; }

        NodeList<SourceNode> IListNode.NodeList => NodeList;

        /// <summary>
        /// Gets a child element by <paramref name="index"/>.
        /// </summary>
        /// <param name="index">0-based index of element in this list.</param>
        /// <returns>Retrieved child element.</returns>
        public T this[int index] => (T)GetChild(index);

        /// <summary>
        /// Creates a new list node of the same type, but with a new set
        /// of child nodes.
        /// </summary>
        /// <param name="nodes">The nodes to become children of the new node.</param>
        /// <returns>The newly created <see cref="ListNode{T}"/>,
        /// being of the same type as this instance.</returns>
        public abstract ListNode<T> WithNodes(NodeList<T> nodes);

        public override IEnumerable<SourceNode> Children() => NodeList;

        public override IEnumerable<ChildInfo> ChildrenInfos()
        {
            return NodeList.Index()
                .Select(x => new ChildInfo(x.Key.ToString(), x.Value));
        }

        protected internal override int ChildrenCount => NodeList.Count;

        protected internal override SourceNode GetChild(int index) => NodeList[index];

        public IEnumerator<T> GetEnumerator() => NodeList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
