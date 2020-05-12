using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel
{
    internal sealed class End<TElement> : Kernel<TElement>
        where TElement : IElement
    {
        public End() : base() { }

        public override bool Operate(Rule<TElement> rule)
        {
            return true;
        }
    }
}
