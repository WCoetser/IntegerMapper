using System.Collections.Generic;

namespace Trl.IntegerMapper.GenericSequenceIntegerMapper
{
    public class GenericSequenceNode<TItem> {
        public TItem Value { get; }
        public ulong? AssignedInteger { get; set; }
        public Dictionary<TItem, GenericSequenceNode<TItem>> NextValues { get; }
        public GenericSequenceNode<TItem> Parent { get; }

        public GenericSequenceNode(TItem value, GenericSequenceNode<TItem> parent, IEqualityComparer<TItem> equalityComparer)
        {
            NextValues = new Dictionary<TItem, GenericSequenceNode<TItem>>(equalityComparer);
            Parent = parent;
            Value = value;            
        }
    }
}
