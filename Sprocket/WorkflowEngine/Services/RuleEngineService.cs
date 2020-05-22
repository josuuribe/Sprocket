using RaraAvis.Sprocket.RuleEngine.Elements;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
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
            rule.ExecutionResult = ExecutionResult.Positive;
            this.Process(op, rule);
            return rule;
        }

        /// <summary>
        /// Starts an engine manager.
        /// </summary>
        /// <param name="element">Element  to process.</param>
        private void Process(IOperator<T> op, Rule<T> rule)
        {
            try
            {
                rule.ExecutionResult = op.Process(rule) ? rule.ExecutionResult : ExecutionResult.Negative;
            }
            catch (Exception)
            {
                rule.ExecutionResult = ExecutionResult.Error;
            }
        }

        public string Serialize(IOperator<T> @operator)
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
