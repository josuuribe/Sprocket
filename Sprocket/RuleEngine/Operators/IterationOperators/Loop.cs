using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Operators.IterationOperators
{
    [DataContract]
    internal sealed class Loop<TTarget, TValue> : IterationOperator<TTarget>
        where TTarget : notnull
    {
        [DataMember]
        public Operand<TTarget, TValue> Block { get; set; }

        public Loop(Operator<TTarget> condition, Operand<TTarget, TValue> block) : base(condition)
        {
            this.Block = block;
        }

        public override bool Process(Rule<TTarget> rule)
        {
            while (Condition.Process(rule))
            {
                var next = Block;
                do
                {
                    next.Process(rule);
                    next = next.Next;
                } while (!(next is Operand<TTarget, TValue>.Noop));
            }
            return true;
        }
    }
}
