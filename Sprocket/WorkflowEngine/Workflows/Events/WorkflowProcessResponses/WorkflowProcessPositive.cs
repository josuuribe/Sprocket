namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores a positive event.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessPositive : WorkflowProcessResponse
    {
        public WorkflowProcessPositive() { }
        public WorkflowProcessPositive(Workflow workflow, Stage stage)
            : base(workflow, stage)
        { }
    }
}
