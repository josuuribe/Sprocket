using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators
{
    /// <summary>
    /// Operator that returns a RuleElement<T> object.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    //[KnownType("GetKnownType")]
    internal class True<T> : UnaryOperator<T>
        where T : IElement
    {
        public True() : base()
        { }
        public True(IOperator<T> @operator) : base(@operator)
        { }
        public override bool Match(RuleElement<T> element)
        {
            Operator.Match(element);
            return true;
        }

        //public static IEnumerable<Type> GetKnownType()
        //{
        //    return new Type[] { typeof(True<T>) };
        //}
    }
}


