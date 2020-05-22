using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators
{
    [DataContract]
    //[KnownType("GetKnownType")]
    internal class False<T> : UnaryOperator<T>
        where T : IElement
    {
        public False() : base()
        { }
        public False(Operator<T> @operator) : base(@operator)
        { }
        public override bool Process(Rule<T> element)
        {
            Operator.Process(element);
            return false;
        }

        //public static IEnumerable<Type> GetKnownType()
        //{
        //    return new Type[] { typeof(False<T>) };
        //}
    }
}
