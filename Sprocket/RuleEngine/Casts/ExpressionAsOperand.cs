using RaraAvis.Sprocket.WorkflowEngine.Entities;
using Serialize.Linq.Serializers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Casts
{
    [DataContract]
    internal class ExpressionAsOperand<TTarget, TValue> : Operand<TTarget, TValue>
        where TTarget : notnull
    {
        private Expression<Func<Rule<TTarget>, TValue>> Expression { get; set; }

        [DataMember]
        [DisallowNull]
        private string SerializedExpression
        {
            get; set;
        } = string.Empty;

        public ExpressionAsOperand([DisallowNull]Expression<Func<Rule<TTarget>, TValue>> expression)
        {
            this.Expression = expression;
        }

        public override TValue Process(TTarget target)
        {
            return this.Expression.Compile().Invoke(target);
        }

        [OnSerializing()]
        private void OnSerializing(StreamingContext c)
        {
            SerializedExpression = GetSerializer(c).SerializeText(Expression);
        }

        [OnDeserialized()]
        private void OnDeserialized(StreamingContext c)
        {
            Expression = (GetSerializer(c).DeserializeText(SerializedExpression) as Expression<Func<Rule<TTarget>, TValue>>)!;
        }

        private ExpressionSerializer GetSerializer(StreamingContext c)
        {
            switch (c.State)
            {
                case StreamingContextStates.Other:
                    var jsonSerializer = new JsonSerializer();
                    return new ExpressionSerializer(jsonSerializer);
                default:
                    var xmlSerializer = new XmlSerializer();
                    return new ExpressionSerializer(xmlSerializer);
            }
        }
    }
}
