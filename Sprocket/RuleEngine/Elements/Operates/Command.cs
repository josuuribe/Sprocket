using RaraAvis.Sprocket.RuleEngine.Elements.Casts;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ComparisonOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.ConditionalOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.IterationOperators;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.Kernel;
using RaraAvis.Sprocket.RuleEngine.Elements.Operators.UnaryOperators;
using RaraAvis.Sprocket.RuleEngine.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.RuleEngine.Elements.Operates
{
    [DataContract]
    public abstract class Command<TElement, TValue> : Operand<TElement, TValue>
        where TElement : IElement
    {
        public Command() : base() { }

        public Command(TElement element) : base(element) { }

        //public static Operator<TElement> operator >>(Command<TElement, TValue> command, int shift)
        //{
        //    var op = new OperateAsOperator<TElement, TValue>(command);
        //    AddFlag<TElement> addFlag = new AddFlag<TElement>(op, shift);
        //    return addFlag;
        //}

        //public static Operator<TElement> operator <<(Command<TElement, TValue> command, int shift)
        //{
        //    var op = new OperateAsOperator<TElement, TValue>(command);
        //    RemoveFlag<TElement> removeFlag = new RemoveFlag<TElement>(op, shift);
        //    return removeFlag;
        //}

        public static Operator<TElement> operator %(Command<TElement, TValue> @operator, Operator<TElement> jump)
        {
            JMP<TElement> jmp = new JMP<TElement>(@operator, jump);
            return jmp;
        }

        public static Operator<TElement> operator +(bool boolConstant, Command<TElement, TValue> operatorRight)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new ValueAsOperator<TElement, bool>(boolConstant);
            it.Then = new CommandAsOperator<TElement, TValue>(operatorRight);
            return it;
        }

        public static Operator<TElement> operator +(Operator<TElement> operatorIf, Command<TElement, TValue> command)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = operatorIf;
            it.Then = new CommandAsOperator<TElement, TValue>(command);
            return it;
        }

        public static Operator<TElement> operator *(Operator<TElement> expression, Command<TElement, TValue> command)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = expression;
            loop.Block = new CommandAsOperator<TElement, TValue>(command);
            return loop;
        }

        public static Operator<TElement> operator /(Command<TElement, TValue> command1, Command<TElement, TValue> command2)
        {
            OperateAsOperator<TElement, TValue> operator1 = new OperateAsOperator<TElement, TValue>(command1);
            OperateAsOperator<TElement, TValue> operator2 = new OperateAsOperator<TElement, TValue>(command2);
            operator1.Next = operator2;

            return operator1;
        }

        public static IfThenElse<TElement> operator -(Command<TElement, TValue> operatorThen, Command<TElement, TValue> operatorElse)
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

        //public static Operator<TElement> operator -(Operator<TElement> @operator, Command<TElement, TValue> command)
        //{
        //    OperateAsOperator<TElement, TValue> op = new OperateAsOperator<TElement, TValue>(command);
        //    @operator.Next = op;
        //    return @operator;
        //}

        public static implicit operator TValue(Command<TElement, TValue> command)
        {// No devuelve el valor correctamente, el Match no devuelve el Wrapper y los comandos no deberían almacenar valor
            return command.Value(command.element);
        }

        public static implicit operator Operator<TElement>(Command<TElement, TValue> command)
        {
            return new OperateAsOperator<TElement, TValue>(command);
        }
    }
}
