using RaraAvis.Sprocket.RuleEngine;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using RaraAvis.Sprocket.WorkflowEngine.Serialization;
using System;

namespace RaraAvis.Sprocket.WorkflowEngine.Services
{
    /// <summary>
    /// Engine manager, inits, process and creates rule related information.
    /// </summary>
    /// <typeparam name="TTarget">An IElement object.</typeparam>
    internal class RuleEngineService<TTarget> : IRuleEngineService<TTarget>
        where TTarget : notnull
    {
        /// <inheritdoc/>
        public ISerializer<TTarget> Serializer { get; private set; }

        public RuleEngineService(ISerializer<TTarget> serializer)
        {
            this.Serializer = serializer;
        }
        #region ·   Methods ·
        /// <inheritdoc />
        public Rule<TTarget> Init(IOperator<TTarget> op, TTarget target)
        {
            Rule<TTarget> rule = new Rule<TTarget>(target)
            {
                ExecutionResult = ExecutionResult.Positive
            };
            this.Process(op, rule);
            return rule;
        }
        private void Process(IOperator<TTarget> @operator, Rule<TTarget> rule)
        {
            try
            {
                rule.ExecutionResult = @operator.Process(rule) ? rule.ExecutionResult : ExecutionResult.Negative;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception)
            {
                rule.ExecutionResult = ExecutionResult.Error;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
        #endregion
    }
}
