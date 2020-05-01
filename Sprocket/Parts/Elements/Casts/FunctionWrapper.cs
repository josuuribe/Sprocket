using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class FunctionWrapper<T, U, V> : Operator<T>
        where T : IElement
    {
        [DataMember]
        public Function<T, U, V> Operate { get; set; }

        public V Result { get; private set; }

        public FunctionWrapper(Function<T, U, V> operate)
        {
            this.Operate = operate;
        }

        public override bool Match(RuleElement<T> element)
        {
            this.Result = this.Operate.Process(element);
            return true;
        }

        //private static Type[] GetKnownType()
        //{
        //    Type[] t = new Type[1];
        //    t[0] = typeof(FunctionWrapper<T, U, V>);
        //    return t;
        //}
    }
}
