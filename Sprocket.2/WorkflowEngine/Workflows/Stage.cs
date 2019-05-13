using System.Collections.Generic;

namespace RaraAvis.Sprocket.WorkflowEngine.Workflows
{
    /// <summary>
    /// Stage that is part of a workflow.
    /// </summary>
    public class Stage
    {

        #region ·   Constructor ·
        public Stage()
        {
            this.ActivitiesAssemblyNames = new List<ActivityAssembly>();
        }
        #endregion

        #region ·   Properties  ·
        /// <summary>
        /// Internal Id.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Name for this stage.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Assemblies that store information about types inside rules.
        /// </summary>
        public virtual List<ActivityAssembly> ActivitiesAssemblyNames { get; set; }
        /// <summary>
        /// XML that stores information about one stage.
        /// </summary>
        public string XMLStage { get; set; }    
        #endregion
    }
}
