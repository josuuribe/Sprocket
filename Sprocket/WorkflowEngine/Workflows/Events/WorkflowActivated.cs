using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
