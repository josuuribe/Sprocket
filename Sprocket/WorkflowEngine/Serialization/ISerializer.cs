using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine.Serialization
{
    public interface ISerializer<TElement>
        where TElement:IElement
    {
        string Serialize(IOperator<TElement> @operator);

        IOperator<TElement> Deserialize(string text);
    }
}
