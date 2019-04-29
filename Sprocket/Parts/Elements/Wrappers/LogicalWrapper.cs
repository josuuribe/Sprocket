using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [KnownType("GetKnownType")]
    [DataContract]
    internal class LogicalWrapper<T> : Operator<T>
        where T : IElement
    {
        [DataMember]
        public IOperate<T, bool> Operate { get; set; }

        public LogicalWrapper(IOperate<T, bool> operate)
        {
            this.Operate = operate;
        }

        public LogicalWrapper(bool affirm)
        {
            this.Operate = new ValueWrapper<T, bool>(affirm);
        }

        public override bool Match(RuleElement<T> element)
        {
            return this.Operate.Value(element);
        }

        private static Type[] GetKnownType()
        {
            Type[] t = new Type[1];
            t[0] = typeof(LogicalWrapper<T>);
            return t;
        }
    }
}
