using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators
{
    [DataContract]
    internal class Loop<T> : ConnectiveOperator<T>
        where T : IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public IOperator<T> If { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperator<T> Block { get; set; }

        public override bool Match(RuleElement<T> element)
        {
            bool b = true;
            while(If.Match(element))
            {
                b &= Block.Match(element);
            }
            return b;
        }
    }
}
