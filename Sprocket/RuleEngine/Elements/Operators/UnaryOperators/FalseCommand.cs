using RaraAvis.Sprocket.RuleEngine.Elements.Operates;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands
{
    [DataContract]
    public class FalseCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public FalseCommand() { }
        public FalseCommand(TElement p) : base(p) { }
        public override bool Process(Rule<TElement> element)
        {
            return false;
        }
    }
}
