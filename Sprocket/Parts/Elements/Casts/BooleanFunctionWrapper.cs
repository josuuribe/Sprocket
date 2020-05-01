using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [DataContract]
    internal class BooleanFunctionWrapper<TElement, TParameters> : BooleanFunction<TElement, TParameters>
        where TElement : IElement
    {
        public BooleanFunctionWrapper(Operator<TElement> operatorToWrap)
        {
            this.Operator = operatorToWrap;
        }

        [DataMember]
        public Operator<TElement> Operator { get; set; }

        public override bool Value(RuleElement<TElement> element)
        {
            return Operator.Match(element);
        }
    }
}
