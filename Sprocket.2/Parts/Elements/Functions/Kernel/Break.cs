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
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal sealed class Break<T> : BooleanCommand<T>
        where T : IElement
    {
        public override bool Value(RuleElement<T> element)
        {
            element.StageAction = StageStatus.BREAK;
            return true;
        }
    }
}
