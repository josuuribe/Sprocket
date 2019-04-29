using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;

namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Rule is the highest container, stores one or several workflows, it stores rule general information.
    /// </summary>
    public class Rule
    {
        public Rule()
        {
            BeginDate = DateTime.MinValue;
            EndDate = DateTime.MaxValue;
            CreationDate = DateTime.Now;
            Level = RuleLevel.WARNING;
            RuleVersion = "1";
        }
        /// <summary>
        /// Internal Id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name for this rule.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description about this rule.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Rule version, one rule can be executed by several rule engines, a version defines witch rule engine to use.
        /// </summary>
        public string RuleVersion { get; set; }
        /// <summary>
        /// Indicates if the rule is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Indicates when starts to apply this rule, by default DateTime.MinValue.
        /// </summary>
        public System.DateTime BeginDate { get; set; }
        /// <summary>
        /// Indicates when starts to apply this rule, by default DateTime.MaxValue.
        /// </summary>
        public System.DateTime EndDate { get; set; }
        /// <summary>
        /// Indicates when was created this rule.
        /// </summary>
        public System.DateTime CreationDate { get; set; }
        /// <summary>
        /// Indicates criticity for this rule.
        /// </summary>
        public RuleLevel Level { get; set; }
    }
}
