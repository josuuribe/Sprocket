using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators
{
    [DataContract]
    internal abstract class ComparisonOperator<T, U> : Operator<T>
        where T : IElement
    {
        [DataMember]
        public IOperand<T, U> OperateLeft;
        [DataMember]
        public IOperand<T, U> OperateRight;
    }
}
