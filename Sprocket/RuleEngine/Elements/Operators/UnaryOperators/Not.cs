using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators
{
    /// <summary>
    /// Operator that returns the inverse of a RuleElement<T> object.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    //[KnownType("GetKnownType")]
    internal class Not<T> : UnaryOperator<T>
        where T : IElement
    {
        public Not() : base()
        { }

        public Not(Operator<T> @operator) : base(@operator)
        { }

        public override bool Process(Rule<T> element)
        {
            return !Operator.Process(element);
        }

        //public static IEnumerable<Type> GetKnownType()
        //{
        //    return new Type[] { typeof(Not<T>) };
        //}
    }
}
