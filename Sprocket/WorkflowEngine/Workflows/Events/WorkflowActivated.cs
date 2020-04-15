namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    //[Serializable]
    public class WorkflowActivated : WorkflowEvent
    {
        public WorkflowActivated() { }

        public WorkflowActivated(Workflow workflow)
            : base(workflow)
        {

        }
    }
}
