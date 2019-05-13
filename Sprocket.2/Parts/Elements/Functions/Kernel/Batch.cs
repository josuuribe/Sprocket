using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    [DataContract]
    public sealed class Batch<T> : BooleanCommand<T>
        where T : IElement
    {
        [DataMember]
        public ArrayList Operates { get; set; }

        internal Batch()
        {
            this.Operates = new ArrayList();
        }

        public void Add<U>(IOperate<T, U> operate)
        {
            this.Operates.Add(operate);
        }

        public override bool Value(RuleElement<T> element)
        {
            var enumerator = this.Operates.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var parameter = enumerator.Current;
                    parameter.GetType().GetMethod("Value").Invoke(parameter, new object[] { element });
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