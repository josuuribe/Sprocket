using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System;
using System.Collections;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions.Kernel
{
    [DataContract]
    public sealed class Batch<TElement> : BooleanCommand<TElement>
        where TElement : IElement
    {
        [DataMember]
        public ArrayList Operates { get; set; }

        internal Batch()
        {
            this.Operates = new ArrayList();
        }

        public void Add<U>(IOperate<TElement, U> operate)
        {
            this.Operates.Add(operate);
        }

        public void Add<U>(Batch<TElement> batch)
        {
            this.Operates.AddRange(batch.Operates);
        }

        public override bool Value(RuleElement<TElement> element)
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

        public static Batch<TElement> operator +(Batch<TElement> batchLeft, Batch<TElement> batchRight)
        {
            Batch<TElement> batch = new Batch<TElement>();
            batch.Add(batchLeft);
            batch.Add(batchRight);
            return batch;
        }

        public static implicit operator Operator<TElement>(Batch<TElement> batch)
        {
            return new OperateWrapper<TElement>(batch);
        }
    }
}