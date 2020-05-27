using System.Collections.Generic;

namespace RaraAvis.Sprocket.WorkflowEngine.Entities
{
    /// <summary>
    /// Sprocket configuration.
    /// </summary>
    internal sealed class SprocketConfiguration
    {
        /// <summary>
        /// Paths to be used to search for serialization types and rule engine service, the paths can be relatived to execution folder or absolute folders.
        /// </summary>
        public List<string> Paths { get; set; } = new List<string>();
        /// <summary>
        /// Serialization format, possible values:
        /// <list type="bullet">
        /// <item>json: using <see cref="Newtonsoft.Json.JsonConvert"/> library.</item>
        /// <item>xml: using <see cref="System.Runtime.Serialization.DataContractSerializer"/>.</item>
        /// </list>
        /// Any other value will use a NullSerializer that returns nothing.
        /// </summary>
        public string SerializationFormat { get; set; } = "xml";
    }
}
