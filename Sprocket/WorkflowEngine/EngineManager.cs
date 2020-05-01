﻿using RaraAvis.Sprocket.Parts.Elements;
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
        #endregion

        #region ·   Constructor ·
        public EngineManager()
        {
            this.serializationEngine = new SerializationManager<T>();
        }
        #endregion

        #region ·   Methods ·
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
        /// <param name="element">Element  to process.</param>
        public RuleElement<T> Init(IList<Stage> stages, T element)
        {
            return Continue(stages, stages.First(), element);
        }
        /// <summary>
        /// Continue a workflow pending.
        /// </summary>
        /// <param name="workflow">Workflow to process.</param>
        /// <param name="stages">Stages belonging this workflow.</param>
        /// <param name="stage">Init stage.</param>
        /// <param name="element">Element to process.</param>
        public RuleElement<T> Continue(IList<Stage> stages, Stage stage, T element)
        {
            this.ExecutionEngineResult = ExecutionEngineResult.NONE;

            var re = Process(stages, stage, element);

            switch (re.StageStatus)
            {
                case StageResult.Positive:
                    ExecutionEngineResult = ExecutionEngineResult.OK;
                    break;
                case StageResult.Negative:
                    ExecutionEngineResult = ExecutionEngineResult.KO;
                    break;
                case StageResult.Error:
                    ExecutionEngineResult = ExecutionEngineResult.ERROR;
                    break;
                case StageResult.Exit:
                    ExecutionEngineResult = ExecutionEngineResult.EXIT;
                    break;
            }
            return re;
        }
        /// <summary>
        /// Process one workflow element given.
        /// </summary>
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
                                case StageAction.Continue:
                                    {
                                        ruleElement.StageStatus = StageResult.Positive;
                                    }
                                    break;
                                case StageAction.Jmp:
                                    {
                                        stagesEnumerator.Reset();
                                        return this.Process(stages, stages.First(s => s.Id == ruleElement.NextStageId), element);
                                    }
                                    break;
                                case StageAction.Break:
                                    {
                                        ruleElement.StageStatus = StageResult.Exit;
                                        return ruleElement;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            ruleElement.StageStatus = StageResult.Negative;
                            return ruleElement;
                        }
                    }
                }
            }
            catch (Exception)
            {
                ruleElement.StageStatus = StageResult.Error;
            }
            return ruleElement;
        }
        #endregion
    }
}
