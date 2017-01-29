using System.Collections.Generic;
using System.Runtime.Serialization;
using CuttingEdge.Patterns.Abstractions;

namespace CuttingEdge.Patterns.View
{
    [DataContract]
    public class EntityView : IView
    {
        public EntityView()
        {
            AttributeViews = new List<AttributeView>();
            ActionViews = new List<ActionView>();
        }

        public string Reference { get; set; }

        [DataMember(Order = 1)]
        public string Label { get; set; }

        [DataMember(Order = 2)]
        public string Prompt { get; set; }

        [DataMember(Order = 3)]
        public virtual List<AttributeView> AttributeViews { get; set; }

        [DataMember(Order = 4)]
        public virtual List<ActionView> ActionViews { get; set; }
    }

    [DataContract]
    public class AttributeView
    {
        public AttributeView()
        {
            ActionViews = new List<ActionView>();
        }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        [DataMember(Order = 2)]
        public string Format { get; set; }

        [DataMember(Order = 3)]
        public string Label { get; set; }

        [DataMember(Order = 4)]
        public string Prompt { get; set; }

        [DataMember(Order = 5)]
        public string Href { get; set; }

        [DataMember(Order = 6)]
        public bool IsReadOnly { get; set; }

        [DataMember(Order = 7)]
        public bool IsRequired { get; set; }

        [DataMember(Order = 8)]
        public bool IsSortable { get; set; }

        [DataMember(Order = 9)]
        public bool IsFilterable { get; set; }

        [DataMember(Order = 10)]
        public virtual List<ActionView> ActionViews { get; set; }
    }

    [DataContract]
    public class ActionView
    {
        [DataMember(Order = 1)]
        public string Label { get; set; }

        [DataMember(Order = 2)]
        public string Prompt { get; set; }

        [DataMember(Order = 3)]
        public string Href { get; set; }
    }
}
