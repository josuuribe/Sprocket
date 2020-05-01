using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    [DataContract]
    internal sealed class RemoveFlag<TElement> : Function<TElement, int, bool>
        where TElement : IElement
    {
        public RemoveFlag(int parameter) : base(default(TElement), parameter)
        { }

        protected internal override bool Process(RuleElement<TElement> element)
        {
            element.UserStatus = element.UserStatus & ~Parameters;
            return true;
        }
    }
}
