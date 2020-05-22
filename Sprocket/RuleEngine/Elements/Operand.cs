using Newtonsoft.Json;
using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Elements.Operands;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements
{
    [DataContract]
    [JsonObject(IsReference = true)]
    public abstract class Operand<TElement, TValue> : IOperand<TElement, TValue>, ICode
        where TElement : IElement
    {
        protected TElement element;
        public abstract TValue Process(Rule<TElement> element);
        protected ICode next = null;

        [DataMember]
        public virtual ICode Previous { get; set; }
        [DataMember]
        public virtual ICode Next
        {
            get
            {
                //return next ?? new End<TElement>();
                return next ?? new Noop<TElement>();
            }
            set
            {
                ICode next = this;
                while (!(next.Next is Noop<TElement>))
                {
                    next = next.Next;
                }
                (next as Operand<TElement, TValue>).next = value;
                //(next as Operand<TElement, TValue>).Next.Previous = next;
            }
        }

        //public IEnumerator<IOperand<TElement, TValue>> GetEnumerator()
        //{
        //    return new Roamable<IOperand<TElement, TValue>>(this);
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}

        public Operand()
        {
            this.next = new Noop<TElement>();
            //this.Previous = new Begin<TElement>();
            //this.Previous.Next = next;
        }

        public Operand(TElement element) : this()
        {
            this.element = element;
        }

        //protected internal abstract TValue Process(RuleElement<TElement> rule);

        public static Operator<TElement> operator *(Operand<TElement, bool> condition, Operand<TElement, TValue> operand)
        {
            Loop<TElement, TValue> loop = new Loop<TElement, TValue>();
            loop.Condition = condition;
            loop.Block = operand;
            return loop;
        }

        public static Operator<TElement> operator *(Operator<TElement> expression, Operand<TElement, TValue> operand)
        {
            Loop<TElement, TValue> loop = new Loop<TElement, TValue>();
            loop.Condition = expression;
            loop.Block = operand;
            return loop;
        }

        public static Operator<TElement> operator ==(Operand<TElement, TValue> command, TValue o)
        {
            Equals<TElement, TValue> oe = new Equals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueAsOperand<TElement, TValue> wrapper = new ValueAsOperand<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Operator<TElement> operator !=(Operand<TElement, TValue> command, TValue o)
        {
            NotEquals<TElement, TValue> oe = new NotEquals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueAsOperand<TElement, TValue> wrapper = new ValueAsOperand<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }



        public static Operand<TElement, TValue> operator /(Operand<TElement, TValue> left, Operand<TElement, TValue> right)
        {
            left.Next = right;
            return left;
        }

        public static Operator<TElement> operator >=(Operand<TElement, TValue> operateLeft, TValue operateRight)
        {
            GreaterThanOrEquals<TElement, IComparable> gtoe = new GreaterThanOrEquals<TElement, IComparable>();
            gtoe.OperateLeft = new OperandAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsComparable<TElement, TValue>(operateRight);
            return gtoe;
        }

        public static Operator<TElement> operator <=(Operand<TElement, TValue> operateLeft, TValue operateRight)
        {
            LessThanOrEquals<TElement, IComparable> ltoe = new LessThanOrEquals<TElement, IComparable>();
            ltoe.OperateLeft = new OperandAsComparable<TElement, TValue>(operateLeft);
            ltoe.OperateRight = new ValueAsComparable<TElement, TValue>(operateRight);
            return ltoe;
        }

        public static Operator<TElement> operator >(Operand<TElement, TValue> operateLeft, TValue operateRight)
        {
            GreaterThan<TElement, IComparable> gtoe = new GreaterThan<TElement, IComparable>();
            gtoe.OperateLeft = new OperandAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsComparable<TElement, TValue>(operateRight);
            return gtoe;
        }

        public static Operator<TElement> operator <(Operand<TElement, TValue> operateLeft, TValue operateRight)
        {
            LessThan<TElement, IComparable> gtoe = new LessThan<TElement, IComparable>();
            gtoe.OperateLeft = new OperandAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsComparable<TElement, TValue>(operateRight);
            return gtoe;
        }

        public static Operator<TElement> operator +(Operand<TElement, TValue> operand)
        {
            var @operator = new OperandAsOperator<TElement, TValue>(operand);
            return new True<TElement>(@operator);
        }

        public static Operator<TElement> operator -(Operand<TElement, TValue> operand)
        {
            var @operator = new OperandAsOperator<TElement, TValue>(operand);
            return new False<TElement>(@operator);
        }

        public static Operator<TElement> operator !(Operand<TElement, TValue> operand)
        {
            Operator<TElement> op = (operand as Operand<TElement, bool>);
            return new Not<TElement>(op);
        }

        public static implicit operator TValue(Operand<TElement, TValue> operate)
        {
            return operate.Process(operate.element);
        }

        public static implicit operator Operand<TElement, TValue>(Expression<Func<Rule<TElement>, TValue>> operand)
        {
            PointerToFunc<TElement, TValue> brk = new PointerToFunc<TElement, TValue>(operand);
            return brk;
        }
    }
}
