using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    /// <summary>
    /// Sets the JMP property to establish next stage to execute.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    [DataContract]
    internal sealed class JMP<TElement> : Function<TElement, int, bool>
        where TElement : IElement
    {
        public JMP()
        { }
        public JMP(TElement element = default(TElement), int parameters = default(int)) : base(element, parameters)
        { }
        protected internal override bool Process(RuleElement<TElement> element)
        {
            element.StageAction = StageAction.Jmp;
            element.NextStageId = this.Parameters;
            return true;
        }
    }
}
