namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Class that stores information about workflow processing.
    /// </summary>
    //[Serializable]
    public abstract class WorkflowProcessResponse : WorkflowEvent
    {
        public WorkflowProcessResponse() { }

        public WorkflowProcessResponse(Workflow workflow, Stage stage)
        {
            this.ActualStage = stage;
        }
        /// <summary>
        /// Stage that is actually in use.
        /// </summary>
        public Stage ActualStage
        { get; set; }
    }
}
