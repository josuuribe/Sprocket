using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators
{
    [DataContract]
    [KnownType("GetKnownType")]
    internal class False<T> : UnaryOperator<T>
        where T : IElement
    {
        public override bool Match(RuleElement<T> element)
        {
            Operator.Match(element);
            return false;
        }

        public static IEnumerable<Type> GetKnownType()
        {
            return new Type[] { typeof(False<T>) };
        }
    }
}
