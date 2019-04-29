using RaraAvis.Sprocket.Parts.Elements.Commands;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements
{
    [DataContract]
    public abstract class Command<T, U> : IOperate<T, U>
        where T : IElement
    {
        public abstract U Value(RuleElement<T> element);

        public static ExpressionOperator<T> operator >>(Command<T, U> operatorLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = (Operator<T>)operatorLeft;
            AddResult<T> shift = new AddResult<T>();
            shift.Parameters = shiftNumber;
            ifThen.Then = (Operator<T>)shift;
            return ifThen;
        }

        public static ExpressionOperator<T> operator <<(Command<T, U> operatorLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = (Operator<T>)operatorLeft;
            RemoveResult<T> shift = new RemoveResult<T>();
            shift.Parameters = shiftNumber;
            ifThen.Then = (Operator<T>)shift;
            return ifThen;
        }

        public static ExpressionOperator<T> operator /(Command<T, U> operatorLeft, Command<T, U> operatorRight)
        {
            IfThenElse<T> ite = new IfThenElse<T>();
            ite.Then = (Operator<T>)operatorLeft;
            ite.Else = (Operator<T>)operatorRight;
            return ite;
        }

        public static ExpressionOperator<T> operator %(bool boolConstant, Command<T, U> operatorRight)
        {
            IfThen<T> it = new IfThen<T>();
            it.If = new LogicalWrapper<T>(boolConstant);
            it.Then = (Operator<T>)operatorRight;
            return it;
        }

        public static ExpressionOperator<T> operator %(Operator<T> operatorIf, Command<T, U> operatorRight)
        {
            IfThen<T> it = new IfThen<T>();
            it.If = operatorIf;
            it.Then = (Operator<T>)operatorRight;
            return it;
        }

        public static ExpressionOperator<T> operator *(ExpressionOperator<T> expression, Command<T, U> function)
        {
            Loop<T> loop = new Loop<T>();
            loop.If = expression;
            loop.Block = (Operator<T>)function;
            return loop;
        }


        public static BooleanCommand<T> operator +(Command<T, U> command1, Command<T, U> command2)
        {
            Batch<T> batch = new Batch<T>();
            batch.Add(command1);
            batch.Add(command2);
            return batch;
        }

        public static BooleanCommand<T> operator +(Batch<T> batch, Command<T, U> command)
        {
            batch.Add(command);
            return batch;
        }

        public static BooleanCommand<T> operator +(Command<T, U> command, Batch<T> batch)
        {
            batch.Add(command);
            return batch;
        }

        public static implicit operator Operator<T>(Command<T, U> command)
        {
            return new CommandWrapper<T, U>(command);
        }

        //public static LogicalOperator<T> operator +(Operate<T, U> operatorUnary)
        //{
        //    BooleanVariable<T> bv = new BooleanVariable<T>((dynamic)operatorUnary);
        //    BoolWrapper<T> bw = new BoolWrapper<T>(bv);
        //    Yes<T> yes = new Yes<T>();
        //    yes.Operator = bw;
        //    return yes;
        //}

        //public static LogicalOperator<T> operator -(Operate<T, U> operatorUnary)
        //{
        //    BooleanVariable<T> bv = new BooleanVariable<T>((dynamic)operatorUnary);
        //    BoolWrapper<T> bw = new BoolWrapper<T>(bv);
        //    False<T> not = new False<T>();
        //    not.Operator = bw;
        //    return not;
        //}

        //public static implicit operator LogicalOperator<T>(Operate<T, U> function)
        //{
        //    OperateWrapper<T, U> wrapper = new OperateWrapper<T, U>(function);
        //    Ignore<T> yes = new Ignore<T>();
        //    yes.Operator = wrapper;
        //    return yes;
        //}
    }
}
