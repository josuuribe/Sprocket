using RaraAvis.Sprocket.RuleEngine.Interfaces;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates.Commands
{
    public class TrueCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public TrueCommand() { }
        public TrueCommand(TElement p) : base(p) { }
        public override bool Value(TElement element)
        {
            return true;
        }
    }
}
