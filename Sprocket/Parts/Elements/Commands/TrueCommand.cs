using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public class TrueCommand<T> : BooleanCommand<T>
        where T : IElement
    {
        public override bool Value(RuleElement<T> element)
        {
            return true;
        }
    }
}
