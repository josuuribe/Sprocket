using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Operator<T> : IOperator<T>
        where T : IElement
    {
        public abstract bool Match(RuleElement<T> element);

        private static BinaryOperator<T> and = new AndAlso<T>();
        private static BinaryOperator<T> or = new OrElse<T>();

        public static bool operator true(Operator<T> operatorTrue)
        {
            or = new Or<T>();
            return false;
        }

        public static bool operator false(Operator<T> operatorTrue)
        {
            and = new And<T>();
            return false;
        }

        public static Operator<T> operator &(Operator<T> operatorLeft, Operator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static Operator<T> operator |(Operator<T> operatorLeft, Operator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            or = new OrElse<T>();
            return clone;
        }


        public static Operator<T> operator !(Operator<T> operatorUnary)
        {
            Not<T> ngte = new Not<T>();
            ngte.Operator = operatorUnary;
            return ngte;
        }

        //public static LogicalOperator<T> operator +(Operator<T> operatorUnary)
        //{
        //    Yes<T> ngte = new Yes<T>();
        //    ngte.Operator = operatorUnary;
        //    return ngte;
        //}

        public static ExpressionOperator<T> operator &(Operator<T> operatorLeft, bool right)
        {
            And<T> ngte = new And<T>();
            ngte.OperatorLeft = operatorLeft;
            ngte.OperatorRight = new LogicalWrapper<T>(right);
            return ngte;
        }

        public static ExpressionOperator<T> operator &(bool left, Operator<T> operatorRight)
        {
            And<T> ngte = new And<T>();
            ngte.OperatorLeft = new LogicalWrapper<T>(left);
            ngte.OperatorRight = operatorRight;
            return ngte;
        }

        public static ExpressionOperator<T> operator |(Operator<T> operatorLeft, bool operatorRight)
        {
            Or<T> ngte = new Or<T>();
            ngte.OperatorLeft = operatorLeft;
            ngte.OperatorRight = new LogicalWrapper<T>(operatorRight);
            return ngte;
        }

        public static ExpressionOperator<T> operator |(bool operatorLeft, Operator<T> operatorRight)
        {
            Or<T> ngte = new Or<T>();
            ngte.OperatorLeft = new LogicalWrapper<T>(operatorLeft);
            ngte.OperatorRight = operatorRight;
            return ngte;
        }

        public static ExpressionOperator<T> operator -(Operator<T> op)
        {
            False<T> f = new False<T>();
            f.Operator = op;
            return f;
        }

        public static Operator<T> operator >>(Operator<T> operatorLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = operatorLeft;
            AddResult<T> shift = new AddResult<T>();
            shift.Parameters = shiftNumber;
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>((IOperate<T, bool>)shift);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static Operator<T> operator <<(Operator<T> operatorLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = operatorLeft;
            RemoveResult<T> shift = new RemoveResult<T>();
            shift.Parameters = shiftNumber;
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>((IOperate<T, bool>)shift);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static Operator<T> operator %(Operator<T> operatorIf, Operator<T> operatorThen)
        {
            IfThen<T> it = new IfThen<T>();
            it.If = operatorIf;
            it.Then = operatorThen;
            return it;
        }
    }
}