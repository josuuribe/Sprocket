using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Flows
{
    [DataContract]
    internal sealed class End<TElement> : IOperand<TElement, bool>
        where TElement : IElement
    {
        //private readonly ICode next;
        //[JsonIgnore]
        //[DataMember]
        //public ICode Previous { get; set; }
        //[DataMember]
        //[JsonIgnore]
        //public ICode Next
        //{
        //    get
        //    {
        //        return next;
        //    }
        //    set { }
        //}

        public End()
        {
            //this.next = this;
        }

        public bool Process(Rule<TElement> rule)
        {
            return true;
        }
    }
}
