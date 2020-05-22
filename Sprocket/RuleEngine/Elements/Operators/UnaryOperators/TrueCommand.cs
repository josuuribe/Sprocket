using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands
{
    public class TrueCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public TrueCommand() { }
        public TrueCommand(TElement p) : base(p) { }
        public override bool Process(Rule<TElement> element)
        {
            return true;
        }
    }
}
