using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public class FalseCommand<TElement> : BooleanCommand<TElement>
        where TElement : IElement
    {
        public override bool Value(RuleElement<TElement> element)
        {
            return false;
        }
    }
}
