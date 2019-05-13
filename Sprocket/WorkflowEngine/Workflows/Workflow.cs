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
        public WorkflowProcessResponse WorkflowEvent { get; set; }
        #endregion

        #region ·   Constructors    ·
        /// <summary>
        /// Base constructor.
        /// </summary>
        public Workflow()
        {
            this.Id = Guid.NewGuid();
            this.WorkflowEvent = new WorkflowProcessResponse(this, null, WorkflowStatus.None);
        }
        #endregion

        #region ·   Properties  ·
        /// <summary>
        /// Internal workflow id.
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// Name for this workflow.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Workflow type.
        /// </summary>
        //public WorkflowType Type { get; set; }
        /// <summary>
        /// Workflow that is in the upper hierarchy.
        /// </summary>
        //public Guid? ParentWorkflow { get; set; }
        #endregion

        #region ·   Methods ·
        /// <summary>
        /// Adds event indicating that this workflow has failed.
        /// </summary>
        /// <param name="stageId"></param>
        //public void Failed(Stage stage)
        //{
        //    this.WorkflowEvent = new WorkflowProcessResponse(this, stage, WorkflowStatus.Error);
        //}
        /// <summary>
        /// Adds event indicating that this workflow has finished correctly aplying a rule.
        /// </summary>
        /// <param name="stageId">Stage processed.</param>
        //public void Positive(Stage stage)
        //{
        //    this.WorkflowEvent = new WorkflowProcessPositive(this, stage);
        //}
        /// <summary>
        /// Adds event indicating that this workflow has finished correctly with negative result.
        /// </summary>
        /// <param name="stage"></param>
        //public void Incompleted(Stage stage)
        //{
        //    this.WorkflowEvent = new WorkflowProcessResponse(this, stage, WorkflowStatus.Incompleted);
        //}
        ///// <summary>
        ///// Adds event indicating that this workflow has executed correctly.
        ///// </summary>
        //public void Completed(Stage stage)
        //{
        //    this.WorkflowEvent = new WorkflowProcessResponse(this, stage, WorkflowStatus.Completed);
        //}
        ///// <summary>
        ///// Adds event indicating that this workflow has exited before stage processing.
        ///// </summary>
        ///// <param name="stageId"></param>
        //public void Exited(Stage stage)
        //{
        //    this.WorkflowEvent = new WorkflowProcessResponse(this, stage, WorkflowStatus.Exited);
        //}
        #endregion
    }
}
