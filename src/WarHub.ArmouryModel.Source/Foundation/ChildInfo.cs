namespace WarHub.ArmouryModel.Source
{
    /// <summary>
    /// A pair of a child node and a name of the child in parent.
    /// </summary>
    public struct ChildInfo
    {
        /// <summary>
        /// Creates a new instance of <see cref="ChildInfo"/>.
        /// </summary>
        /// <param name="name">The name of the node.</param>
        /// <param name="node">The node with the given name.</param>
        public ChildInfo(string name, SourceNode node)
        {
            Name = name;
            Node = node;
        }

        /// <summary>
        /// True if <see cref="Node"/>'s <see cref="SourceNode.IsList"/> is <c>true</c>.
        /// </summary>
        public bool IsList => Node.IsList;

        /// <summary>
        /// Name this <see cref="Node"/> can be found under in its parent node.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The node that is found under <see cref="Name"/> in its parent node.
        /// </summary>
        public SourceNode Node { get; }
    }
}
