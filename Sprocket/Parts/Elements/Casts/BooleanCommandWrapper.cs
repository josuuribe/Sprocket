using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    [DataContract]
    internal class BooleanCommandWrapper<TElement> : OperateAsOperator<TElement, bool>
        where TElement : IElement
    {
        public BooleanCommandWrapper(Operate<TElement, bool> operate) : base(operate) { }

        public override bool Match(RuleElement<TElement> element)
        {
            return Operate.Process(element);
        }
    }
}
