using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Flows;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ConditionalOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine.Entities;
using System;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements
{
    [DataContract]
    public abstract class Operator<TElement> : IOperator<TElement>
        where TElement : IElement
    {
        private static BinaryOperator<TElement> and = new AndAlso<TElement>();
        private static BinaryOperator<TElement> or = new OrElse<TElement>();
        protected IOperator<TElement> next = null;

        public abstract bool Operate(Rule<TElement> rule);

        public virtual IOperator<TElement> Previous { get; set; }

        [DataMember]
        public virtual IOperator<TElement> Next
        {
            get
            {
                return next ?? new End<TElement>();
            }
            set
            {
                IOperator<TElement> end = this;
                while (!(end.Next is End<TElement>))
                {
                    end = end.Next;
                }
                value.Previous = end;
                (end as Operator<TElement>).next = value;
            }
        }

        public Operator()
        {
            this.next = new End<TElement>();
        }

        public Operator(IOperator<TElement> next)
        {
            this.Next = next;
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

        public static Operator<TElement> operator *(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = operatorLeft;
            loop.Block = operatorRight;
            return loop;
        }

        public static Operator<TElement> operator ~(Operator<TElement> @operator)
        {
            Break<TElement> brk = new Break<TElement>(@operator);
            return brk;
        }

        public static Operator<TElement> operator /(Operator<TElement> left, Operator<TElement> right)
        {
            left.Next = right;
            return left;
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

        //public static Operator<TElement> operator %(bool operatorIf, Operator<TElement> operatorThen)
        //{
        //    IfThen<TElement> it = new IfThen<TElement>();
        //    it.If = new OperateWrapper<TElement>(new ValueWrapper<TElement, bool>(operatorIf));
        //    it.Then = operatorThen;
        //    return it;
        //}

        public static Operator<TElement> operator +(Operator<TElement> operatorIf, IfThenElse<TElement> operatorIfThenElse)
        {
            operatorIfThenElse.If = operatorIf;
            return operatorIfThenElse;
        }

        //public static Operator<TElement> operator %(Operator<TElement> operatorIf, IfThen<TElement> operatorIfThen)
        //{
        //    operatorIfThen.If = operatorIf;
        //    return operatorIfThen;
        //}

        public static implicit operator Operator<TElement>(bool value)
        {
            return new ValueAsOperator<TElement, bool>(value);
        }
    }
}