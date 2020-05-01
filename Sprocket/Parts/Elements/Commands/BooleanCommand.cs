using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    [DataContract]
    public abstract class BooleanCommand<TElement> : Command<TElement, bool>
        where TElement : IElement
    {
        public BooleanCommand() { }
        public BooleanCommand(TElement p) : base(p) { }
        //private static BinaryOperator<T> and = new AndAlso<T>();
        //private static BinaryOperator<T> or = new OrElse<T>();

        //public static bool operator true(BooleanCommand<T> operatorTrue)
        //{
        //    or = new Or<T>();
        //    return false;
        //}

        //public static bool operator false(BooleanCommand<T> operatorTrue)
        //{
        //    and = new And<T>();
        //    return false;
        //}

        //public static BooleanCommand<T> operator &(BooleanCommand<T> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    AndAlso<T> aa = new AndAlso<T>();
        //    aa.OperatorLeft = operatorLeft;
        //    aa.OperatorRight = operatorRight;
        //    BooleanCommandWrapper<T> bcw = new BooleanCommandWrapper<T>(aa);
        //    return bcw;
        //}

        //public static BooleanCommand<T> operator |(BooleanCommand<T> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
        //    clone.OperatorLeft = operatorLeft;
        //    clone.OperatorRight = operatorRight;
        //    or = new OrElse<T>();
        //    BooleanCommandWrapper<T> bcw = new BooleanCommandWrapper<T>(or);
        //    return bcw;
        //}

        //public static ExpressionOperator<T> operator &(Command<T, bool> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
        //    LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
        //    clone.OperatorLeft = bwLeft;
        //    clone.OperatorRight = operatorRight;
        //    and = new AndAlso<T>();
        //    return clone;
        //}

        //public static ExpressionOperator<T> operator &(BooleanCommand<T> operatorLeft, Command<T, bool> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
        //    clone.OperatorLeft = operatorLeft;
        //    LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
        //    clone.OperatorRight = bwRight;
        //    and = new AndAlso<T>();
        //    return clone;
        //}

        //public static ExpressionOperator<T> operator |(Command<T, bool> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
        //    LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
        //    clone.OperatorLeft = bwLeft;
        //    LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
        //    clone.OperatorRight = bwRight;
        //    or = new OrElse<T>();
        //    return clone;
        //}

        //public static ExpressionOperator<T> operator |(BooleanCommand<T> operatorLeft, Command<T, bool> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
        //    LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
        //    clone.OperatorLeft = bwLeft;
        //    LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
        //    clone.OperatorRight = bwRight;
        //    or = new OrElse<T>();
        //    return clone;
        //}

        //public static AndAlso<T> operator &(BooleanCommand<T> operatorLeft, Operator<T> operatorRight)
        //{
        //    AndAlso<T> clone = (AndAlso<T>)and.Clone();
        //    LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
        //    clone.OperatorLeft = bwLeft;
        //    clone.OperatorRight = operatorRight;
        //    and = new AndAlso<T>();
        //    return clone;
        //}

        //public static ExpressionOperator<T> operator |(BooleanCommand<T> operatorLeft, Operator<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
        //    LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
        //    clone.OperatorLeft = bwLeft;
        //    clone.OperatorRight = operatorRight;
        //    or = new OrElse<T>();
        //    return clone;
        //}

        //public static BooleanCommand<T> operator &(Operator<T> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
        //    clone.OperatorLeft = operatorLeft;
        //    LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
        //    clone.OperatorRight = bwRight;
        //    and = new AndAlso<T>();
        //    BooleanCommandWrapper<T> bcw = new BooleanCommandWrapper<T>(and);
        //    return bcw;
        //}

        //public static BooleanCommand<T> operator |(Operator<T> operatorLeft, BooleanCommand<T> operatorRight)
        //{
        //    BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
        //    clone.OperatorLeft = operatorLeft;
        //    LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
        //    clone.OperatorRight = bwRight;
        //    or = new OrElse<T>();
        //    BooleanCommandWrapper<T> bcw = new BooleanCommandWrapper<T>(or);
        //    return bcw;
        //}

        public static Operator<TElement> operator !(BooleanCommand<TElement> operatorUnary)
        {
            Not<TElement> not = new Not<TElement>();
            not.Operator = (Operator<TElement>)operatorUnary;
            return not;
        }

        public static Operator<TElement> operator +(BooleanCommand<TElement> operatorUnary)
        {
            OperateAsOperator<TElement, bool> ow = new OperateAsOperator<TElement, bool>(operatorUnary);
            return ow;
        }

        //public static ExpressionOperator<T> operator %(ExpressionOperator<T> operatorIf, BooleanCommand<T> operatorThen)
        //{
        //    IfThen<T> ifThen = new IfThen<T>();
        //    ifThen.If = operatorIf;
        //    OperateWrapper<T> bw = new OperateWrapper<T>(operatorThen);
        //    ifThen.Then = bw;
        //    return ifThen;
        //}

        //public static implicit operator BooleanCommand<T>(BinaryOperator<T> operatorToWrap)
        //{
        //    return new BooleanCommandWrapper<T>(operatorToWrap);
        //}

        public static implicit operator Operator<TElement>(BooleanCommand<TElement> operate)
        {
            OperateAsOperator<TElement, bool> wrapper = new OperateAsOperator<TElement, bool>(operate);
            return wrapper;
        }

        //public static implicit operator bool(BooleanCommand<TElement> operate)
        //{
        //    return operate.Value;
        //}
    }
}
