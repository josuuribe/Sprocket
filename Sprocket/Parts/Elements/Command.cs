using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Command<TElement, TValue> : Operate<TElement, TValue>
        where TElement : IElement
    {
        public Command() : base() { }

        public Command(TElement element) : base(element) { }

        public static ExpressionOperator<TElement> operator >>(Command<TElement, TValue> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = (Operator<TElement>)operatorLeft;
            AddFlag<TElement> shift = new AddFlag<TElement>(shiftNumber);
            ifThen.Then = (Operator<TElement>)shift;
            return ifThen;
        }

        public static ExpressionOperator<TElement> operator <<(Command<TElement, TValue> operatorLeft, int shiftNumber)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = new OperateAsOperator<TElement, TValue>(operatorLeft);
            RemoveFlag<TElement> shift = new RemoveFlag<TElement>(shiftNumber);
            ifThen.Then = new OperateAsOperator<TElement, bool>(shift);
            return ifThen;
        }

        public static ExpressionOperator<TElement> operator %(bool boolConstant, Command<TElement, TValue> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new ValueAsOperator<TElement, bool>(boolConstant);
            it.Then = new CommandAsOperator<TElement, TValue>(operatorRight);
            return it;
        }

        public static ExpressionOperator<TElement> operator %(Operator<TElement> operatorIf, Command<TElement, TValue> command)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = operatorIf;
            it.Then = new CommandAsOperator<TElement, TValue>(command);
            return it;
        }

        public static ExpressionOperator<TElement> operator *(Operator<TElement> expression, Command<TElement, TValue> command)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = expression;
            loop.Block = new CommandAsOperator<TElement, TValue>(command);
            return loop;
        }

        public static Batch<TElement> operator +(Command<TElement, TValue> command1, Command<TElement, TValue> command2)
        {
            Batch<TElement> batch = new Batch<TElement>();
            batch.Add(command1);
            batch.Add(command2);
            return batch;
        }

        public static IfThenElse<TElement> operator /(Command<TElement, TValue> operatorThen, Command<TElement, TValue> operatorElse)
        {
            IfThenElse<TElement> ite = new IfThenElse<TElement>();
            ite.Then = new CommandAsOperator<TElement, TValue>(operatorThen);
            ite.Else = new CommandAsOperator<TElement, TValue>(operatorElse);
            return ite;
        }

        public static Operator<TElement> operator ==(Command<TElement, TValue> command, TValue o)
        {
            Equals<TElement, TValue> oe = new Equals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueAsOperate<TElement, TValue> wrapper = new ValueAsOperate<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Operator<TElement> operator !=(Command<TElement, TValue> command, TValue o)
        {
            NotEquals<TElement, TValue> oe = new NotEquals<TElement, TValue>();
            oe.OperateLeft = command;
            ValueAsOperate<TElement, TValue> wrapper = new ValueAsOperate<TElement, TValue>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Operator<TElement> operator +(Command<TElement, TValue> command)
        {
            return new True<TElement>(new OperateAsOperator<TElement, TValue>(command));
        }

        public static Operator<TElement> operator -(Command<TElement, TValue> command)
        {
            return new False<TElement>(new OperateAsOperator<TElement, TValue>(command));
        }

        public static Operator<TElement> operator !(Command<TElement, TValue> command)
        {
            return new Not<TElement>(new OperateAsOperator<TElement, TValue>(command));
        }

        public static implicit operator TValue(Command<TElement, TValue> command)
        {// No devuelve el valor correctamente, el Match no devuelve el Wrapper y los comandos no deberían almacenar valor
            return command.Process(command.element);
        }

        public static implicit operator Operator<TElement>(Command<TElement, TValue> command)
        {
            return new OperateAsOperator<TElement, TValue>(command);
        }
    }
}
