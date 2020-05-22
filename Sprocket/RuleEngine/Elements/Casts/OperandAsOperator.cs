using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class OperandAsOperator<TElement, TValue> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public IOperand<TElement, TValue> Operand { get; set; }

        public OperandAsOperator(Operand<TElement, TValue> operate) : base()
        {
            this.Operand = operate;
        }

        public override bool Process(Rule<TElement> rule)
        {
            Operand.Process(rule);
            return true;
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
