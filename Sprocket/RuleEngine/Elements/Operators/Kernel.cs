using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operators
{
    [DataContract]
    internal abstract class Kernel<TElement> : Operator<TElement>
        where TElement : IElement
    {

        [DataMember]
        public virtual Operator<TElement> Operator { get; set; }

        public Kernel(Operator<TElement> @operator)
        {
            this.Operator = @operator;
        }
    }
}
