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
    internal sealed class Begin<TElement> : IOperand<TElement, bool>, ICode
        where TElement : IElement
    {
        private readonly ICode previous;

        [DataMember]
        [JsonIgnore]
        public ICode Previous
        {
            get
            {
                return previous;
            }
            set { }
        }

        [DataMember]
        [JsonIgnore]
        public ICode Next
        {
            get; set;
        }

        public Begin()
        {
            this.previous = this;
        }


        public bool Process(Rule<TElement> rule)
        {
            return true;
        }
    }
}
