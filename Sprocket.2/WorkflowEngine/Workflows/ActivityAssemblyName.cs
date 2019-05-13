namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Class that stores information about assembly that stores type information.
    /// </summary>
    public class ActivityAssembly
    {
        public ActivityAssembly()
        {
            this.Version = 1;
        }
        /// <summary>
        /// Internal Id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Assembly name that stores type information.
        /// </summary>
        public string AssemblyName
        {
            get
            {
                return System.IO.Path.GetFileName(AssemblyPath);
            }
        }
        /// <summary>
        /// Full path to assembly.
        /// </summary>
        public string AssemblyPath { get; set; }
        /// <summary>
        /// Description information and other related stuff.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Assembly version.
        /// </summary>
        public int Version { get; set; }
    }
}
