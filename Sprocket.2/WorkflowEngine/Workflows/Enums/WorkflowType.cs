namespace RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums
{
    /// <summary>
    /// Workflow type, it indicates if must be processed before of after element processes dependant.
    /// </summary>
    public enum WorkflowType
    {
        PREPROCESS = 1,
        INPROCESS = 2,
        POSTPROCESS = 3
    }
}
