using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [KnownType("GetKnownType")]
    [DataContract]
    internal class OperateWrapper<T> : Operator<T>
        where T : IElement
    {
        [DataMember]
        public IOperate<T, bool> Operate { get; set; }

        public OperateWrapper(IOperate<T, bool> operate)
        {
            this.Operate = operate;
        }

        //public LogicalWrapper(bool affirm)
        //{
        //    this.Operate = new ValueWrapper<T, bool>(affirm);
        //}

        public override bool Match(RuleElement<T> element)
        {
            return this.Operate.Value(element);
        }

        private static Type[] GetKnownType()
        {
            Type[] t = new Type[1];
            t[0] = typeof(OperateWrapper<T>);
            return t;
        }
        //public static implicit operator LogicalWrapper<T>(bool value)
        //{
        //    var operate = new ValueWrapper<T, bool>(value);
        //    return new LogicalWrapper<T>(operate);
        //}
        public static Operator<T> operator !(OperateWrapper<T> operatorUnary)
        {
            Not<T> ngte = new Not<T>();
            ngte.Operator = operatorUnary;
            return ngte;
        }
    }
}
