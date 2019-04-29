using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;
using System;
using System.Collections.Generic;

namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores information about process workflow, one workflow executes stages until finish or is necessary an external action.
    /// </summary>
    public class Workflow
    {
        #region ·   Fields  ·
        /// <summary>
        /// Events storing information about workflow status.
        /// </summary>
        public List<WorkflowEvent> WorkflowEvents { get; set; }
        #endregion

        #region ·   Constructors    ·
        /// <summary>
        /// Base constructor, creates a new Id.
        /// </summary>
        public Workflow()
        {
            this.WorkflowEvents = new List<WorkflowEvent>();
        }
        #endregion

        #region ·   Properties  ·
        /// <summary>
        /// Internal workflow id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name for this workflow.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Workflow type.
        /// </summary>
        public WorkflowType Type { get; set; }
        /// <summary>
        /// Workflow that is in the upper hierarchy.
        /// </summary>
        public Guid? ParentWorkflow { get; set; }
        #endregion

        #region ·   Methods ·
        /// <summary>
        /// Adds event indicating that this workflow has failed.
        /// </summary>
        /// <param name="stageId"></param>
        public void Failed(Stage stage)
        {

            this.WorkflowEvents.Add(new WorkflowProcessFailed(this, stage));
        }
        /// <summary>
        /// Adds event indicating that this workflow has finished correctly aplying a rule.
        /// </summary>
        /// <param name="stageId">Stage processed.</param>
        public void Positive(Stage stage)
        {
            this.WorkflowEvents.Add(new WorkflowProcessPositive(this, stage));
        }
        /// <summary>
        /// Adds event indicating that this workflow has executed correctly.
        /// </summary>
        public void Completed()
        {
            this.WorkflowEvents.Add(new WorkflowProcessCompleted(this));
        }
         /// <summary>
        /// Adds event indicating that this workflow has exited before stage processing.
        /// </summary>
        /// <param name="stageId"></param>
        public void Exited(Stage stage)
        {

            this.WorkflowEvents.Add(new WorkflowProcessExited(this, stage));
        }
        #endregion
    }
}
