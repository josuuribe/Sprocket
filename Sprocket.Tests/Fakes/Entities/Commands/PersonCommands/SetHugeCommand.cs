using RaraAvis.Sprocket.RuleEngine.Elements.Operates;

namespace RaraAvis.Sprocket.Tests.Fakes.Entities.Commands.PersonCommands
{
    class SetHugeCommand : Command<Person, bool>
    {
        public SetHugeCommand() : base() { }
        public SetHugeCommand(Person p) : base(p) { }
        public override bool Value(Person element)
        {
            element.UserStatus = (int)Feature.Huge;
            return true;
        }
    }
}
