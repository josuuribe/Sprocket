using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public abstract class ExpressionCommand<TElement, TValue> : Command<TElement, TValue>
        where TElement : IElement
        where TValue : IComparable<TValue>
    {
        public ExpressionCommand() : base() { }
        public ExpressionCommand(TElement p) : base(p) { }
        public static Operator<TElement> operator >=(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            GreaterThanOrEquals<TElement, TValue> ngte = new GreaterThanOrEquals<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }

        public static Operator<TElement> operator <=(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            LessThanOrEquals<TElement, TValue> ngte = new LessThanOrEquals<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }

        public static Operator<TElement> operator >(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            GreaterThan<TElement, TValue> ngte = new GreaterThan<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }

        public static Operator<TElement> operator <(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            LessThan<TElement, TValue> ngte = new LessThan<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }

        public static Operator<TElement> operator ==(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            Equals<TElement, TValue> ngte = new Equals<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }

        public static Operator<TElement> operator !=(ExpressionCommand<TElement, TValue> operateLeft, TValue operateRight)
        {
            NotEquals<TElement, TValue> ngte = new NotEquals<TElement, TValue>();
            ngte.OperateLeft = operateLeft;
            ngte.OperateRight = new ValueAsOperate<TElement, TValue>(operateRight);
            return ngte;
        }
    }
}
