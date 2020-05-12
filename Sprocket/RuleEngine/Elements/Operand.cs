using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements
{
    [DataContract]
    public abstract class Operand<TElement, TValue> : IOperand<TElement, TValue>
        where TElement : IElement
    {
        protected TElement element;
        public abstract TValue Value(TElement element);
        public virtual TElement Element => this.element;
        public Operand() { }
        public Operand(TElement element)
        {
            this.element = element;
        }

        //protected internal abstract TValue Process(RuleElement<TElement> rule);

        //public static Operator<TElement> operator >>(Operand<TElement, TValue> operate, int shift)
        //{
        //    var op = new OperateAsOperator<TElement, TValue>(operate);
        //    JMP<TElement> jmp = new JMP<TElement>(op, Math.Abs(shift));
        //    return jmp;
        //}

        //public static Operator<TElement> operator <<(Operand<TElement, TValue> operate, int shift)
        //{
        //    var op = new OperateAsOperator<TElement, TValue>(operate);
        //    JMP<TElement> jmp = new JMP<TElement>(op, Math.Abs(shift) * -1);
        //    return jmp;
        //}

        public static Operator<TElement> operator >=(Operand<TElement, TValue> operateLeft, IComparable operateRight)
        {
            GreaterThanOrEquals<TElement, IComparable> gtoe = new GreaterThanOrEquals<TElement, IComparable>();
            gtoe.OperateLeft = new OperateAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsOperate<TElement, IComparable>(operateRight);
            return gtoe;
        }

        public static Operator<TElement> operator <=(Operand<TElement, TValue> operateLeft, IComparable operateRight)
        {
            LessThanOrEquals<TElement, IComparable> ltoe = new LessThanOrEquals<TElement, IComparable>();
            ltoe.OperateLeft = new OperateAsComparable<TElement, TValue>(operateLeft);
            ltoe.OperateRight = new ValueAsOperate<TElement, IComparable>(operateRight);
            return ltoe;
        }

        public static Operator<TElement> operator >(Operand<TElement, TValue> operateLeft, IComparable operateRight)
        {
            GreaterThan<TElement, IComparable> gtoe = new GreaterThan<TElement, IComparable>();
            gtoe.OperateLeft = new OperateAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsOperate<TElement, IComparable>(operateRight);
            return gtoe;
        }

        public static Operator<TElement> operator <(Operand<TElement, TValue> operateLeft, IComparable operateRight)
        {
            LessThan<TElement, IComparable> gtoe = new LessThan<TElement, IComparable>();
            gtoe.OperateLeft = new OperateAsComparable<TElement, TValue>(operateLeft);
            gtoe.OperateRight = new ValueAsOperate<TElement, IComparable>(operateRight);
            return gtoe;
        }

        public static implicit operator TValue(Operand<TElement, TValue> operate)
        {
            return operate.Value(operate.element);
        }
    }
}
