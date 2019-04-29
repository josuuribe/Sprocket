using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;

namespace RaraAvis.Sprocket.Parts.Elements.Functions
{
    public abstract class ExpressionFunction<T, U, V> : Function<T, U, V>
        where T : IElement
        where V : IComparable<V>
    {
        public static ExpressionOperator<T> operator >(ExpressionFunction<T, U, V> operatorLeft, ExpressionFunction<T, U, V> operatorRight)
        {
            GreaterThan<T, V> ogt = new GreaterThan<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator <(ExpressionFunction<T, U, V> operatorLeft, ExpressionFunction<T, U, V> operatorRight)
        {
            LessThan<T, V> olt = new LessThan<T, V>();
            olt.OperateLeft = operatorLeft;
            olt.OperateRight = operatorRight;
            return olt;
        }

        public static ExpressionOperator<T> operator >=(ExpressionFunction<T, U, V> operatorLeft, ExpressionFunction<T, U, V> operatorRight)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator <=(ExpressionFunction<T, U, V> operatorLeft, ExpressionFunction<T, U, V> operatorRight)
        {
            LessThanOrEquals<T, V> olt = new LessThanOrEquals<T, V>();
            olt.OperateLeft = operatorLeft;
            olt.OperateRight = operatorRight;
            return olt;
        }

        public static ExpressionOperator<T> operator >(ExpressionFunction<T, U, V> operatorLeft, V right)
        {
            GreaterThan<T, V> ogt = new GreaterThan<T, V>();
            ogt.OperateLeft = operatorLeft;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(right);
            ogt.OperateRight = wrapper;
            return ogt;
        }

        public static ExpressionOperator<T> operator <(ExpressionFunction<T, U, V> operatorLeft, V right)
        {
            LessThan<T, V> olt = new LessThan<T, V>();
            olt.OperateLeft = operatorLeft;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(right);
            olt.OperateRight = wrapper;
            return olt;
        }

        public static ExpressionOperator<T> operator >=(ExpressionFunction<T, U, V> operatorLeft, V right)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ogt.OperateLeft = operatorLeft;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(right);
            ogt.OperateRight = wrapper;
            return ogt;
        }

        public static ExpressionOperator<T> operator <=(ExpressionFunction<T, U, V> operatorLeft, V right)
        {
            LessThanOrEquals<T, V> olt = new LessThanOrEquals<T, V>();
            olt.OperateLeft = operatorLeft;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(right);
            olt.OperateRight = wrapper;
            return olt;
        }

        public static ExpressionOperator<T> operator >(V left, ExpressionFunction<T, U, V> operatorRight)
        {
            GreaterThan<T, V> ogt = new GreaterThan<T, V>();
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(left);
            ogt.OperateLeft = wrapper;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator <(V left, ExpressionFunction<T, U, V> operatorRight)
        {
            LessThan<T, V> ogt = new LessThan<T, V>();
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(left);
            ogt.OperateLeft = wrapper;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator <=(V left, ExpressionFunction<T, U, V> operatorRight)
        {
            LessThanOrEquals<T, V> ogt = new LessThanOrEquals<T, V>();
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(left);
            ogt.OperateLeft = wrapper;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator >=(V left, ExpressionFunction<T, U, V> operatorRight)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(left);
            ogt.OperateLeft = wrapper;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator >(ExpressionFunction<T, U, V> operatorLeft, ExpressionCommand<T, V> right)
        {
            GreaterThan<T, V> ogt = new GreaterThan<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = right;
            return ogt;
        }

        public static ExpressionOperator<T> operator <(ExpressionFunction<T, U, V> operatorLeft, ExpressionCommand<T, V> right)
        {
            LessThan<T, V> olt = new LessThan<T, V>();
            olt.OperateLeft = operatorLeft;
            olt.OperateRight = right;
            return olt;
        }

        public static ExpressionOperator<T> operator >=(ExpressionFunction<T, U, V> operateLeft, ExpressionCommand<T, V> operatorRight)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ogt.OperateLeft = operateLeft;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator <=(ExpressionFunction<T, U, V> operateLeft, ExpressionCommand<T, V> operatorRight)
        {
            LessThanOrEquals<T, V> ogt = new LessThanOrEquals<T, V>();
            ogt.OperateLeft = operateLeft;
            ogt.OperateRight = operatorRight;
            return ogt;
        }

        public static ExpressionOperator<T> operator >(ExpressionCommand<T, V> operatorLeft, ExpressionFunction<T, U, V> right)
        {
            GreaterThan<T, V> ogt = new GreaterThan<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = right;
            return ogt;
        }

        public static ExpressionOperator<T> operator <(ExpressionCommand<T, V> operatorLeft, ExpressionFunction<T, U, V> right)
        {
            LessThan<T, V> ogt = new LessThan<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = right;
            return ogt;
        }

        public static ExpressionOperator<T> operator >=(ExpressionCommand<T, V> operatorLeft, ExpressionFunction<T, U, V> right)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = right;
            return ogt;
        }

        public static ExpressionOperator<T> operator <=(ExpressionCommand<T, V> operatorLeft, ExpressionFunction<T, U, V> right)
        {
            GreaterThanOrEquals<T, V> ogt = new GreaterThanOrEquals<T, V>();
            ogt.OperateLeft = operatorLeft;
            ogt.OperateRight = right;
            return ogt;
        }

        public static ExpressionFunction<T, U, V> operator -(ExpressionFunction<T, U, V> function, U parameter)
        {
            function.Parameters = parameter;
            return function;
        }

        //public static implicit operator ArithmeticFunction<T, U, V>(Function<T, U, IComparable<V>> function)
        //{
        //    return function;
        //}
    }
}
