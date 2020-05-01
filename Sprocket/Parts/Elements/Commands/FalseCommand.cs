using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public class FalseCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public FalseCommand() { }
        public FalseCommand(TElement p) : base(p) { }
        protected internal override bool Process(RuleElement<TElement> element)
        {
            return false;
        }
    }
}
