namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Base information about workflow.
    /// </summary>
    //[Serializable]
    public abstract class WorkflowEvent
    {
        public WorkflowEvent() { }

        public WorkflowEvent(Workflow workflow)
        {
            this.Workflow = workflow;
        }
        /// <summary>
        /// Workflow in process.
        /// </summary>
        public Workflow Workflow { get; set; }
    }
}
