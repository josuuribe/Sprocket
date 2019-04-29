using RaraAvis.Sprocket.Parts.Elements.Functions;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [DataContract]
    internal class BooleanFunctionWrapper<T, U> : BooleanFunction<T, U>
        where T : IElement
    {
        public BooleanFunctionWrapper(Operator<T> operatorToWrap)
        {
            this.Operator = operatorToWrap;
        }

        [DataMember]
        public Operator<T> Operator { get; set; }

        public override bool Execute(RuleElement<T> element)
        {
            return Operator.Match(element);
        }
    }
}
