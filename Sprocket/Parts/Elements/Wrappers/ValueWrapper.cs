using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [DataContract]
    internal class ValueWrapper<T, U> : IOperate<T, U>
        where T : IElement
    {
        [DataMember]
        public U Operate { get; set; }

        public ValueWrapper(U operate)
        {
            this.Operate = operate;
        }

        public U Value(RuleElement<T> element)
        {
            return Operate;
        }

        public static implicit operator ValueWrapper<T, U>(U value)
        {
            return new ValueWrapper<T, U>(value);
        }
    }
}
