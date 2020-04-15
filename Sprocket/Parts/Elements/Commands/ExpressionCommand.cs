using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using System;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    public abstract class ExpressionCommand<T, U> : Command<T, U>
        where T : IElement
        where U : IComparable<U>
    {
        public static Operator<T> operator >=(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            GreaterThanOrEquals<T, U> ngte = new GreaterThanOrEquals<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }

        public static Operator<T> operator <=(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            LessThanOrEquals<T, U> ngte = new LessThanOrEquals<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }

        public static Operator<T> operator >(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            GreaterThan<T, U> ngte = new GreaterThan<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }

        public static Operator<T> operator <(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            LessThan<T, U> ngte = new LessThan<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }

        public static Operator<T> operator ==(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            Equals<T, U> ngte = new Equals<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }

        public static Operator<T> operator !=(ExpressionCommand<T, U> operateLeft, U operateRight)
        {
            NotEquals<T, U> ngte = new NotEquals<T, U>();
            ngte.OperateLeft = operateLeft;
            ValueWrapper<T, U> wrapper = new ValueWrapper<T, U>(operateRight);
            ngte.OperateRight = wrapper;
            return ngte;
        }
    }
}
