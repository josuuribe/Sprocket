using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions
{
    [DataContract]
    public abstract class BooleanFunction<TElement, TParameters> : Function<TElement, TParameters, bool>
        where TElement : IElement
    {
        private static BinaryOperator<TElement> and = new AndAlso<TElement>();
        private static BinaryOperator<TElement> or = new OrElse<TElement>();

        //public static bool operator true(BooleanFunction<TElement, TParameters> operatorTrue)
        //{
        //    or = new Or<TElement>();
        //    return false;
        //}

        //public static bool operator false(BooleanFunction<TElement, TParameters> operatorTrue)
        //{
        //    and = new And<TElement>();
        //    return false;
        //}

        //public static BooleanFunction<TElement, TParameters> operator &(BooleanFunction<TElement, TParameters> operatorLeft, BooleanFunction<TElement, TParameters> operatorRight)
        //{
        //    AndAlso<TElement> andAlso = new AndAlso<TElement>();
        //    andAlso.OperatorLeft = operatorLeft;
        //    andAlso.OperatorRight = operatorRight;
        //    return andAlso;
        //}
        public static Operator<TElement> operator +(BooleanFunction<TElement, TParameters> operatorUnary)
        {
            OperateWrapper<TElement> ow = new OperateWrapper<TElement>(operatorUnary);
            return ow;
        }

        public static Operator<TElement> operator !(BooleanFunction<TElement, TParameters> operatorUnary)
        {
            OperateWrapper<TElement> ow = new OperateWrapper<TElement>(operatorUnary);
            Not<TElement> not = new Not<TElement>();
            not.Operator = ow;
            return not;
        }

        public static BooleanFunction<TElement, TParameters> operator |(BooleanFunction<TElement, TParameters> operatorLeft, BooleanFunction<TElement, TParameters> operatorRight)
        {
            BinaryOperator<TElement> clone = (BinaryOperator<TElement>)or.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            BooleanFunctionWrapper<TElement, TParameters> wrapper = new BooleanFunctionWrapper<TElement, TParameters>(clone);
            or = new OrElse<TElement>();
            return wrapper;
        }

        public static ExpressionOperator<TElement> operator %(bool left, BooleanFunction<TElement, TParameters> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            OperateWrapper<TElement> wrapper = new OperateWrapper<TElement>(new ValueWrapper<TElement, bool>(left));
            it.If = wrapper;
            it.Then = new OperateWrapper<TElement>(operatorRight);
            return it;
        }

        public static ExpressionOperator<TElement> operator %(Operator<TElement> operatorIf, BooleanFunction<TElement, TParameters> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = operatorIf;
            it.Then = new OperateWrapper<TElement>(operatorRight);
            return it;
        }

        public static implicit operator Operator<TElement>(BooleanFunction<TElement, TParameters> function)
        {
            OperateWrapper<TElement> bw = new OperateWrapper<TElement>(function);
            return bw;
        }
    }
}
