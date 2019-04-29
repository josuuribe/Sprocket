using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Operators
{
    /// <summary>
    /// A base class for all logical operators.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    [KnownType("GetTypes")]
    [DataContract]
    public abstract class ExpressionOperator<T> : Operator<T>
        where T : IElement
    {
        private static BinaryOperator<T> and = new AndAlso<T>();
        private static BinaryOperator<T> or = new OrElse<T>();

        public static bool operator true(ExpressionOperator<T> operatorTrue)
        {
            or = new Or<T>();
            return false;
        }

        public static bool operator false(ExpressionOperator<T> operatorTrue)
        {
            and = new And<T>();
            return false;
        }

        public static ExpressionOperator<T> operator &(ExpressionOperator<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)and.MemberwiseClone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            and = new AndAlso<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator |(ExpressionOperator<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            BinaryOperator<T> clone = (BinaryOperator<T>)or.MemberwiseClone();
            clone.OperatorLeft = operatorLeft;
            clone.OperatorRight = operatorRight;
            or = new OrElse<T>();
            return clone;
        }

        public static ExpressionOperator<T> operator !(ExpressionOperator<T> operatorUnary)
        {
            Not<T> ngte = new Not<T>();
            ngte.Operator = operatorUnary;
            return ngte;
        }

        public static ExpressionOperator<T> operator +(ExpressionOperator<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            IfThenElse<T> ite = (IfThenElse<T>)operatorRight;
            ite.If = operatorLeft;
            return operatorRight;
        }

        public static ExpressionOperator<T> operator %(bool operatorLeft, ExpressionOperator<T> operatorRight)
        {
            IfThenElse<T> ite = (IfThenElse<T>)operatorRight;
            ite.If = new LogicalWrapper<T>(operatorLeft);
            return ite;
        }


        public static ExpressionOperator<T> operator *(ExpressionOperator<T> operatorLeft, ExpressionOperator<T> operatorRight)
        {
            Loop<T> loop = new Loop<T>();
            loop.If = operatorLeft;
            loop.Block = operatorRight;
            return loop;
        }

        public static Operator<T> operator ^(ExpressionOperator<T> operatorLeft, string stage)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = operatorLeft;
            JMP<T> jmp = new JMP<T>();
            jmp.Parameters = stage;
            FunctionWrapper<T, string, bool> function = new FunctionWrapper<T, string, bool>(jmp);
            ifThen.Then = function;
            return function;
        }

        private static Type[] GetTypes()
        {
            Type[] t = new Type[0];
            return t;
        }
    }
}
