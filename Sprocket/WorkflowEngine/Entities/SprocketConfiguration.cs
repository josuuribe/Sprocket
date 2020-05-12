using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;

namespace RaraAvis.Sprocket.WorkflowEngine.Entities
{
    public class SprocketConfiguration
    {
        public List<string> Paths { get; set; } = new List<string>();
        public string SerializationFormat { get; set; }
    }
}
