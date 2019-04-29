namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores a deactivated event.
    /// </summary>
    public class WorkflowDeactivated : WorkflowEvent
    {
        public WorkflowDeactivated() { }

        public WorkflowDeactivated(Workflow workflow)
            : base(workflow)
        {

        }
    }
}
