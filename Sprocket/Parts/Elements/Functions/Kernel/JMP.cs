using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    /// <summary>
    /// Sets the JMP property to establish next stage to execute.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal sealed class JMP<T> : BooleanFunction<T, int>
        where T : IElement
    {
        public override bool Value(RuleElement<T> element)
        {
            element.StageAction = StageAction.Jmp;
            element.NextStageId = this.Parameters;
            return true;
        }
    }
}
