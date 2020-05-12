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
        public Function() : base() { }
        public Function(TElement element) { }
        public Function(TElement element, TParameters parameters) : this(element)
        {
            this.Parameters = parameters;
        }

        public Function(TParameters parameters) : this(default(TElement), parameters)
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
        //    ValueAsOperator<TElement, TValue> wrapper = new ValueAsOperator<TElement, TValue>(function);
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

        public static Operator<TElement> operator *(bool left, Function<TElement, TParameters, TValue> function)
        {
            Loop<TElement> loop = new Loop<TElement>();
            loop.Condition = new ValueAsOperator<TElement, bool>(left);
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            loop.Block = operateAsOperator;
            return loop;
        }

        public static Operator<TElement> operator +(bool left, Function<TElement, TParameters, TValue> function)
        {
            IfThen<TElement> it = new IfThen<TElement>();
            it.If = new ValueAsOperator<TElement, bool>(left);
            it.Then = new OperateAsOperator<TElement, TValue>(function);
            return it;
        }

        public static Operator<TElement> operator +(Operator<TElement> operatorLeft, Function<TElement, TParameters, TValue> function)
        {
            IfThen<TElement> ifThen = new IfThen<TElement>();
            ifThen.If = operatorLeft;
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            ifThen.Then = operateAsOperator;
            return ifThen;
        }

        public static Operator<TElement> operator ~(Function<TElement, TParameters, TValue> function)
        {
            OperateAsOperator<TElement, TValue> operateAsOperator = new OperateAsOperator<TElement, TValue>(function);
            Break<TElement> brk = new Break<TElement>(operateAsOperator);
            return brk;
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

        public static Operator<TElement> operator /(Function<TElement, TParameters, TValue> function1, Function<TElement, TParameters, TValue> function2)
        {
            OperateAsOperator<TElement, TValue> f1 = new OperateAsOperator<TElement, TValue>(function1);
            OperateAsOperator<TElement, TValue> f2 = new OperateAsOperator<TElement, TValue>(function2);
            f1.Next = f2;

            return f1;
        }

        public static Operator<TElement> operator /(Operator<TElement> @operator, Function<TElement, TParameters, TValue> function)
        {
            OperateAsOperator<TElement, TValue> f1 = new OperateAsOperator<TElement, TValue>(function);
            @operator.Next = f1;
            return @operator;
        }

        public static Operator<TElement> operator /(Function<TElement, TParameters, TValue> function, Operator<TElement> @operator)
        {
            OperateAsOperator<TElement, TValue> f1 = new OperateAsOperator<TElement, TValue>(function);
            f1.Next = @operator;
            return f1;
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
