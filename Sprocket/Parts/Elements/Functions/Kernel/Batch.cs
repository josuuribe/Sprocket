using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    [DataContract]
    public sealed class Batch<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        [DataMember]
        public ArrayList Operates { get; set; }

        internal Batch() 
        {
            this.Operates = new ArrayList();
        }

        public void Add<TValue>(Operate<TElement, TValue> operate)
        {
            this.Operates.Add(operate);
        }

        protected internal override bool Process(RuleElement<TElement> element)
        {
            //var enumerator = this.Operates.GetEnumerator();
            try
            {
                foreach(var operate in this.Operates)
                {
                    ((dynamic)operate).Process(element);
                }
                //while (enumerator.MoveNext())
                //{
                //    var parameter = enumerator.Current;
                //    parameter.GetType().GetMethod("Value").Invoke(parameter, new object[] { element });
                //}
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        //public static implicit operator Operator<TElement>(Batch<TElement> batch)
        //{
        //    return new OperateAsOperator<TElement>(batch);
        //}
    }
}