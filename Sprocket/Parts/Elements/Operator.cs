using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.BinaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Operator<TElement> : IOperator<TElement>
        where TElement : IElement
    {
        public abstract bool Match(RuleElement<TElement> element);

        private static BinaryOperator<TElement> and = new AndAlso<TElement>();
        private static BinaryOperator<TElement> or = new OrElse<TElement>();

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

        public static Operator<TElement> operator ^(Operator<TElement> operatorLeft, int stageId)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = operatorLeft;
            JMP<TElement> jmp = new JMP<TElement>();
            jmp.Parameters = stageId;
            FunctionWrapper<TElement, int, bool> function = new FunctionWrapper<TElement, int, bool>(jmp);
            ifThen.Then = function;
            return function;
        }

        public static Operator<TElement> operator *(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = operatorLeft;
            loop.Block = operatorRight;
            return loop;
        }

        public static Operator<TElement> operator ~(Operator<TElement> expressionIf)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = expressionIf;
            Break<TElement> brk = new Break<TElement>();
            OperateWrapper<TElement> wrapperBrk = new OperateWrapper<TElement>(brk);
            ifThen.Then = wrapperBrk;
            return ifThen;
        }

        public static Batch<TElement> operator +(Operator<TElement> operatorLeft, Operator<TElement> operatorRight)
        {
            Batch<TElement> batch = new Batch<TElement>();
            BooleanCommandWrapper<TElement> booleanMethodWrapperLeft = new BooleanCommandWrapper<TElement>(operatorLeft);
            batch.Add(booleanMethodWrapperLeft);
            BooleanCommandWrapper<TElement> booleanMethodWrapperRight = new BooleanCommandWrapper<TElement>(operatorRight);
            batch.Add(booleanMethodWrapperRight);
            return batch;
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

        public static IOperate<TElement, bool> operator +(Operator<TElement> op)
        {
            return null;
        }

        public static ExpressionOperator<TElement> operator -(Operator<TElement> op)
        {
            False<TElement> f = new False<TElement>();
            f.Operator = op;
            return f;
        }

        public static Operator<TElement> operator >>(Operator<TElement> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = operatorLeft;
            AddFlag<TElement> result = new AddFlag<TElement>();
            result.Parameters = shiftNumber;
            OperateWrapper<TElement> wrapper = new OperateWrapper<TElement>((IOperate<TElement, bool>)result);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static Operator<TElement> operator <<(Operator<TElement> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = operatorLeft;
            RemoveFlag<TElement> shift = new RemoveFlag<TElement>();
            shift.Parameters = shiftNumber;
            OperateWrapper<TElement> wrapper = new OperateWrapper<TElement>((IOperate<TElement, bool>)shift);
            ifThen.Then = wrapper;
            return ifThen;
        }

        //public static Operator<TElement> operator %(bool operatorIf, Operator<TElement> operatorThen)
        //{
        //    IfThen<TElement> it = new IfThen<TElement>();
        //    it.If = new OperateWrapper<TElement>(new ValueWrapper<TElement, bool>(operatorIf));
        //    it.Then = operatorThen;
        //    return it;
        //}

        public static Operator<TElement> operator %(Operator<TElement> operatorIf, IfThenElse<TElement> operatorIfThenElse)
        {
            operatorIfThenElse.If = operatorIf;
            return operatorIfThenElse;
        }

        public static Operator<TElement> operator %(Operator<TElement> operatorIf, IfThen<TElement> operatorIfThen)
        {
            operatorIfThen.If = operatorIf;
            return operatorIfThen;
        }

        public static implicit operator Operator<TElement>(bool value)
        {
            var operate = new ValueWrapper<TElement, bool>(value);
            return new OperateWrapper<TElement>(operate);
        }
    }
}