using RaraAvis.Sprocket.RuleEngine.Casts;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.RuleEngine.Operators;
using RaraAvis.Sprocket.RuleEngine.Operators.BinaryOperators;
using RaraAvis.Sprocket.RuleEngine.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Operators.UnaryOperators;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine
{
    /// <summary>
    /// Possible values returned when a rule is processed.
    /// </summary>
    public enum ExecutionResult
    {
        /// <summary>
        /// Nothing has happened yet.
        /// </summary>
        None,
        /// <summary>
        /// Rule returns true.
        /// </summary>
        Positive,
        /// <summary>
        /// Rule returns false.
        /// </summary>
        Negative,
        /// <summary>
        /// Rule has failed during execution.
        /// </summary>
        Error,
        /// <summary>
        /// Rule has exited mainly because a ~ operator.
        /// </summary>
        Exit
    }
    /// <summary>
    /// Operator to use for execute actions on <see cref="TTarget" /> using operands.
    /// </summary>
    /// <typeparam name="TTarget">Target type to use.</typeparam>
    [DataContract]
    public abstract class Operator<TTarget> : IOperator<TTarget>
        where TTarget : notnull
    {
        private static BinaryOperator<TTarget> and = new AndAlso<TTarget>();
        private static BinaryOperator<TTarget> or = new OrElse<TTarget>();

        /// <inheritdoc/>
        public abstract bool Process(Rule<TTarget> rule);

#pragma warning disable IDE0060 // Remove unused parameter
        public static bool operator true(Operator<TTarget> operatorTrue)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            or = new Or<TTarget>();
            return false;
        }
#pragma warning disable IDE0060 // Remove unused parameter
        public static bool operator false(Operator<TTarget> operatorFalse)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            and = new And<TTarget>();
            return false;
        }
        public static Operator<TTarget> operator &(Operator<TTarget> operatorLeft, Operator<TTarget> operatorRight)
        {
            BinaryOperator<TTarget> cloned = (BinaryOperator<TTarget>)and.Clone();
            cloned.OperatorLeft = operatorLeft;
            cloned.OperatorRight = operatorRight;
            and = new AndAlso<TTarget>();
            return cloned;
        }
        public static Operator<TTarget> operator |(Operator<TTarget> operatorLeft, Operator<TTarget> operatorRight)
        {
            BinaryOperator<TTarget> cloned = (BinaryOperator<TTarget>)or.Clone();
            cloned.OperatorLeft = operatorLeft;
            cloned.OperatorRight = operatorRight;
            or = new OrElse<TTarget>();
            return cloned;
        }
        public static Operator<TTarget> operator !(Operator<TTarget> operatorUnary)
        {
            Not<TTarget> ngte = new Not<TTarget>
            {
                Operator = operatorUnary
            };
            return ngte;
        }
        public static IOperator<TTarget> operator ~(Operator<TTarget> @operator)
        {
            Break<TTarget> brk = new Break<TTarget>(@operator);
            return brk;
        }
        public static Operator<TTarget> operator +(Operator<TTarget> op)
        {
            True<TTarget> t = new True<TTarget>(op);
            return t;
        }
        public static Operator<TTarget> operator -(Operator<TTarget> op)
        {
            False<TTarget> f = new False<TTarget>(op);
            return f;
        }
        public static Operator<TTarget> operator %(Operator<TTarget> condition, Operand<TTarget, bool> jump)
        {
            Jump<TTarget> jmp = new Jump<TTarget>(condition, jump);
            return jmp;
        }
        public static Operator<TTarget> operator %(Operator<TTarget> condition, (Operand<TTarget, bool>, Operand<TTarget, bool>) jump)
        {
            Jump<TTarget> jmp = new Jump<TTarget>(condition, jump);
            return jmp;
        }
        public static Operator<TTarget> operator >>(Operator<TTarget> @operator, int shift)
        {
            AddFlag<TTarget> addFlag = new AddFlag<TTarget>(@operator, shift);
            return addFlag;
        }
        public static Operator<TTarget> operator <<(Operator<TTarget> @operator, int shift)
        {
            RemoveFlag<TTarget> removeFlag = new RemoveFlag<TTarget>(@operator, shift);
            return removeFlag;
        }
        public static Operator<TTarget> operator *(Operator<TTarget> condition, Operand<TTarget, bool> operand)
        {
            Loop<TTarget, bool> loop = new Loop<TTarget, bool>(condition, operand);
            return loop;
        }
        public static implicit operator Operator<TTarget>(Expression<Func<Rule<TTarget>, bool>> operand)
        {
            return new ExpressionAsOperand<TTarget, bool>(operand);
        }
        public static implicit operator Operator<TTarget>(Operand<TTarget, bool> operand)
        {
            return new BooleanOperandAsOperator<TTarget>(operand);
        }
        public static implicit operator Operator<TTarget>(bool value)
        {
            return new ValueAsOperator<TTarget, bool>(value);
        }
        internal class NullOperator : Operator<TTarget>
        {
            public override bool Process(Rule<TTarget> rule)
            {
                return false;
            }
        }
    }
}