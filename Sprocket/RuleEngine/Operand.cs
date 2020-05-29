using RaraAvis.Sprocket.RuleEngine.Casts;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.RuleEngine.Operators.ComparisonOperators;
using RaraAvis.Sprocket.RuleEngine.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine
{
    /// <summary>
    /// Operand to execute on <see cref="TTarget"/> and return a value.
    /// </summary>
    /// <typeparam name="TTarget">Target type to use.</typeparam>
    /// <typeparam name="TValue">Value type returned.</typeparam>
    [DataContract]
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public abstract class Operand<TTarget, TValue> : IOperand<TTarget, TValue>
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
        where TTarget : notnull
    {
        [MaybeNull]
        protected TTarget target;
        protected Operand<TTarget, TValue> next = null!;

        /// <summary>
        /// Next value to be executed in this execution chain.
        /// </summary>
        [DataMember]
        public virtual Operand<TTarget, TValue> Next
        {
            get
            {
                return next ?? new Noop();
            }
            protected set
            {
                Operand<TTarget, TValue> next = this;
                while (!(next.Next is Noop))
                {
                    next = next.Next;
                }
                next.next = value;
            }
        }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        protected Operand()
        {
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        }

        protected Operand([NotNull]TTarget target)
        {
            this.target = target;
        }

        /// <inheritdoc/>
        [return: MaybeNull]
        public abstract TValue Process(TTarget target);

        public static Operator<TTarget> operator *(Operand<TTarget, bool> condition, Operand<TTarget, TValue> operand)
        {
            Loop<TTarget, TValue> loop = new Loop<TTarget, TValue>(condition, operand);
            return loop;
        }

        public static Operator<TTarget> operator *(Operator<TTarget> condition, Operand<TTarget, TValue> operand)
        {
            Loop<TTarget, TValue> loop = new Loop<TTarget, TValue>(condition, operand);
            return loop;
        }

        public static Operator<TTarget> operator ==(Operand<TTarget, TValue> command, TValue o)
        {
            var left = new ValueAsOperand<TTarget, TValue>(o);
            Equals<TTarget, TValue> oe = new Equals<TTarget, TValue>(left, command);
            return oe;
        }

        public static Operator<TTarget> operator !=(Operand<TTarget, TValue> command, TValue o)
        {
            var right = new ValueAsOperand<TTarget, TValue>(o);
            NotEquals<TTarget, TValue> oe = new NotEquals<TTarget, TValue>(command, right);
            return oe;
        }

        public static Operand<TTarget, TValue> operator /(Operand<TTarget, TValue> left,  Operand<TTarget, TValue> right)
        {
            left.Next = right;
            return left;
        }

        public static Operator<TTarget> operator >=(Operand<TTarget, TValue> operateLeft, TValue operateRight)
        {
            var left = new OperandAsComparable<TTarget, TValue>(operateLeft);
            var right = new ValueAsComparable<TTarget, TValue>(operateRight);
            GreaterThanOrEquals<TTarget, IComparable> gtoe = new GreaterThanOrEquals<TTarget, IComparable>(left, right);
            return gtoe;
        }

        public static Operator<TTarget> operator <=(Operand<TTarget, TValue> operateLeft, TValue operateRight)
        {
            var left = new OperandAsComparable<TTarget, TValue>(operateLeft);
            var right = new ValueAsComparable<TTarget, TValue>(operateRight);
            LessThanOrEquals<TTarget, IComparable> ltoe = new LessThanOrEquals<TTarget, IComparable>(left, right);
            return ltoe;
        }

        public static Operator<TTarget> operator >(Operand<TTarget, TValue> operateLeft,  TValue operateRight)
        {
            var left = new OperandAsComparable<TTarget, TValue>(operateLeft);
            var right = new ValueAsComparable<TTarget, TValue>(operateRight);
            GreaterThan<TTarget, IComparable> gtoe = new GreaterThan<TTarget, IComparable>(left, right);
            return gtoe;
        }

        public static Operator<TTarget> operator <(Operand<TTarget, TValue> operateLeft, TValue operateRight)
        {
            var left = new OperandAsComparable<TTarget, TValue>(operateLeft);
            var right = new ValueAsComparable<TTarget, TValue>(operateRight);
            LessThan<TTarget, IComparable> gtoe = new LessThan<TTarget, IComparable>(left, right);
            return gtoe;
        }

        public static Operator<TTarget> operator +(Operand<TTarget, TValue> operand)
        {
            var @operator = new OperandAsOperator<TTarget, TValue>(operand);
            return new True<TTarget>(@operator);
        }

        public static Operator<TTarget> operator -(Operand<TTarget, TValue> operand)
        {
            var @operator = new OperandAsOperator<TTarget, TValue>(operand);
            return new False<TTarget>(@operator);
        }

        public static implicit operator TValue(Operand<TTarget, TValue> operate)
        {
            return operate.Process(operate.target);
        }

        public static implicit operator Operand<TTarget, TValue>(Expression<Func<Rule<TTarget>, TValue>> operand)
        {
            ExpressionAsOperand<TTarget, TValue> brk = new ExpressionAsOperand<TTarget, TValue>(operand);
            return brk;
        }

        [DataContract]
        internal class Noop : Operand<TTarget, TValue>
        {
            public override Operand<TTarget, TValue> Next
            {
                get
                {
                    return this;
                }
            }

            public override TValue Process(TTarget target)
            {
                return default!;
            }
        }
    }
}
