using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators
{
    /// <summary>
    /// Operator that returns a RuleElement<T> object.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal class True<T> : UnaryOperator<T>
        where T : IElement
    {
        public True() : base()
        { }
        public True(Operator<T> @operator) : base(@operator)
        { }
        public override bool Process(Rule<T> element)
        {
            Operator.Process(element);
            return true;
        }
    }
}


