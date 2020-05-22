using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators.BinaryOperators
{
    /// <summary>
    /// Processes And with short-circuit evaluation.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    //[KnownType("GetKnownType")]
    public class And<T> : BinaryOperator<T>
        where T : IElement
    {
        public override bool Process(Rule<T> element)
        {
            return OperatorLeft.Process(element) && OperatorRight.Process(element);
        }

        //private static Type[] GetKnownType()
        //{
        //    Type[] t = new Type[1];
        //    t[0] = typeof(And<T>);
        //    return t;
        //}
    }
}
