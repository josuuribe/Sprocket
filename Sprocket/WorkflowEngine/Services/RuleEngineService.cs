using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Entities.Enums;
using System;
using System.Composition;


namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Engine manager, inits, process and creates rule related information.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    //[PartMetadata(CreationPolicy.NonShared)]
    [Export(typeof(IRuleEngineService<>))]
    internal class RuleEngineService<T> : IRuleEngineService<T>
        where T : IElement
    {
        private SerializationManager<T> serializationEngine = null;

        public RuleEngineService()
        {
            serializationEngine = new SerializationManager<T>(RuleEngineActivatorService<T>.Configuration);//RuleEngineActivatorService<T>.Configuration);
        }

        #region ·   Methods ·
        public Rule<T> Init(IOperator<T> op, T element)
        {
            Rule<T> rule = new Rule<T>(element);
            rule.ExecutionResult = ExecutionEngineResult.Correct;
            this.Process(op, rule);
            return rule;
        }

        /// <summary>
        /// Starts an engine manager.
        /// </summary>
        /// <param name="element">Element  to process.</param>
        private void Process(IOperator<T> op, Rule<T> rule)
        {
            bool b = true;
            do
            {
                b &= op.Operate(rule);
                try
                {
                    switch (rule.StageAction)
                    {
                        case StageAction.Break:
                            {
                                rule.ExecutionResult = ExecutionEngineResult.Exit;
                                return;
                            }
                        case StageAction.Continue:
                            {
                                rule.ExecutionResult = ExecutionEngineResult.Correct;
                                var nextOP = op.Next;
                                this.Process(nextOP, rule);
                                return;
                            }
                        case StageAction.Finish:
                            {
                                return;
                            }
                    }
                }
                catch (Exception)
                {
                    rule.ExecutionResult = ExecutionEngineResult.Error;
                }
            } while (b && op != null);
        }

        public string Serialize(Operator<T> @operator)
        {
            return serializationEngine.Serialize(@operator);
        }

        public IOperator<T> Deserialize(string serialized)
        {
            return serializationEngine.Deserialize(serialized);
        }
        #endregion
    }
}
