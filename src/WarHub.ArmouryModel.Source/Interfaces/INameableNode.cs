﻿namespace WarHub.ArmouryModel.Source
{
    public interface INameableNode
    {
        string Name { get; }
    }

    partial class CatalogueBaseNode : INameableNode { }
    partial class CatalogueLinkNode : INameableNode { }
    partial class CharacteristicNode : INameableNode { }
    partial class CharacteristicTypeNode : INameableNode { }
    partial class CostBaseNode : INameableNode { }
    partial class CostTypeNode : INameableNode { }
    partial class DataIndexNode : INameableNode { }
    partial class EntryBaseNode : INameableNode { }
    partial class ProfileTypeNode : INameableNode { }
    partial class PublicationNode : INameableNode { }
    partial class RosterElementBaseNode : INameableNode { }
    partial class RosterNode : INameableNode { }
}
