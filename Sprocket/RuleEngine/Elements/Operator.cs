using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements
{
    public enum ExecutionResult
    {
        None,
        Positive,
        Negative,
        Error,
        Exit
    }

    [DataContract]
    public abstract class Operator<TElement> : IOperator<TElement>
        where TElement : IElement
    {
        private static BinaryOperator<TElement> and = new AndAlso<TElement>();
        private static BinaryOperator<TElement> or = new OrElse<TElement>();
        protected ICode next = null;

        public abstract bool Process(Rule<TElement> rule);



        public Operator()
        {
        }

        //public void Operate(Rule<TElement> rule)
        //{
        //    Operator<TElement> op = this;
        //    bool b = true;
        //    do
        //    {
        //        b &= op.Match(rule);
        //        op = op.Next as Operator<TElement>;
        //    }
        //    while (b && op != null);
        //}

        public static bool operator true(Operator<TElement> operatorTrue)
        {
            or = new Or<TElement>();
            return false;
        }

        public static bool operator false(Operator<TElement> operatorFalse)
        {
            and = new And<TElement>();
            return false;
        }


        public static Operator<TElement> operator &(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        {
            BinaryOperator<TElement> cloned = (BinaryOperator<TElement>)and.Clone();
            cloned.OperatorLeft = operatorLeft;
            cloned.OperatorRight = operatorRight;
            and = new AndAlso<TElement>();
            return cloned;
        }

        public static Operator<TElement> operator |(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        {
            BinaryOperator<TElement> cloned = (BinaryOperator<TElement>)or.Clone();
            cloned.OperatorLeft = operatorLeft;
            cloned.OperatorRight = operatorRight;
            or = new OrElse<TElement>();
            return cloned;
        }

        public static Operator<TElement> operator !(Operator<TElement> operatorUnary)
        {
            Not<TElement> ngte = new Not<TElement>();
            ngte.Operator = operatorUnary;
            return ngte;
        }

        //public static Operator<TElement> operator %(Operator<TElement> @operator, Operator<TElement> jump)
        //{
        //    JMP<TElement> jmp = new JMP<TElement>(@operator, jump);
        //    return jmp;
        //}

        //public static Operator<TElement> operator <<(Operator<TElement> @operator, int shift)
        //{
        //    JMP<TElement> jmp = new JMP<TElement>(@operator, Math.Abs(shift) * -1);
        //    return jmp;
        //}

        //public static Operator<TElement> operator *(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        //{
        //    Loop<TElement> loop = new Loop<TElement>();
        //    loop.Condition = operatorLeft;
        //    loop.Block = operatorRight;
        //    return loop;
        //}

        public static IOperator<TElement> operator ^(Operator<TElement> @operator, ExecutionResult executionResult)
        {
            Break<TElement> brk = new Break<TElement>(@operator, executionResult);
            return brk;
        }



        //public static ExpressionOperator<TElement> operator &(Operator<TElement> operatorLeft, bool right)
        //{
        //    And<TElement> ngte = new And<TElement>();
        //    ngte.OperatorLeft = operatorLeft;
        //    ngte.OperatorRight = new LogicalWrapper<TElement>(new ValueWrapper<TElement, bool>(right));
        //    return ngte;
        //}

        //public static ExpressionOperator<TElement> operator &(bool left, Operator<TElement> operatorRight)
        //{
        //    AndAlso<TElement> ngte = new AndAlso<TElement>();
        //    ngte.OperatorLeft = new LogicalWrapper<TElement>(new ValueWrapper<TElement, bool>(left));
        //    ngte.OperatorRight = operatorRight;
        //    return ngte;
        //}

        //public static ExpressionOperator<TElement> operator |(Operator<TElement> operatorLeft, bool right)
        //{
        //    Or<TElement> ngte = new Or<TElement>();
        //    ngte.OperatorLeft = operatorLeft;
        //    ngte.OperatorRight = new LogicalWrapper<TElement>(new ValueWrapper<TElement, bool>(right));
        //    return ngte;
        //}

        //public static ExpressionOperator<TElement> operator |(bool left, Operator<TElement> operatorRight)
        //{
        //    Or<TElement> ngte = new Or<TElement>();
        //    ngte.OperatorLeft = new LogicalWrapper<TElement>(new ValueWrapper<TElement, bool>(left));
        //    ngte.OperatorRight = operatorRight;
        //    return ngte;
        //}

        public static Operator<TElement> operator +(Operator<TElement> op)
        {
            True<TElement> t = new True<TElement>();
            t.Operator = op;
            return t;
        }

        public static Operator<TElement> operator -(Operator<TElement> op)
        {
            False<TElement> f = new False<TElement>();
            f.Operator = op;
            return f;
        }

        public static Operator<TElement> operator %(Operator<TElement> condition, Operand<TElement, bool> jump)
        {
            Jump<TElement> jmp = new Jump<TElement>(condition, jump);
            return jmp;
        }

        public static Operator<TElement> operator %(Operator<TElement> condition, (Operand<TElement, bool>, Operand<TElement, bool>) jump)
        {
            Jump<TElement> jmp = new Jump<TElement>(condition, jump);
            return jmp;
        }

        //public static Operator<TElement> operator %(Operator<TElement> @operator, (Pointer<TElement, TElement>, Operator<TElement>) jump)
        //{
        //    Jump<TElement> jmp = new Jump<TElement>(@operator, jump);
        //    return jmp;
        //}

        //public static Operator<TElement> operator %(bool operatorIf, Operator<TElement> operatorThen)
        //{
        //    IfThen<TElement> it = new IfThen<TElement>();
        //    it.If = new OperateWrapper<TElement>(new ValueWrapper<TElement, bool>(operatorIf));
        //    it.Then = operatorThen;
        //    return it;
        //}

        //public static Operator<TElement> operator +(Operator<TElement> operatorIf, IfThenElse<TElement> operatorIfThenElse)
        //{
        //    operatorIfThenElse.If = operatorIf;
        //    return operatorIfThenElse;
        //}

        //public static Operator<TElement> operator %(Operator<TElement> operatorIf, IfThen<TElement> operatorIfThen)
        //{
        //    operatorIfThen.If = operatorIf;
        //    return operatorIfThen;
        //}

        public static Operator<TElement> operator >>(Operator<TElement> @operator, int shift)
        {
            AddFlag<TElement> addFlag = new AddFlag<TElement>(@operator);
            addFlag.Operator = @operator;
            addFlag.Flag = shift;
            return addFlag;
        }

        public static Operator<TElement> operator <<(Operator<TElement> @operator, int shift)
        {
            RemoveFlag<TElement> removeFlag = new RemoveFlag<TElement>(@operator);
            removeFlag.Operator = @operator;
            removeFlag.Flag = shift;
            return removeFlag;
        }

        public static Operator<TElement> operator *(Operator<TElement> condition, Operand<TElement, bool> operand)
        {
            Loop<TElement, bool> loop = new Loop<TElement, bool>();
            loop.Condition = condition;
            loop.Block = operand;
            return loop;
        }

        public static implicit operator Operator<TElement>(Expression<Func<Rule<TElement>, bool>> operand)
        {
            return new PointerToFunc<TElement, bool>(operand);
        }

        public static implicit operator Operator<TElement>(Operand<TElement, bool> operand)
        {
            return new BooleanOperandAsOperator<TElement>(operand);
        }

        public static implicit operator Operator<TElement>(bool value)
        {
            return new ValueAsOperator<TElement, bool>(value);
        }
    }
}