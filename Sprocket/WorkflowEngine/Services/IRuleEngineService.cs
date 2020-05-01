using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.Services
{
    /// <summary>
    /// Expose all requires functionality a rule engine must adopt.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[InheritedExport(typeof(IRuleEngineService<>))]
    public interface IRuleEngineService<T>
        where T : IElement
    {
        /// <summary>
        /// Starts an engine manager.
        /// </summary>
        /// <param name="stages">Woorkflow stages.</param>
        /// <param name="stags">Element to process.</param>
        RuleElement<T> Init(IList<Stage> stages, T element);
        /// <summary>
        /// Continue a workflow pending.
        /// </summary>
        /// <param name="stages">Stages belonging this workflow.</param>
        /// <param name="stage">Init stage.</param>
        /// <param name="element">Element to process.</param>
        RuleElement<T> Continue(IList<Stage> stages, Stage stage, T element);
        /// <summary>
        /// Serializes one rule.
        /// </summary>
        /// <param name="rule">Rule to serialize.</param>
        /// <param name="stage">Stage with Xml.</param>
        /// <returns>A string with Xml serialized.</returns>
        string Serialize(Operator<T> rule, Stage stage);
        /// <summary>
        /// Stores individual result by workflow.
        /// </summary>
        ExecutionEngineResult ExecutionEngineResult { get; }
    }
}
