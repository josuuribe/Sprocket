using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    /// <summary>
    /// Breaks execution for this rule.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    internal sealed class Break<TElement> : Command<TElement, bool>
        where TElement : IElement
    {

        protected internal override bool Process(RuleElement<TElement> element)
        {
            element.StageAction = StageAction.Break;
            return true;
        }
    }
}
