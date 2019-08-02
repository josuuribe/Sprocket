using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Elements.Wrappers;
using RaraAvis.Sprocket.Parts.Interfaces;
using RaraAvis.Sprocket.WorkflowEngine;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators
{
    /// <summary>
    /// Processes functions with in and out parameters.
    /// </summary>
    /// <typeparam name="T">An IElement object.</typeparam>
    /// <typeparam name="U">The function parameters.</typeparam>
    /// <typeparam name="V">The result type.</typeparam>
    [DataContract]
    public abstract class Function<T, U, V> : IOperate<T, V>
        where T : IElement
    {
        [DataMember]
        public U Parameters { get; set; }

        [DataMember]
        public V Result { get; set; }

        public V Value(RuleElement<T> element)
        {
            V v = this.Execute(element);
            this.Result = v;
            return v;
        }

        public abstract V Execute(RuleElement<T> element);

        //public static LogicalOperator<T> operator +(Function<T, U, V> function)
        //{
        //    FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(function);
        //    Yes<T> yes = new Yes<T>();
        //    yes.Operator = wrapper;
        //    return yes;
        //}

        public static ExpressionOperator<T> operator !(Function<T, U, V> function)
        {
            FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(function);
            Not<T> not = new Not<T>();
            not.Operator = wrapper;
            return not;
        }

        public static Operator<T> operator ==(Function<T, U, V> function, V o)
        {
            Equals<T, V> oe = new Equals<T, V>();
            oe.OperateLeft = function;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Operator<T> operator !=(Function<T, U, V> function, V o)
        {
            NotEquals<T, V> oe = new NotEquals<T, V>();
            oe.OperateLeft = function;
            ValueWrapper<T, V> wrapper = new ValueWrapper<T, V>(o);
            oe.OperateRight = wrapper;
            return oe;
        }

        public static Function<T, U, V> operator -(Function<T, U, V> function, U parameter)
        {
            function.Parameters = parameter;
            return function;
        }

        public static ExpressionOperator<T> operator *(ExpressionOperator<T> operatorLeft, Function<T, U, V> operatorRight)
        {
            Loop<T> loop = new Loop<T>();
            loop.If = operatorLeft;
            FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(operatorRight);
            loop.Block = wrapper;
            return loop;
        }

        public static ExpressionOperator<T> operator *(bool left, Function<T, U, V> operatorRight)
        {
            Loop<T> loop = new Loop<T>();
            loop.If = new LogicalWrapper<T>(left);
            FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(operatorRight);
            loop.Block = wrapper;
            return loop;
        }

        public static Operator<T> operator %(Operator<T> operatorLeft, Function<T, U, V> operatorRight)
        {
            IfThen<T> ifThen = new IfThen<T>();
            ifThen.If = operatorLeft;
            FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(operatorRight);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static ExpressionOperator<T> operator ~(Function<T, U, V> ifFunction)
        {
            IfThen<T> ifThen = new IfThen<T>();
            FunctionWrapper<T, U, V> wrapperIf = new FunctionWrapper<T, U, V>(ifFunction);
            ifThen.If = wrapperIf;
            Break<T> brk = new Break<T>();
            LogicalWrapper<T> wrapperBrk = new LogicalWrapper<T>(brk);
            ifThen.Then = wrapperBrk;
            return ifThen;
        }

        public static Operator<T> operator >>(Function<T, U, V> operateLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            FunctionWrapper<T, U, V> wrapperIf = new FunctionWrapper<T, U, V>(operateLeft);
            ifThen.If = wrapperIf;
            AddResult<T> shift = new AddResult<T>();
            shift.Parameters = shiftNumber;
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>((IOperate<T, bool>)shift);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static Operator<T> operator <<(Function<T, U, V> operateLeft, int shiftNumber)
        {
            IfThen<T> ifThen = new IfThen<T>();
            FunctionWrapper<T, U, V> wrapperIf = new FunctionWrapper<T, U, V>(operateLeft);
            ifThen.If = wrapperIf;
            RemoveResult<T> shift = new RemoveResult<T>();
            shift.Parameters = shiftNumber;
            LogicalWrapper<T> wrapper = new LogicalWrapper<T>((IOperate<T, bool>)shift);
            ifThen.Then = wrapper;
            return ifThen;
        }

        public static Batch<T> operator +(Function<T,U,V> function1, Function<T, U, V> function2)
        {
            Batch<T> batch = new Batch<T>();
            batch.Add(function1);
            batch.Add(function2);
            return batch;
        }

        public static Batch<T> operator +(Function<T, U, V> function, Command<T, U> command)
        {
            Batch<T> batch = new Batch<T>();
            batch.Add(function);
            batch.Add(command);
            return batch;
        }

        public static Batch<T> operator +(Command<T, U> command, Function<T, U, V> function)
        {
            Batch<T> batch = new Batch<T>();
            batch.Add(function);
            batch.Add(command);
            return batch;
        }
        //public static implicit operator V(Function<T, U, V> function)
        //{
        //    return function.Result;
        //}

        public static Batch<T> operator +(Batch<T> batch, Function<T, U, V> function)
        {
            batch.Add(function);
            return batch;
        }

        public static Batch<T> operator +(Function<T, U, V> function, Batch<T> batch)
        {
            batch.Add(function);
            return batch;
        }

        public static implicit operator Operator<T>(Function<T, U, V> function)
        {
            FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(function);
            return wrapper;
        }
    }
}
