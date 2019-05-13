using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;

namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Class that stores information about workflow processing.
    /// </summary>
    //[Serializable]
    public class WorkflowProcessResponse : WorkflowEvent
    {
        public WorkflowProcessResponse() { }

        public WorkflowProcessResponse(Workflow workflow, Stage stage, WorkflowStatus workflowStatus)
        {
            this.Stage = stage;
            this.WorkflowStatus = workflowStatus;
        }
        /// <summary>
        /// Stage that is actually in use.
        /// </summary>
        public Stage Stage
        { get; private set; }

        public WorkflowStatus WorkflowStatus { get; private set; }
    }
}
