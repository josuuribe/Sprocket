using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates.Kernel
{
    [DataContract]
    public sealed class Batch<TElement> : Operator<TElement>
        where TElement : IElement
    {
        [DataMember]
        public ArrayList Operates { get; set; }

        internal Batch() 
        {
            this.Operates = new ArrayList();
        }

        public void Add<TValue>(IOperand<TElement, TValue> operate)
        {
            this.Operates.Add(operate);
        }

        protected internal override bool Match(Rule<TElement> rule)
        {
            try
            {
                foreach (var operate in this.Operates)
                {
                    ((dynamic)operate).Value(rule);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}