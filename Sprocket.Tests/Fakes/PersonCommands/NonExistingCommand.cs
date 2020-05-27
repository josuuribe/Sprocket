using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.Tests.Fakes.Entities;
using System.Diagnostics.CodeAnalysis;

namespace RaraAvis.Sprocket.Tests.Fakes.PersonCommands
{
    internal class NonExistingCommand : Operand<Person, bool>
    {
        [return: MaybeNull]
        public override bool Process([DisallowNull] Person target)
        {
            return false;
        }
    }
}
