namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores a failed event.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessFailed : WorkflowProcessResponse
    {
        public WorkflowProcessFailed() { }

        public WorkflowProcessFailed(Workflow workflow, Stage stage)
            : base(workflow, stage)
        { }
    }
}
