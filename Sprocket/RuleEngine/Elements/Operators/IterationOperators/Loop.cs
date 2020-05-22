using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Elements.Operands;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators
{
    [DataContract]
    internal class Loop<TElement, TValue> : IterationOperator<TElement>
        where TElement : IElement
    {
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperand<TElement, TValue> Block { get; set; }

        public Loop()
        { }

        public override bool Process(Rule<TElement> element)
        {
            while (Condition.Process(element))
            {
                var next = Block;
                do
                {
                    next.Process(element);
                    next = (next as ICode).Next as IOperand<TElement, TValue>;
                } while (!(next is Noop<TElement>));
            }
            return true;
        }
    }
}
