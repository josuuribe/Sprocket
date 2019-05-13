namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores a positive event.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessIncompleted : WorkflowProcessResponse
    {
        public WorkflowProcessIncompleted() { }
        public WorkflowProcessIncompleted(Workflow workflow, Stage stage)
            : base(workflow, stage)
        { }
    }
}
