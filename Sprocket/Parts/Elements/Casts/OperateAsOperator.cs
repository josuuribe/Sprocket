using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class OperateAsOperator<TElement, TValue> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public Operate<TElement, TValue> Operate { get; set; }

        public OperateAsOperator(Command<TElement, TValue> operate)
        {
            this.Operate = operate;
        }

        public OperateAsOperator(Operate<TElement, TValue> operate)
        {
            this.Operate = operate;
        }

        public override bool Match(RuleElement<TElement> element)
        {
            return (this.Operate.Process(element) as bool?) ?? false;
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
