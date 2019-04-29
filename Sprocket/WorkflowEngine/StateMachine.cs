using RaraAvis.Sprocket.WorkflowEngine.Workflows.Enums;

namespace RaraAvis.Sprocket.WorkflowEngine
{
    /// <summary>
    /// Class that stores rule internal state.
    /// </summary>
    internal class StateMachine
    {
        /// <summary>
        /// Status after execute stage.
        /// </summary>
        public StageStatus Status { get; set; }
        /// <summary>
        /// Next stage to execute.
        /// </summary>
        public string NextStage { get; set; }
    }
}
