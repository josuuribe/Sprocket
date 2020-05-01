using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Casts
{
    //[KnownType("GetKnownType")]
    [DataContract]
    internal class CommandAsOperator<TElement, TValue> : OperateAsOperator<TElement, TValue>
        where TElement : IElement
    {
        public CommandAsOperator(Operate<TElement, TValue> operate) : base(operate) { }

        public override bool Match(RuleElement<TElement> element)
        {
            Operate.Process(element);
            return true;
        }

        //private static Type[] GetKnownType()
        //{
        //    Type[] t = new Type[1];
        //    t[0] = typeof(CommandWrapper<TElement, TValue>);
        //    return t;
        //}
    }
}
