using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators.BinaryOperators
{
    /// <summary>
    /// Processes Or with short-circuit evaluation.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    //[KnownType("GetKnownType")]
    public class Or<T> : BinaryOperator<T>
        where T : IElement
    {
        public override bool Operate(Rule<T> element)
        {
            return OperatorLeft.Operate(element) || OperatorRight.Operate(element);
        }

        //private static Type[] GetKnownType()
        //{
        //    Type[] t = new Type[1];
        //    t[0] = typeof(Or<T>);
        //    return t;
        //}
    }
}
