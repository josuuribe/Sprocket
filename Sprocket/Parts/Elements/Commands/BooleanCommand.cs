using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Commands
{
    [DataContract]
    public abstract class BooleanCommand<T> : Command<T, bool>
        where T : IElement
    {
        private static BinaryOperator<T> and = new AndAlso<T>();
        private static BinaryOperator<T> or = new OrElse<T>();

        public static bool operator true(BooleanCommand<T> operatorTrue)
        {
            or = new Or<T>();
            return false;
        }

        public static bool operator false(BooleanCommand<T> operatorTrue)
        {
            and = new And<T>();
            return false;
        }

        public static BooleanCommand<T> operator &(BooleanCommand<T> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static BooleanCommand<T> operator |(BooleanCommand<T> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            or = new OrElse<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator &(Command<T, bool> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator &(BooleanCommand<T> operatorLeft, Command<T, bool> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            and = new AndAlso<T>();
            return clone;
        }
                
        public static ExpressionOperator<T> operator |(Command<T, bool> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            or = new OrElse<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator |(BooleanCommand<T> operatorLeft, Command<T, bool> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            or = new OrElse<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator &(BooleanCommand<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            clone.OperatorRight = operatorRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator |(BooleanCommand<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            LogicalWrapper<T> bwLeft = new LogicalWrapper<T>(operatorLeft);
            clone.OperatorLeft = bwLeft;
            clone.OperatorRight = operatorRight;
            or = new OrElse<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator &(ExpressionOperator<T> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.Clone();
            clone.OperatorLeft = operatorLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator |(ExpressionOperator<T> operatorLeft, BooleanCommand<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.Clone();
            clone.OperatorLeft = operatorLeft;
            LogicalWrapper<T> bwRight = new LogicalWrapper<T>(operatorRight);
            clone.OperatorRight = bwRight;
            or = new OrElse<T>();
            return clone;
        }

        //public static LogicalOperator<T> operator +(BooleanOperate<T> operatorUnary)
        //{
        //    BoolWrapper<T> bw = new BoolWrapper<T>(operatorUnary);
        //    Yes<T> yes = new Yes<T>();
        //    yes.Operator = bw;
        //    return yes;
        //}

        public static BooleanCommand<T> operator !(BooleanCommand<T> operatorUnary)
        {
            Not<T> not = new Not<T>();
            not.Operator = (Operator<T>)operatorUnary;
            return not;
        }

        public static ExpressionOperator<T> operator %(ExpressionOperator<T> operatorIf, BooleanCommand<T> operatorThen)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = operatorIf;
            LogicalWrapper<T> bw = new LogicalWrapper<T>(operatorThen);
            ifThen.Then = bw;
            return ifThen;
        }

        public static implicit operator BooleanCommand<T>(Operator<T> operatorToWrap)
        {
            return new BooleanCommandWrapper<T>(operatorToWrap);
        }

        public static implicit operator Operator<T>(BooleanCommand<T> operate)
        {
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>(operate);
            return wrapper;
        }
    }
}
