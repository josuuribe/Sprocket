using RaraAvis.Sprocket.Parts.Elements.Casts;
using RaraAvis.Sprocket.Parts.Elements.Functions.Kernel;
using RaraAvis.Sprocket.Parts.Elements.Operators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ComparisonOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.ConnectiveOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.IterationOperators;
using RaraAvis.Sprocket.Parts.Elements.Operators.ExpressionOperators.UnaryOperators;
using RaraAvis.Sprocket.Parts.Interfaces;
using System.Runtime.Serialization;

namespace RaraAvis.Sprocket.Parts.Elements.Commands.ExpressionOperators
{
    /// <summary>
    /// Processes functions with in and out parameters.
    /// </summary>
    /// <typeparam name="TElement">An IElement object.</typeparam>
    /// <typeparam name="TParameters">The function parameters.</typeparam>
    /// <typeparam name="TValue">The result type.</typeparam>
    [DataContract]
    public abstract class Function<TElement, TParameters, TValue> : Command<TElement, TValue>
        where TElement : IElement
    {
        public Function() { }
        public Function(TElement element = default(TElement), TParameters parameters = default(TParameters)) : base(element) 
        {
            this.Parameters = parameters;
        }

        [DataMember]
        public TParameters Parameters { get; set; }

        //public static LogicalOperator<T> operator +(Function<T, U, V> function)
        //{
        //    FunctionWrapper<T, U, V> wrapper = new FunctionWrapper<T, U, V>(function);
        //    Yes<T> yes = new Yes<T>();
        //    yes.Operator = wrapper;
        //    return yes;
        //}

        //public static ExpressionOperator<TElement> operator !(Function<TElement, TParameters, TValue> function)
        //{
        //    FunctionWrapper<TElement, TParameters, TValue> wrapper = new FunctionWrapper<TElement, TParameters, TValue>(function);
        //    Not<TElement> not = new Not<TElement>();
        //    not.Operator = wrapper;
        //    return not;
        //}

        public static Operator<TElement> operator ==(Function<TElement, TParameters, TValue> function, TValue o)
        {
            Equals<TElement, TValue> oe = new Equals<TElement, TValue>();
            oe.OperateLeft = function;
            oe.OperateRight = new ValueAsOperate<TElement, TValue>(o);
            return oe;
        }

        public static Operator<TElement> operator !=(Function<TElement, TParameters, TValue> function, TValue o)
        {
            NotEquals<TElement, TValue> oe = new NotEquals<TElement, TValue>();
            oe.OperateLeft = function;
            oe.OperateRight = new ValueAsOperate<TElement, TValue>(o);
            return oe;
        }

        public static Function<TElement, TParameters, TValue> operator -(Function<TElement, TParameters, TValue> function, TParameters parameter)
        {
            function.Parameters = parameter;
            return function;
        }

        public static Operator<TElement> operator *(Operator<TElement> operatorLeft, Function<TElement, TParameters, TValue> function)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = operatorLeft;
            OperateAsOperator<TElement, TValue> wrapper = new OperateAsOperator<TElement, TValue>(function);
            loop.Block = wrapper;
            return loop;
        }

        public static ExpressionOperator<TElement> operator *(bool left, Function<TElement, TParameters, TValue> function)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = new ValueAsOperator<TElement, bool>(left);
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            loop.Block = operateAsOperator;
            return loop;
        }

        public static ExpressionOperator<TElement> operator %(bool left, Function<TElement, TParameters, TValue> function)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new ValueAsOperator<TElement, bool>(left);
            it.Then = new OperateAsOperator<TElement, TValue>(function);
            return it;
        }

        public static Operator<TElement> operator %(Operator<TElement> operatorLeft, Function<TElement, TParameters, TValue> function)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = operatorLeft;
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            ifThen.Then = operateAsOperator;
            return ifThen;
        }

        public static ExpressionOperator<TElement> operator ~(Function<TElement, TParameters, TValue> function)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            ifThen.If = operateAsOperator;
            Break<TElement> brk = new Break<TElement>();
            OperateAsOperator<TElement, bool> wrapperBrk = new OperateAsOperator<TElement, bool>(brk);
            ifThen.Then = wrapperBrk;
            return ifThen;
        }

        //public static Operator<TElement> operator >>(Function<TElement, TParameters, TValue> function, int shiftNumber)
        //{
        //    IfThen<TElement> ifThen = new IfThen<TElement>();
        //    OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
        //    ifThen.If = operateAsOperator;
        //    AddFlag<TElement> shift = new AddFlag<TElement>(shiftNumber);
        //    OperateAsOperator<TElement, bool> wrapper = new OperateAsOperator<TElement, bool>(shift);
        //    ifThen.Then = wrapper;
        //    return ifThen;
        //}

        //public static Operator<TElement> operator <<(Function<TElement, TParameters, TValue> function, int shiftNumber)
        //{
        //    IfThen<TElement> ifThen = new IfThen<TElement>();
        //    OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
        //    ifThen.If = operateAsOperator;
        //    RemoveFlag<TElement> shift = new RemoveFlag<TElement>(shiftNumber);
        //    OperateAsOperator<TElement, bool> wrapper = new OperateAsOperator<TElement, bool>(shift);
        //    ifThen.Then = wrapper;
        //    return ifThen;
        //}

        public static Batch<TElement> operator +(Function<TElement, TParameters, TValue> function1, Function<TElement, TParameters, TValue> function2)
        {
            Batch<TElement> batch = new Batch<TElement>();
            batch.Add(function1);
            batch.Add(function2);
            return batch;
        }

        public static Batch<TElement> operator +(Batch<TElement> batch, Function<TElement, TParameters, TValue> function)
        {
            batch.Add(function);
            return batch;
        }

        public static Batch<TElement> operator +(Function<TElement, TParameters, TValue> function, Batch<TElement> batch)
        {
            batch.Add(function);
            return batch;
        }

        public static Operator<TElement> operator !(Function<TElement, TParameters, TValue> operatorUnary)
        {
            OperateAsOperator<TElement, TValue> ow = new OperateAsOperator<TElement, TValue>(operatorUnary);
            Not<TElement> not = new Not<TElement>();
            not.Operator = ow;
            return not;
        }

        public static implicit operator Operator<TElement>(Function<TElement, TParameters, TValue> function)
        {
            return new OperateAsOperator<TElement, TValue>(function);
        }
    }
}
