using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class OperateAsOperator<TElement, TValue> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public Operand<TElement, TValue> Operand { get; set; }

        public OperateAsOperator(Command<TElement, TValue> operate) : base()
        {
            this.Operand = operate;
        }

        public OperateAsOperator(Operand<TElement, TValue> operate) : base()
        {
            this.Operand = operate;
        }

        public override bool Operate(Rule<TElement> rule)
        {
            return (this.Operand.Value(rule.Element) as bool?) ?? false;
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
