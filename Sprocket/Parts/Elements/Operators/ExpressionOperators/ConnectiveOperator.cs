using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators
{
    /// <summary>
    /// Base class that stores all connective operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal abstract class ConnectiveOperator<T> : ExpressionOperator<T>
    where T : IElement
    {
        public ConnectiveOperator()
        {

        }

        public ConnectiveOperator(IOperator<T> ifOperator, IOperator<T> thenOperator)
        {
            IfThen<T> ifthen = new IfThen<T>();
            ifthen.If = ifOperator;
            ifthen.Then = thenOperator;
        }

        public ConnectiveOperator(IOperator<T> ifOperator, IOperator<T> thenOperator, IOperator<T> elseOperator)
        {
            IfThenElse<T> ifthen = new IfThenElse<T>();
            ifthen.If = ifOperator;
            ifthen.Then = thenOperator;
            ifthen.Else = elseOperator;
        }
    }
}
