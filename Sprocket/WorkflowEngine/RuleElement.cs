using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Dynamic;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    /// <summary>
    /// Class that defines base rule container, a workflow to process, a state machine with the result, an element with the information for the rule and dynamic data with information created by the rule.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RuleElement<T>
        where T : IElement
    {
        public RuleElement()
        {
            this.DynamicData = new ExpandoObject();
            this.StageResult = StageResult.NONE;
            this.UserStatus = 1;
        }

        public RuleElement(T element) : this()
        {
            this.Element = element;
        }
        /// <summary>
        /// Element to process.
        /// </summary>
        public T Element { get; set; }

        public int UserStatus { get; set; }
        internal StageStatus StageStatus { get; set; }
        internal StageResult StageResult { get; set; }
        /// <summary>
        /// Dynamic data created by rule engine during rule processing.
        /// </summary>
        internal dynamic DynamicData;
    }
}
