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
        //public IList<RuleElement<T>> WorkflowResults { get; private set; }
        public Stage LastExecutedStage { get; private set; }
        #endregion

        #region ·   Constructor ·
        public EngineManager()
        {
            //this.WorkflowResults = new List<RuleElement<T>>();
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
        public RuleElement<T> Init(Workflow workflow, IList<Stage> stages, T element)
        {
            return Continue(workflow, stages, stages.First(), element);
        }
        /// <summary>
        /// Continue a workflow pending.
        /// </summary>
        /// <param name="workflow">Workflow to process.</param>
        /// <param name="stages">Stages belonging this workflow.</param>
        /// <param name="stage">Init stage.</param>
        /// <param name="element">Element to process.</param>
        public RuleElement<T> Continue(Workflow workflow, IList<Stage> stages, Stage stage, T element)
        {
            //this.WorkflowResults.Clear();
            this.ExecutionEngineResult = ExecutionEngineResult.NONE;

            var re = Process(stages, stage, element);

            switch (re.StageStatus)
            {//Workflow end result
                case StageResult.POSITIVE:
                    ExecutionEngineResult = ExecutionEngineResult.OK;
                    //workflow.Completed(stage);
                    break;
                case StageResult.NEGATIVE:
                    ExecutionEngineResult = ExecutionEngineResult.KO;
                    //workflow.Incompleted(stage);
                    break;
                case StageResult.ERROR:
                    ExecutionEngineResult = ExecutionEngineResult.ERROR;
                    //workflow.Failed(stage);
                    break;
                case StageResult.EXIT:
                    ExecutionEngineResult = ExecutionEngineResult.EXIT;
                    //workflow.Exited(stage);
                    break;
            }
            LastExecutedStage = stage;
            return re;
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
                            switch (ruleElement.StageAction)
                            {
                                case StageStatus.CONTINUE:
                                    {
                                        ruleElement.StageStatus = StageResult.POSITIVE;
                                    }
                                    break;
                                case StageStatus.JMP:
                                    {
                                        stagesEnumerator.Reset();
                                        return this.Process(stages, stages.First(s => s.Name == ruleElement.DynamicData.NextStage), element);
                                    }
                                case StageStatus.BREAK:
                                    {
                                        ruleElement.StageStatus = StageResult.EXIT;
                                        //WorkflowResults.Add(ruleElement);
                                        return ruleElement;
                                    }
                                default:
                                    {
                                        ruleElement.StageStatus = StageResult.NONE;
                                        //WorkflowResults.Add(ruleElement);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            ruleElement.StageStatus = StageResult.NEGATIVE;
                            //WorkflowResults.Add(ruleElement);
                            return ruleElement;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ruleElement.StageStatus = StageResult.ERROR;
            }
            return ruleElement;
        }
        #endregion
    }
}
