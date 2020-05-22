using RaraAvis.Sprocket.WorkflowEngine.Entities;

namespace RaraAvis.Sprocket.RuleEngine.Interfaces
{
    public interface ICode
    {
        ICode Next { get; set; }

        ICode Previous { get; set; }
    }

    public interface IProcessor<TElement, TValue> 
        where TElement : IElement
    {
        TValue Process(Rule<TElement> rule);            
    }
}
