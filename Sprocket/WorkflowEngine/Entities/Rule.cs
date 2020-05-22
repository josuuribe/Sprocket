using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.WorkflowEngine.Entities
{
    /// <summary>
    /// Class that defines base rule container, a workflow to process, a state machine with the result, an element with the information for the rule and dynamic data with information created by the rule.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Rule<T>
        where T : IElement
    {
        public Rule()
        {
            this.ExecutionResult = ExecutionResult.None;
            this.UserStatus = 0;
            this.Parameters = new Dictionary<string, object>();
        }

        public Rule(T element) : this()
        {
            this.Element = element;
        }
        /// <summary>
        /// Element to process.
        /// </summary>
        public T Element { get; set; }

        public int UserStatus { get; set; }
        public ExecutionResult ExecutionResult { get; set; }

        public Dictionary<string, object> Parameters { get; }

        public static implicit operator Rule<T>(T person)
        {
            return new Rule<T>(person);
        }

        public static implicit operator T(Rule<T> rule)
        {
            return rule.Element;
        }
    }
}
