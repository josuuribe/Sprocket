using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Functions
{
    [DataContract]
    public abstract class BooleanFunction<TElement, TParameters> : Function<TElement, TParameters, bool>
        where TElement : IElement
    {
        public BooleanFunction()
        { }

        public BooleanFunction(TElement element = default(TElement), TParameters parameters = default(TParameters)) : base(element, parameters)
        { }
        //private static BinaryOperator<TElement> and = new AndAlso<TElement>();
        //private static BinaryOperator<TElement> or = new OrElse<TElement>();

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
            OperateAsOperator<TElement, bool> ow = new OperateAsOperator<TElement, bool>(operatorUnary);
            return ow;
        }

        public static Operator<TElement> operator !(BooleanFunction<TElement, TParameters> operatorUnary)
        {
            OperateAsOperator<TElement, bool> ow = new OperateAsOperator<TElement, bool>(operatorUnary);
            Not<TElement> not = new Not<TElement>();
            not.Operator = ow;
            return not;
        }

        //public static BooleanFunction<TElement, TParameters> operator |(BooleanFunction<TElement, TParameters> operatorLeft, BooleanFunction<TElement, TParameters> operatorRight)
        //{
        //    BinaryOperator<TElement> clone = (BinaryOperator<TElement>)or.Clone();
        //    clone.OperatorLeft = operatorLeft;
        //    clone.OperatorRight = operatorRight;
        //    BooleanFunctionWrapper<TElement, TParameters> wrapper = new BooleanFunctionWrapper<TElement, TParameters>(clone);
        //    or = new OrElse<TElement>();
        //    return wrapper;
        //}

        public static ExpressionOperator<TElement> operator %(bool left, BooleanFunction<TElement, TParameters> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new ValueAsOperator<TElement, bool>(left);
            it.Then = new OperateAsOperator<TElement, bool>(operatorRight);
            return it;
        }

        public static ExpressionOperator<TElement> operator %(Operator<TElement> operatorIf, BooleanFunction<TElement, TParameters> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = operatorIf;
            it.Then = new OperateAsOperator<TElement, bool>(operatorRight);
            return it;
        }

        public static implicit operator Operator<TElement>(BooleanFunction<TElement, TParameters> function)
        {
            OperateAsOperator<TElement, bool> bw = new OperateAsOperator<TElement, bool>(function);
            return bw;
        }
    }
}
