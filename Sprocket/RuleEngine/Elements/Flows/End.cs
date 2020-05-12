using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Flows
{
    [DataContract]
    internal sealed class End<TElement> : IOperator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public IOperator<TElement> Next { get; set; }
        public IOperator<TElement> Previous { get; set; }

        public End()
        {
            this.Next = this;
        }

        public bool Operate(Rule<TElement> rule)
        {
            rule.StageAction = StageAction.Finish;
            return false;
        }
    }
}
