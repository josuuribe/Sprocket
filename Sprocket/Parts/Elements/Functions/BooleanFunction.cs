using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions
{
    [DataContract]
    public abstract class BooleanFunction<T, U> : Function<T, U, bool>
        where T : IElement
    {
        private static BinaryOperator<T> and = new AndAlso<T>();
        private static BinaryOperator<T> or = new OrElse<T>();

        public static bool operator true(BooleanFunction<T, U> operatorTrue)
        {
            or = new Or<T>();
            return false;
        }

        public static bool operator false(BooleanFunction<T, U> operatorTrue)
        {
            and = new And<T>();
            return false;
        }

        public static BooleanFunction<T, U> operator &(BooleanFunction<T, U> operatorLeft, BooleanFunction<T, U> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            BooleanFunctionWrapper<T, U> wrapper = new BooleanFunctionWrapper<T, U>(clone);
            and = new AndAlso<T>();
            return wrapper;
        }

        public static BooleanFunction<T, U> operator |(BooleanFunction<T, U> operatorLeft, BooleanFunction<T, U> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            BooleanFunctionWrapper<T, U> wrapper = new BooleanFunctionWrapper<T, U>(clone);
            or = new OrElse<T>();
            return wrapper;
        }

        //public static LogicalOperator<T> operator !(BooleanFunction<T, U> operatorUnary)
        //{
        //    Not<T> ngte = new Not<T>();
        //    BoolWrapper<T> wrapper = new BoolWrapper<T>((IOperate<T, bool>)operatorUnary);
        //    ngte.Operator = wrapper;
        //    return ngte;
        //}

        //public static LogicalOperator<T> operator +(BooleanFunction<T, U> operatorUnary)
        //{
        //    Yes<T> ngte = new Yes<T>();
        //    BoolWrapper<T> wrapper = new BoolWrapper<T>((IOperate<T, bool>)operatorUnary);
        //    ngte.Operator = wrapper;
        //    return ngte;
        //}

        public static ExpressionOperator<T> operator /(BooleanFunction<T, U> operatorLeft, BooleanFunction<T, U> operatorRight)
        {
            IfThenElse<T> ite = new IfThenElse<T>();
            LogicalWrapper<T> wrapperThen = new LogicalWrapper<T>(operatorLeft);
            LogicalWrapper<T> wrapperElse = new LogicalWrapper<T>(operatorRight);
            ite.Then = wrapperThen;
            ite.Else = wrapperElse;
            return ite;
        }

        public static ExpressionOperator<T> operator %(bool boolConstant, BooleanFunction<T, U> operatorRight)
        {
            IfThen<T> it = new IfThen<T>();
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>(boolConstant);
            it.If = wrapper;
            it.Then = new LogicalWrapper<T>(operatorRight);
            return it;
        }

        public static ExpressionOperator<T> operator %(Operator<T> operatorIf, BooleanFunction<T, U> operatorRight)
        {
            IfThen<T> it = new IfThen<T>();
            it.If = operatorIf;
            it.Then = new LogicalWrapper<T>(operatorRight);
            return it;
        }

        public static ExpressionOperator<T> operator *(ExpressionOperator<T> expression, BooleanFunction<T, U> function)
        {
            Loop<T> loop = new Loop<T>();
            loop.If = expression;
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>(function);
            loop.Block = wrapper;
            return loop;
        }

        public static implicit operator Operator<T>(BooleanFunction<T, U> function)
        {
            LogicalWrapper<T> bw = new LogicalWrapper<T>(function);
            return bw;
        }
    }
}
