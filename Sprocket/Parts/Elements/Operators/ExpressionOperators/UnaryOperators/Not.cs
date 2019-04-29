using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators
{
    /// <summary>
    /// Operator that returns the inverse of a RuleElement<T> object.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    [KnownType("GetKnownType")]
    internal class Not<T> : UnaryOperator<T>
        where T : IElement
    {
        public override bool Match(RuleElement<T> element)
        {
            return !Operator.Match(element);
        }

        public static IEnumerable<Type> GetKnownType()
        {
            return new Type[] { typeof(Not<T>) };
        }
    }
}
