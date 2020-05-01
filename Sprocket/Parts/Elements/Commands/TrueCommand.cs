using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public class TrueCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public TrueCommand() { }
        public TrueCommand(TElement p) : base(p) { }
        protected internal override bool Process(RuleElement<TElement> element)
        {
            return true;
        }
    }
}
