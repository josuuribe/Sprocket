namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Workflow that stores an exited event.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessExited : WorkflowProcessResponse
    {
        public WorkflowProcessExited() { }

        public WorkflowProcessExited(Workflow workflow, Stage stage)
            : base(workflow, stage)
        {
            
        }
    }
}
