namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores a executed event.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessCompleted : WorkflowProcessResponse
    {
        public WorkflowProcessCompleted() { }

        public WorkflowProcessCompleted(Workflow workflow)
            : base(workflow, null)
        {

        }
    }
}
