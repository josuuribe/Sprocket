using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.Parts.Elements.Wrappers
{
    [KnownType("GetKnownType")]
    [DataContract]
    internal class CommandWrapper<T, U> : Operator<T>
        where T : IElement
    {
        [DataMember]
        public Command<T, U> Command { get; set; }

        public U Result { get; private set; }

        public CommandWrapper(Command<T, U> command)
        {
            this.Command = command;
        }

        public override bool Match(RuleElement<T> element)
        {
            this.Result = Command.Value(element);
            return true;
        }

        public static implicit operator U(CommandWrapper<T, U> command)
        {
            return command.Result;
        }

        private static Type[] GetKnownType()
        {
            Type[] t = new Type[1];
            t[0] = typeof(CommandWrapper<T, U>);
            return t;
        }
    }
}
