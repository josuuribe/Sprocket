using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Command<TElement, TValue> : IOperate<TElement, TValue>
        where TElement : IElement
    {
        public abstract TValue Value(RuleElement<TElement> element);

        public static ExpressionOperator<TElement> operator >>(Command<TElement, TValue> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = (Operator<TElement>)operatorLeft;
            AddFlag<TElement> shift = new AddFlag<TElement>();
            shift.Parameters = shiftNumber;
            ifThen.Then = (Operator<TElement>)shift;
            return ifThen;
        }

        public static ExpressionOperator<TElement> operator <<(Command<TElement, TValue> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = (Operator<TElement>)operatorLeft;
            RemoveFlag<TElement> shift = new RemoveFlag<TElement>();
            shift.Parameters = shiftNumber;
            ifThen.Then = (Operator<TElement>)shift;
            return ifThen;
        }

        public static ExpressionOperator<TElement> operator %(bool boolConstant, Command<TElement, TValue> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new OperateWrapper<TElement>(new ValueWrapper<TElement, bool>(boolConstant));
            it.Then = (Operator<TElement>)operatorRight;
            return it;
        }

        public static ExpressionOperator<TElement> operator %(Operator<TElement> operatorIf, Command<TElement, TValue> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = operatorIf;
            it.Then = (Operator<TElement>)operatorRight;
            return it;
        }

        public static ExpressionOperator<TElement> operator *(Operator<TElement> expression, Command<TElement, TValue> command)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = expression;
            loop.Block = (Operator<TElement>)command;
            return loop;
        }

        public static Batch<TElement> operator +(Command<TElement, TValue> command1, Command<TElement, TValue> command2)
        {
            Batch<TElement> batch = new Batch<TElement>();
            batch.Add(command1);
            batch.Add(command2);
            return batch;
        }

        public static implicit operator Operator<TElement>(Command<TElement, TValue> command)
        {
            return new CommandWrapper<TElement, TValue>(command);
        }

        public static IfThenElse<TElement> operator /(Command<TElement, TValue> operatorThen, Command<TElement, TValue> operatorElse)
        {
            IfThenElse<TElement> ite = new IfThenElse<TElement>();
            ite.Then = new CommandWrapper<TElement, TValue>(operatorThen);
            ite.Else = new CommandWrapper<TElement, TValue>(operatorElse);
            return ite;
        }

        public static Operator<TElement> operator ==(Command<TElement, TValue> command, TValue o)
        {
            Equals<TElement, TValue> oe = new Equals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueWrapper<TElement, TValue> wrapper = new ValueWrapper<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Operator<TElement> operator !=(Command<TElement, TValue> command, TValue o)
        {
            NotEquals<TElement, TValue> oe = new NotEquals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueWrapper<TElement, TValue> wrapper = new ValueWrapper<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static implicit operator TValue(Command<TElement, TValue> command)
        {
            CommandWrapper<TElement, TValue> wrapper = new CommandWrapper<TElement, TValue>(command);
            return wrapper.Result;
        }
    }
}
