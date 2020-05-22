using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operands
{
    public class Noop<TElement> : IOperand<TElement, bool>, ICode
        where TElement:IElement
    {
        public ICode Next { get
            {
                return this;
            }
            set { } }
        public ICode Previous { get; set; }

        public bool Process(Rule<TElement> rule)
        {
            return true;
        }
    }
}
