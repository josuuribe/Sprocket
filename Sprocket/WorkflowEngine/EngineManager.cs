using RaraAvis.Sprocket.Parts.Elements;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.Services;
using RaraAvis.Sprocket.WorkflowEngine.Workflows;
using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;


namespace RaraAvis.Sprocket.WorkflowEngine
{
    /// <summary>
    /// Engine manager, inits, process and creates rule related information.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    //[PartMetadata(CreationPolicy.NonShared)]
    [Export(typeof(IRuleEngineService<>))]
    public class EngineManager<T> : IRuleEngineService<T>
        where T : IElement
    {
        #region ·   Fields  ·
        private IOperator<T> logicalOperator = null;
        private SerializationManager<T> serializationEngine = null;
        #endregion

        #region ·   Properties  ·
        /// <summary>
        /// Stores global execution result.
        /// </summary>
        public ExecutionEngineResult ExecutionEngineResult { get; private set; }
        /// <summary>
        /// Stores individual result by workflow.
        /// </summary>
        public IList<RuleElement<T>> WorkflowResults { get; private set; }
        #endregion

        #region ·   Constructor ·
        public EngineManager()
        {
            this.WorkflowResults = new List<RuleElement<T>>();
            this.serializationEngine = new SerializationManager<T>();
        }
        #endregion

        #region ·   Methods ·
        public U Execute<U>(Command<T,U> operate, T element)
        {
            RuleElement<T> ruleElement = new RuleElement<T>(element);
            return operate.Value(ruleElement);
        }
        /// <summary>
        /// Serializes one rule.
        /// </summary>
        /// <param name="rule">Rule to serialize.</param>
        /// <param name="stage">Stage with Xml.</param>
        /// <returns>A string with Xml serialized.</returns>
        public string Serialize(Operator<T> rule, Stage stage)
        {
            return serializationEngine.Serialize(rule, stage);
        }
        /// <summary>
        /// Starts an engine manager.
        /// </summary>
        /// <param name="workflows">Workflows to execute.</param>
        /// <param name="element">Element  to process.</param>
        public void Init(Workflow workflow, IList<Stage> stages, T element)
        {
            Continue(workflow, stages, stages.First(), element);
        }
        /// <summary>
        /// Continue a workflow pending.
        /// </summary>
        /// <param name="workflow">Workflow to process.</param>
        /// <param name="stages">Stages belonging this workflow.</param>
        /// <param name="stage">Init stage.</param>
        /// <param name="element">Element to process.</param>
        public void Continue(Workflow workflow, IList<Stage> stages, Stage stage, T element)
        {
            this.WorkflowResults.Clear();
            this.ExecutionEngineResult = Workflows.Enums.ExecutionEngineResult.NONE;

            switch (Process(stages, stage, element).StageResult)
            {
                case StageResult.POSITIVE:
                    ExecutionEngineResult = ExecutionEngineResult.COMPLETED;
                    workflow.Completed();
                    break;
                case StageResult.NEGATIVE:
                    ExecutionEngineResult = ExecutionEngineResult.FAILED;
                    workflow.Failed(stage);
                    break;
                case StageResult.ERROR:
                    ExecutionEngineResult = ExecutionEngineResult.ERROR;
                    workflow.Failed(stage);
                    break;
                case StageResult.EXITED:
                    ExecutionEngineResult = ExecutionEngineResult.EXIT;
                    workflow.Exited(stage);
                    break;
            }
        }
        /// <summary>
        /// Process one workflow element given.
        /// </summary>
        /// <param name="workflow">Workflow to process.</param>
        /// <param name="element">Element to process.</param>
        /// <returns>Global execution result.</returns>
        private RuleElement<T> Process(IList<Stage> stages, Stage stage, T element)
        {
            RuleElement<T> ruleElement = new RuleElement<T>(element);

            try
            {
                var stagesEnumerator = stages.GetEnumerator();
                while (stagesEnumerator.MoveNext())
                {
                    if (stagesEnumerator.Current.Name == stage.Name)
                    {
                        this.logicalOperator = serializationEngine.Deserialize(stagesEnumerator.Current);

                        if (logicalOperator.Match(ruleElement))
                        {
                            switch (ruleElement.StageStatus)
                            {
                                case StageStatus.JMP:
                                    {
                                        stagesEnumerator.Reset();
                                        return this.Process(stages, stages.First(s => s.Name == ruleElement.DynamicData.NextStage), element);
                                    }
                                case StageStatus.BREAK:
                                    {
                                        ruleElement.StageResult = StageResult.EXITED;
                                        WorkflowResults.Add(ruleElement);
                                        return ruleElement;
                                    }
                                default:
                                    {
                                        ruleElement.StageResult = StageResult.POSITIVE;
                                        WorkflowResults.Add(ruleElement);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            ruleElement.StageResult = StageResult.NEGATIVE;
                            WorkflowResults.Add(ruleElement);
                            return ruleElement;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ruleElement.StageResult = StageResult.ERROR;
            }
            return ruleElement;
        }
        #endregion
    }
}
