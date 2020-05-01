using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Operate<TElement, TValue> : IOperate<TElement, TValue>
        where TElement : IElement
    {
        protected TElement element;
        public TValue Value => this.Process(this.element);
        public TElement Element => this.element;
        internal Operate() { }
        internal Operate(TElement element)
        {
            this.element = element;
        }
        protected internal abstract TValue Process(RuleElement<TElement> element);

        public static implicit operator TValue(Operate<TElement, TValue> operate)
        {
            return operate.Value;
        }

        public static Operator<TElement> operator ^(Operate<TElement, TValue> operate, int stageId)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = new OperateAsOperator<TElement, TValue>(operate);
            JMP<TElement> jmp = new JMP<TElement>();
            jmp.Parameters = stageId;
            ifThen.Then = new OperateAsOperator<TElement, bool>(jmp);
            return ifThen;
        }
    }
}
