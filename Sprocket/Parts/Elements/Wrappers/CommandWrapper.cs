using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [KnownType("GetKnownType")]
    [DataContract]
    internal class CommandWrapper<TElement, TValue> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public Command<TElement, TValue> Command { get; set; }

        public TValue Result { get; private set; }

        public CommandWrapper(Command<TElement, TValue> command)
        {
            this.Command = command;
        }

        public override bool Match(RuleElement<TElement> element)
        {
            this.Result = Command.Value(element);
            return true;
        }

        public static implicit operator TValue(CommandWrapper<TElement, TValue> command)
        {
            return command.Result;
        }

        private static Type[] GetKnownType()
        {
            Type[] t = new Type[1];
            t[0] = typeof(CommandWrapper<TElement, TValue>);
            return t;
        }
    }
}
