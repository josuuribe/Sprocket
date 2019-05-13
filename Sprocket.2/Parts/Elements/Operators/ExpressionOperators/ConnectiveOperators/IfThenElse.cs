using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators
{
    /// <summary>
    /// Processes an if clause with else body.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [DataContract]
    internal class IfThenElse<T> : ConnectiveOperator<T>
        where T:IElement
    {
        /// <summary>
        /// If clause with IOperator to check condition.
        /// </summary>
        [DataMember]
        public IOperator<T> If { get; set; }
        /// <summary>
        /// Then clause to process in case true.
        /// </summary>
        [DataMember]
        public IOperator<T> Then { get; set; }
        /// <summary>
        /// Else clause to execute in case false. 
        /// </summary>
        [DataMember]
        public IOperator<T> Else { get; set; }

        public override bool Match(RuleElement<T> element)
        {
            bool b = If.Match(element);
            bool c = b ? Then.Match(element) : Else.Match(element);
            return b && c;
        }

        public static ExpressionOperator<T> operator +(bool operatorLeft, IfThenElse<T> operatorRight)
        {
            operatorRight.If = new LogicalWrapper<T>(operatorLeft);
            return operatorRight;
        }
    }
}
