﻿using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators
{
    /// <summary>
    /// Processes And with short-circuit evaluation.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    [KnownType("GetKnownType")]
    internal class And<T> : BinaryOperator<T>
        where T : IElement
    {
        public override bool Match(RuleElement<T> element)
        {
            return OperatorLeft.Match(element) && OperatorRight.Match(element);
        }

        private static Type[] GetKnownType()
        {
            Type[] t = new Type[1];
            t[0] = typeof(And<T>);
            return t;
        }
    }
}
