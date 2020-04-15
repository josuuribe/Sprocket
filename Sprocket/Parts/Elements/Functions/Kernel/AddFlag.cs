using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    [DataContract]
    internal sealed class AddFlag<T> : BooleanFunction<T, int>
        where T : IElement
    {
        public override bool Value(RuleElement<T> element)
        {
            element.UserStatus = element.UserStatus | Parameters;
            return true;
        }
    }
}
