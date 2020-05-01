using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    [DataContract]
    internal abstract class ComparisonOperator<T, U> : ExpressionOperator<T>
        where T : IElement

    {
        [DataMember]
        public Operate<T, U> OperateLeft;
        [DataMember]
        public Operate<T, U> OperateRight;
    }
}
