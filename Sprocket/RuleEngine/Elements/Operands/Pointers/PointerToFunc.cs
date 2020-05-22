using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using Serialize.Linq.Interfaces;
using Serialize.Linq.Serializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements
{
    [DataContract]
    public class PointerToFunc<TElement, TValue> : Operand<TElement, TValue>
        where TElement : IElement
    {
        private Expression<Func<Rule<TElement>, TValue>> Expression { get; set; }

        [DataMember]
        private string SerializedExpression
        {
            get;
            set;
        }

        public PointerToFunc(Expression<Func<Rule<TElement>, TValue>> expression)
        {
            this.Expression = expression;
        }

        public override TValue Process(Rule<TElement> element)
        {
            return this.Expression.Compile().Invoke(element);
        }

        [OnSerializing()]
        internal void OnSerializing(StreamingContext c)
        {
            SerializedExpression = GetSerializer(c).SerializeText(Expression);
        }

        [OnDeserialized()]
        internal void OnDeserialized(StreamingContext c)
        {
            Expression = GetSerializer(c).DeserializeText(SerializedExpression) as Expression<Func<Rule<TElement>, TValue>>;
        }

        private ExpressionSerializer GetSerializer(StreamingContext c)
        {
            ISerializer serializer = null;
            switch (c.State)
            {
                case StreamingContextStates.Other:
                    serializer = new Serialize.Linq.Serializers.JsonSerializer();
                    break;
                default:
                    serializer = new XmlSerializer();
                    break;
            }
            return new ExpressionSerializer(serializer);
        }
    }
}
