using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using System;
using System.Collections.Generic;
using System.Text;

namespace RaraAvis.Sprocket.Tests.Entities.Commands.PersonCommands
{
    public class DistanceCommand : ExpressionCommand<Person, int>
    {
        public override int Value(RuleElement<Person> element)
        {
            return element.Element.DistanceTravelled;
        }
    }
}
