namespace WarHub.ArmouryModel.Source
{
    /// <summary>
    /// The base class for all Cores. Defines methods to
    /// create inheritance-supporting <see cref="ToNode(SourceNode)"/> method.
    /// </summary>
    public abstract class NodeCore
    {
        /// <summary>
        /// Create a Node from this Core.
        /// </summary>
        /// <param name="parent">Optional parent of the created Node.</param>
        /// <returns>The newly created Node.</returns>
        public SourceNode ToNode(SourceNode parent = null)
        {
            return ToNodeCore(parent);
        }

        protected abstract SourceNode ToNodeCore(SourceNode parent);
    }
}
