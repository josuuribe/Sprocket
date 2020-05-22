using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Elements.Operands;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class BooleanOperandAsOperator<TElement> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public IOperand<TElement, bool> Operand { get; set; }

        public BooleanOperandAsOperator(Operand<TElement, bool> operate) : base()
        {
            this.Operand = operate;
        }

        public override bool Process(Rule<TElement> rule)
        {
            var next = Operand;
            var res = true;
            do
            {
                res &= next.Process(rule);
                next = (next as ICode).Next as IOperand<TElement, bool>;
            } while (!(next is Noop<TElement>));
            return res;
        }
        //private static Type[] GetKnownType()
        //{
        //    Type[] t = new Type[1];
        //    t[0] = typeof(OperateWrapper<T>);
        //    return t;
        //}
        //public static implicit operator LogicalWrapper<T>(bool value)
        //{
        //    var operate = new ValueWrapper<T, bool>(value);
        //    return new LogicalWrapper<T>(operate);
        //}
        //public LogicalWrapper(bool affirm)
        //{
        //    this.Operate = new ValueWrapper<T, bool>(affirm);
        //}
    }
}
